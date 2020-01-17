using System;


namespace Test_The_Evaluator_Console_App
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("(1+3)*2-(5+5)/5", null));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2+3", null));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("3*2", null));
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("5/5", null));
            
        }
    }
}
