using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FormulaEvaluator
{
    public class Evaluator
    {
        public delegate int Lookup(String variable_name);

        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            
            
            Stack<String> operation = new Stack<String>();
            Stack<int> value = new Stack<int>();

            for (int i = 0; i < substrings.Length; i++)
            {
                if (int.TryParse(substrings[i], out int convert))
                {
                    if(value.Count==0)
                    {
                        value.Push(convert);
                        continue;
                    }
                    if (operation.Count > 0)
                    {
                        if (operation.Peek().Equals("*"))
                        {
                            int popped = value.Pop();
                            int tempVal = convert * popped;
                            value.Push(tempVal);
                            operation.Pop();
                            continue;
                        }

                        if (operation.Peek().Equals("/"))
                        {
                            if (convert == 0)
                            {
                                throw new ArgumentException("Cannot divide zero");
                            }
                            int popped = value.Pop();
                            int tempVal = convert / popped;
                            value.Push(tempVal);
                            operation.Pop();
                            continue;
                        }
                        else
                        {
                            value.Push(convert);
                            continue;
                        }
                    }
                   


                }

                if (substrings[i].Equals("+")|| substrings[i].Equals("-"))
                {
                    if (operation.Count > 0)
                    {
                        if (operation.Peek().Equals("+") && value.Count >= 2)
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
                        if (substrings[i].Equals("-") && value.Count >= 2)
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
                        if(substrings[i].Equals("+"))
                        {
                            operation.Push("+");
                            continue;
                        }
                        if (substrings[i].Equals("-"))
                        {
                            operation.Push("-");
                            continue;
                        }
                    }
                    else
                    {
                        operation.Push("+");
                        continue;
                    }
                }
             
               
                
                if(substrings[i].Equals("*"))
                {
                    operation.Push("*");
                    continue;
                }
                if(substrings[i].Equals("/"))
                {
                    operation.Push("/");
                    continue;
                }

                if (substrings[i].Equals("("))
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
                            int tempVal = value.Pop() - value.Pop();
                            value.Push(tempVal);
                           
                        } 
                    }
                    
                    if (operation.Peek().Equals("("))
                    {
                        operation.Pop();
                        continue;
                    }
                    else
                    {
                        throw new ArgumentException("A ( isnt found where expected");
                    }
                    if(operation.Peek().Equals("*")|| operation.Peek().Equals("/"))
                    {
                        if (operation.Peek().Equals("*") && value.Count >= 2)
                        {
                            operation.Pop();
                            int tempVal = value.Pop() * value.Pop();
                            value.Push(tempVal);
                            continue;
                        }
                        if (operation.Peek().Equals("/") && value.Count >= 2)
                        {
                            operation.Pop();
                            int tempVal = value.Pop();
                            if(value.Peek()==0)
                            {
                                throw new ArgumentException("cannot be divided by zero");
                            }
                            int tempVal2 = value.Pop();
                            int tempResult = tempVal / tempVal2;
                            value.Push(tempResult);
                            continue;
                        }
                    }

                }
            }

            if(operation.Count==0 && value.Count==1)
            {
                return value.Pop();
            }

            if(operation.Count==1&&value.Count>=2)
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

            return value.Pop();
        }
    }
}
