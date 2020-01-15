using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Test_The_Evaluator_Console_App
{
    class Program
    {
        public delegate int Lookup(String variable_name);

        public static int Evaluate(String expression, Lookup variableEvaluator)
        {

            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");
            Stack operation = new Stack();
            Stack value = new Stack();




            for(int i =0; i<substrings.Length; i++)
            {
                if(substrings[i].Equals("("))
                {
                    int temp = i+1;
                    while(!substrings[temp].Equals(")"))
                    {
                        if(!substrings[temp].Equals("(-)|(\\+)|(\\*)|(/)"))
                        { 
                            value.Push(substrings[temp]);
                        }
                        if(substrings[temp].Equals("(\\*)|(/)"))
                            operation.Push(substrings[temp]);
                        

                        temp++;
                    }
                    i = temp + 1;
                }
            }

            return 0;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
