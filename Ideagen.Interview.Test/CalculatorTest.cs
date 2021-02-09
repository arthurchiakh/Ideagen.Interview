using NUnit.Framework;

namespace Ideagen.Interview.Test
{
    public class CalculatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        // Simple
        [TestCase("1 + 1", 2)]
        [TestCase("2 * 2", 4)]
        [TestCase("1 + 2 + 3", 6)]
        [TestCase("6 / 2", 3)]
        [TestCase("11 + 23", 34)]
        [TestCase("11.1 + 23", 34.1)]
        [TestCase("1 + 1 * 3", 4)]
        // Bracket
        [TestCase("( 11.5 + 15.4 ) + 10.1", 37)]
        [TestCase("23 - ( 29.3 - 12.5 )", 6.2)]
        // Nested Bracket
        [TestCase("10 - ( 2 + 3 * ( 7 - 5 ) )", 2)]
        // Power
        [TestCase("10 ^ 2", 100)]
        [TestCase("10 ^ ( 5 - 2 )", 1000)]
        // Modulus
        [TestCase("10 % 4", 2)]
        // Complex
        [TestCase("( 10 * 5 - 15 ) ^ 2 % 100", 25)]
        public void CalculationShouldEqualToAnswer(string input, double expected)
        {
            var result = Calculator.Calculate(input);
            Assert.AreEqual(result, expected);
        }
    }
}