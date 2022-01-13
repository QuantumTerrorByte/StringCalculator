using System;
using System.Text.RegularExpressions;

namespace CalcFromString
{
    class Program
    {
        static void Main(string[] args) //todo problems with start from "-"
        {
            Console.WriteLine(new Parser().OpenParentheses("-2*(10+(1+1))*3/6*5*4/2+3-10-5")); //108
        }

        private static void TempTest()
        {
            foreach (var match in Regex.Matches("-5+2-2*-2,2+3,3+2",@"^(\-*\d+\,*\d*[\+|\-]\d+\,*\d*)|(\d+\,*\d*[\+|\-]\d+\,*\d*)"))
            {
                Console.Write(match+"  ");
            }
            foreach (var match in Regex.Matches("-5+2-2*-2,2+3,3+2",@"\d+\,*\d*[\+|\-]\d+\,*\d*"))
            {
                Console.Write(match+"  ");
            }
        }
    }
}