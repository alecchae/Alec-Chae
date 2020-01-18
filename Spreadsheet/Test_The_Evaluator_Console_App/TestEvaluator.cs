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

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("5-3", null));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("5+5", null));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(2+3)*2", null));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(1+3)*2-(5+5)/5", null));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(1+3)*2-(15-5)/5", null));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("3*2", null)) ;
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("20*30", null));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("30/3", null));
            try
            {
                Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("A3/3", SimpleLookup));
            }
            catch(ArgumentException)
            {
                Console.WriteLine("The variable does not exist");
            }
            try
            {
                Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("A3/0", SimpleLookup));
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Could not be divided by 0");
            }
            try
            {
                Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("-5-", null));
            }
            catch (ArgumentException)
            {
                Console.WriteLine("invalid syntax of the formula");
            }

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(A1+3)/3", SimpleLookup));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(A1-A2)/3", SimpleLookup));
            
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
