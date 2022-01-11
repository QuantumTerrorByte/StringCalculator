using System;
using CalcFromString;
using Xunit;

namespace CalcFromStringTests
{
    public class Tests
    {
        [Fact]
        public void Can_Solve_Simple_Block()
        {
            var parser = new Parser();


            Assert.Equal(7, parser.SolveSimplePartOfEquation("2+5"));
            Assert.Equal(-3, parser.SolveSimplePartOfEquation("2-5"));
            Assert.Equal(2, parser.SolveSimplePartOfEquation("10/5"));
            Assert.Equal(10, parser.SolveSimplePartOfEquation("2*5"));

            Assert.Equal(-10, parser.SolveSimplePartOfEquation("-2*5"));
            Assert.Equal(-10, parser.SolveSimplePartOfEquation("2*-5"));
            Assert.Equal(10, parser.SolveSimplePartOfEquation("-2*-5"));
            Assert.Equal(-7, parser.SolveSimplePartOfEquation("-2+-5"));

            Assert.Equal(4.84m, parser.SolveSimplePartOfEquation("-2,2*-2,2"));
            Assert.Equal(-3.2m, parser.SolveSimplePartOfEquation("2,5+-5,7"));
        }
    }
}