using System;
using System.Text.RegularExpressions;

namespace CalcFromString
{
    class Program
    {
        static void Main(string[] args) //todo problems with start from "-"
        {
            Console.WriteLine(new Parser().OpenParentheses("2*(10+(1+1))*3/6*5*4/2+3-10-5")); //108

            Console.WriteLine(Regex.Match("2.5+2+-2*-2+3.3+2",@"\d+\.*\d*").Value);
            Console.WriteLine(-2+-2);
            // var equation = "1+(2+(1+2)+2)+1";
            // var matchParenthesesRegex = Regex.Match(equation, @"\(.*\)");
            // if (matchParenthesesRegex.Success)
            // {
                // var value = matchParenthesesRegex.Value;
                // Console.WriteLine(equation.Replace(value,value.Substring(1,value.Length-2)));
            // }
        }
    }
}