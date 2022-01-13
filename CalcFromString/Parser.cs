using System;
using System.Text.RegularExpressions;

namespace CalcFromString
{
    public class Parser //todo con with format provider
    {
        private string InnerParentheses { get; set; } = @"\(\w*[^\(\)]\w*\)";
        private string OuterParentheses { get; set; } = @"\(.*\)";
        private string MultiplyAndDivision { get; set; } = @"\d+\,*\d*[\*|\/]\-*\d+\,*\d*";
        private string AdditionAndSubtraction { get; set; } = @"^(\-*\d+\,*\d*[\+|\-]\d+\,*\d*)|(\d+\,*\d*[\+|\-]\d+\,*\d*)";
        
        private int counter = 0;

        public string OpenParentheses(string equation) //  -2*(10+(1+1))*3/6*5*4/2+3-10-5 //-132
        {
            var matchParenthesesRegex = Regex.Match(equation, InnerParentheses);
            if (matchParenthesesRegex.Success)
            {
                var expressionValue = matchParenthesesRegex.Value;
                var expValWithoutParenthesis = expressionValue
                    .Substring(1, expressionValue.Length - 2);
                equation = equation.Replace(expressionValue,
                    SolveComplexEquation(expValWithoutParenthesis));
                equation = OpenParentheses(equation);
            }

            return SolveComplexEquation(equation);
        }

        public string SolveComplexEquation(string equation) 
        {
            string partOfEquationResult;
            equation = SeparatePartOfEquation(equation,@"\d+\,*\d*[\*|\/]\-*\d+\,*\d*");
            equation = SeparatePartOfEquation(equation,@"^(\-*\d+\,*\d*[\+|\-]\d+\,*\d*)|(\d+\,*\d*[\+|\-]\d+\,*\d*)");
            return equation;
        }


        public string SeparatePartOfEquation(string equation, string regex)
        {
            string partOfEquationResult;
            var multiplicationOrDivisionBlock = Regex.Match(equation, regex);
            if (multiplicationOrDivisionBlock.Success)
            {
                partOfEquationResult =
                    SolveSimplePartOfEquation(multiplicationOrDivisionBlock.Value).ToString();
                equation = equation.Replace(multiplicationOrDivisionBlock.Value, partOfEquationResult);
                equation = SolveComplexEquation(equation);
            }
            return equation;
        }

        public decimal SolveSimplePartOfEquation(string equation) // todo optimize performance(separate decimal and int matching and solving) 
        {
            var digits = Regex.Matches(equation, @"-*\d+\,*\d*");
            if (digits.Count != 2)
            {
                throw new ArgumentException("in: " + equation);
            }

            var first = Decimal.Parse(digits[0].Value);
            var second = Decimal.Parse(digits[1].Value);
            var oper = Regex.Match(equation, @"\*|\/|\+");
            if (!oper.Success)
            {
                oper = Regex.Match(equation, @"\-");
            }


            return oper.Value switch //todo what is several case for one answer with arrow syntax?
            {
                "+" => checked(first + second),
                "-" => checked(first + second),
                "*" => checked(first * second),
                "/" => checked(first / second),
                _ => throw new ArgumentException("in: " + equation)
            };
        }
    }
}


// public string SolveEquation(string equation) //  (2+2*3/6) todo extract method with regex args
// {
//     // var startIndex = 0;
//     // var endIndex = 0;
//     string partOfEquationResult;
//
//     var multiplicationOrDivisionBlock = Regex.Match(equation, @"\d*[\*|\/]\d*");
//     if (multiplicationOrDivisionBlock.Success)
//     {
//         partOfEquationResult =
//             SolveSimplePartOfEquation(multiplicationOrDivisionBlock.Value).ToString();
//         // startIndex = singleBlockMultiplicationOrDivision.Index;
//         // endIndex = startIndex + singleBlockMultiplicationOrDivision.Length;
//         equation = equation.Replace(multiplicationOrDivisionBlock.Value, partOfEquationResult);
//         equation = SolveEquation(equation);
//     }
//
//     var plusOrSubtractBlock = Regex.Match(equation, @"\d*[\+|\-]\d*");
//     if (plusOrSubtractBlock.Success)
//     {
//         partOfEquationResult =
//             SolveSimplePartOfEquation(plusOrSubtractBlock.Value).ToString();
//         // startIndex = singleBlockMatchPlusOrSubtract.Index;
//         // endIndex = startIndex + singleBlockMatchPlusOrSubtract.Length;
//         equation = equation.Replace(plusOrSubtractBlock.Value, partOfEquationResult);
//         equation = SolveEquation(equation);
//     }
//
//
//     return equation;
// }