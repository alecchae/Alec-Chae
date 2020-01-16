using System;


namespace Test_The_Evaluator_Console_App
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2+3*2-5/5", null));
        }
    }
}
