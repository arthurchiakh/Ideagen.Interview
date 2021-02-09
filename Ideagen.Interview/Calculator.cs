using System;
using System.Collections.Generic;

namespace Ideagen.Interview
{
    public class Calculator
    {
        private static readonly Dictionary<string, (int, Func<decimal, decimal, decimal>)> _operators =
            new Dictionary<string, (int, Func<decimal, decimal, decimal>)>
            {
                {"+", (1, (a, b) => a + b)},
                {"-", (1, (a, b) => a - b)},
                {"*", (2, (a, b) => a * b)},
                {"/", (2, (a, b) => a / b)},
                {"^", (3, (a, b) => (decimal) Math.Pow((double) a, (double) b))},
                {"%", (2, (a, b) => a % b)}
            };

        public static double Calculate(string sum)
        {
            //Your code starts here

            // Split into elements
            var split = sum.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            // Prepare reverse polish notation (postfix notation)
            var rpn = new Queue<string>();
            var operators = new Stack<string>();

            foreach (var c in split)
            {
                if (double.TryParse(c, out var number)) // Number
                    rpn.Enqueue(c);
                else if (c == "(") // Opening bracket
                    operators.Push(c);
                else if (c == ")") // Closing bracket
                {
                    // Pop all to output until "(" is found
                    while (operators.Peek() != "(")
                        rpn.Enqueue(operators.Pop());

                    operators.Pop(); // remove "(" from stack
                }
                else if (_operators.ContainsKey(c)) // Operator
                {
                    if (operators.Count > 0 &&
                        operators.Peek() != "(" // Ignore if top of the stack is "("
                    )
                    {
                        // Get and compare precedence of current and last operators
                        var lastOperatorPrecedence = _operators[operators.Peek()].Item1;
                        var currentOperatorPrecendence = _operators[c].Item1;

                        if (currentOperatorPrecendence > lastOperatorPrecedence)
                            operators.Push(c);
                        else
                        {
                            rpn.Enqueue(operators.Pop());
                            operators.Push(c);
                        }
                    }
                    else
                        operators.Push(c);
                }
            }

            // pop all the remaining operator to rpn
            while (operators.Count > 0)
                rpn.Enqueue(operators.Pop());

            // Evaluation
            var calcStack = new Stack<decimal>();

            while (rpn.Count > 0)
            {
                var value = rpn.Dequeue();
                if (decimal.TryParse(value, out var number))
                    calcStack.Push(number);
                else
                {
                    var b = calcStack.Pop();
                    var a = calcStack.Pop();

                    calcStack.Push(_operators[value].Item2(a, b));
                }
            }

            return calcStack.Count == 1 ? (double) calcStack.Pop() : throw new ArgumentException("Invalid input.");
        }
    }
}