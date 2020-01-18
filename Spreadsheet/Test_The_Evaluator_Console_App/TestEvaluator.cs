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
///    [Test Evaluates Formula in a priority order] 
/// </summary>
using System;


namespace Test_The_Evaluator_Console_App
{
    class TestEvaluator
    {
        
        static void Main(string[] args)
        {

            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("5-3", null));//2
            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("5+5", null));//10
            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("(2+3)*2", null));//10
            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("(1+3)*2-(5+5)/5", null));//6
            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("(1+3)*2-(15-5)/5", null));//6
            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("3*2", null)) ;//6
            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("20*30", null));//600
            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("30/3", null));//10
            try
            {
                Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("A3/3", SimpleLookup));//variable does not exist
            }
            catch(ArgumentException)
            {
                Console.WriteLine("The variable does not exist");
            }
            try
            {
                Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("A2/0", SimpleLookup)); //divison by 0
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Could not be divided by 0");
            }
            try
            {
                Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("-5-", null)); //sytanx error
            }
            catch (ArgumentException)
            {
                Console.WriteLine("invalid syntax of the formula");
            }

            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("(A1+3)/3", SimpleLookup));//delegate 4
            Console.WriteLine(FormulaEvaluator.FormulaEvaluator.Evaluate("(A1-A2)/3", SimpleLookup));//delegate 1
            
        }
        public static int SimpleLookup(string v)
        {
            if (v.Equals("A1"))
                return 9;
            if (v.Equals("A2"))
                return 6;
            
            throw new ArgumentException("The variable does not exist");
            
        }
    }
}
