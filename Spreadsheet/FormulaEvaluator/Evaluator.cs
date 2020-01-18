/// <summary> 
/// Author:    [Alec Chae(u1172965)] 
/// Partner:   [None] 
/// Date:      [1/17/2020] 
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and [Alec Chae(u1172965)] - This work may not be copied for use in Academic Coursework. 
/// 
/// I, [Alec Chae(u1172965)], certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
///    [Implementation of formula evaluator in order of priority] 
/// </summary>
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    public static class Evaluator
    {
        public delegate int Lookup(String variable_name);
        
        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            Stack<String> operation = new Stack<String>();
            Stack<int> value = new Stack<int>();

            for (int i = 0; i < substrings.Length; i++)
            {
                
                if (substrings[i].Length>=2 && !int.TryParse(substrings[i].Substring(0,1), out int result))// if the cell contains variable
                {
                    int variableVal = variableEvaluator(substrings[i].ToString());
                    substrings[i] = variableVal.ToString();
                }
                if (int.TryParse(substrings[i], out int convert)) // checks if it's an integer
                {
                    if(value.Count==0) // if value stack is empty
                    {
                        value.Push(convert);
                        continue;
                    }
                    if (operation.Count > 0)
                    {
                        if (operation.Peek().Equals("*")) //operates multiplication
                        {
                            int popped = value.Pop();
                            int tempVal = convert * popped;
                            value.Push(tempVal);
                            operation.Pop();
                            continue;
                        }

                        if (operation.Peek().Equals("/")) //operates division
                        {
                            if (convert == 0)
                            {
                                throw new ArgumentException("Cannot divide zero"); //edge case
                            }
                            int popped = value.Pop();
                            int tempVal = popped / convert;
                            value.Push(tempVal);
                            operation.Pop();
                            continue;
                        }
                        else//if there is no * or / stack the value
                        {
                            value.Push(convert);
                            continue;
                        }
                    }
                }

                if (substrings[i].Equals("+")|| substrings[i].Equals("-"))
                {
                    if(value.Count==0)//you can't add operation when value stack is empty
                    {
                        throw new ArgumentException("invalid syntax of the formula");
                    }
                    if (operation.Count > 0)
                    {
                        if (operation.Peek().Equals("+") && value.Count >= 2&& !operation.Peek().Equals("("))//operates addition with 2 value in stack
                        {
                            operation.Pop();
                            int tempVal = value.Pop();
                            int tempVal2 = value.Pop();
                            value.Push(tempVal2+tempVal);
                            if(substrings[i].Equals("+"))
                            {
                                operation.Push("+");
                            }
                            if(substrings[i].Equals("-"))
                            {
                                operation.Push("-");
                            }
                            continue;
                        }
                        if (substrings[i].Equals("-") && value.Count >= 2&& !operation.Peek().Equals("("))//operates substraction with 2 value in stack
                        {
                            operation.Pop();
                            int tempVal = value.Pop();
                            int tempVal2 = value.Pop();

                            value.Push(tempVal2-tempVal);
                            if (substrings[i].Equals("+"))
                            {
                                operation.Push("+");
                            }

                            if (substrings[i].Equals("-"))
                            {
                                operation.Push("-");
                            }
                            continue;
                        }
                        if(substrings[i].Equals("+"))// if there is only 1 value in the stack
                        {
                            operation.Push("+");
                            continue;
                        }
                        if (substrings[i].Equals("-"))// if there is only 1 value in the stack
                        {
                            operation.Push("-");
                            continue;
                        }
                    }
                    if (substrings[i].Equals("+"))//if operation stack is empty
                    {
                        operation.Push("+");
                        continue;
                    }
                    if (substrings[i].Equals("-"))//if operation stack is empty
                    {
                        operation.Push("-");
                        continue;
                    }
                }
    
                if(substrings[i].Equals("*"))
                {
                    if(value.Count==0)
                    {
                        throw new ArgumentException("invalid syntax of the formula");
                    }
                    operation.Push("*");
                    continue;
                }
                if(substrings[i].Equals("/"))
                {
                    if (value.Count == 0)
                    {
                        throw new ArgumentException("invalid syntax of the formula");
                    }
                    operation.Push("/");
                    continue;
                }

                if (substrings[i].Equals("(")) // adds open parenthesis
                {
                    operation.Push("(");
                    continue;
                }


                if (substrings[i].Equals(")"))
                {
                    if (operation.Peek().Equals("+") || operation.Peek().Equals("-"))
                    {

                        if (operation.Peek().Equals("+") && value.Count >= 2)
                        {
                            operation.Pop();
                            int tempVal = value.Pop() + value.Pop();
                            value.Push(tempVal);
                        }
                        if (operation.Peek().Equals("-") && value.Count >= 2)
                        {
                            operation.Pop();
                            int tempVal = value.Pop();
                            int tempVal2 = value.Pop();
                            value.Push(tempVal2-tempVal);
                        } 
                    }

                    if (operation.Peek().Equals("(")) //pops the open parenthesis if operation is done inside the parenthesis
                    {
                        operation.Pop();
                        continue;
                    }
                    else //edge case
                    {
                        throw new ArgumentException("A ( isnt found where expected");
                    }
                }
                
            }

            if(operation.Count==0 && value.Count==1)
            {
                return value.Pop();
            }

            if(operation.Count==1&&value.Count>=2) //left with 1 operation and 2 value
            {
                if(operation.Peek().Equals("+"))
                {
                    int temp = value.Pop() + value.Pop();
                    value.Push(temp);
                    operation.Pop();
                }
                else 
                {
                    int tempVal = value.Pop();
                    int tempVal2 = value.Pop();
                    value.Push(tempVal2-tempVal);
                    operation.Pop();
                }
            }

            return value.Pop(); //pops the answer
        }
    }
}
