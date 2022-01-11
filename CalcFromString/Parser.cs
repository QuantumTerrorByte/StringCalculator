using System;
using System.Text.RegularExpressions;

namespace CalcFromString
{
    public class Parser
    {
        public string innerParentheses { get; set; } = @"\(\w*[^\(\)]\w*\)";
        public string outerParentheses { get; set; } = @"\(.*\)";
        private int counter = 0;

        public string OpenParentheses(string equation) //  2*(10+(1+1))*3/6*5*4/2+3-10-5 //108
        {
            Console.WriteLine(++counter);
            var matchParenthesesRegex = Regex.Match(equation,innerParentheses);
            if (matchParenthesesRegex.Success)
            {
                Console.WriteLine();
                var expressionValue = matchParenthesesRegex.Value;
                var expValWithoutParenthesis = expressionValue
                    .Substring(1, expressionValue.Length - 2);
                equation = equation.Replace(expressionValue,
                    SolveEquation(expValWithoutParenthesis));
                equation = OpenParentheses(equation);
            }

            return SolveEquation(equation);
        }

        public string SolveEquation(string equation) //  (2+2*3/6)
        {
            // var startIndex = 0;
            // var endIndex = 0;
            string partOfEquationResult;

            var multiplicationOrDivisionBlock = Regex.Match(equation, @"\d*[\*|\/]\d*");
            if (multiplicationOrDivisionBlock.Success)
            {
                partOfEquationResult =
                    SolveSimplePartOfEquation(multiplicationOrDivisionBlock.Value).ToString();
                // startIndex = singleBlockMultiplicationOrDivision.Index;
                // endIndex = startIndex + singleBlockMultiplicationOrDivision.Length;
                equation = equation.Replace(multiplicationOrDivisionBlock.Value, partOfEquationResult);
                equation = SolveEquation(equation);
            }

            var plusOrSubtractBlock = Regex.Match(equation, @"\d*[\+|\-]\d*");
            if (plusOrSubtractBlock.Success)
            {
                partOfEquationResult =
                    SolveSimplePartOfEquation(plusOrSubtractBlock.Value).ToString();
                // startIndex = singleBlockMatchPlusOrSubtract.Index;
                // endIndex = startIndex + singleBlockMatchPlusOrSubtract.Length;
                equation = equation.Replace(plusOrSubtractBlock.Value, partOfEquationResult);
                equation = SolveEquation(equation);
            }


            return equation;
        }

        private int SolveSimplePartOfEquation(string equation)
        {
            var digits = Regex.Matches(equation, @"\d+");
            var first = Int32.Parse(digits[0].Value);
            var second = Int32.Parse(digits[1].Value);
            var oper = Regex.Match(equation, @"\+|\-|\*|\/");
            return oper.Value switch
            {
                "+" => (first + second),
                "-" => (first - second),
                "*" => (first * second),
                "/" => (first / second),
                _ => throw new ArgumentException()
            };
        }
    }
}