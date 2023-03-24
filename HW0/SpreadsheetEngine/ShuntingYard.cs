namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection;



    /// <summary>
    /// Will probably move to it's file.
    /// </summary>
    internal class ShuntingYard
    {
        private Dictionary<char, int> precedence;

        internal ShuntingYard(Dictionary<char, int> operatorPrecedence)
        {
            this.precedence = operatorPrecedence;

        }

        public Dictionary<char, int> Precedence { get { return this.precedence; } }

        public string ConvertToPostfix(string infix)
        {
            var output = new List<string>();
            var operatorStack = new Stack<char>();

            foreach (char c in infix)
            {
                if (char.IsDigit(c))
                {
                    output.Add(c.ToString());
                }
                else if (IsOperator(c))
                {
                    while (operatorStack.Count > 0 && IsOperator(operatorStack.Peek()) && this.precedence[operatorStack.Peek()] >= this.precedence[c])
                    {
                        output.Add(operatorStack.Pop().ToString());
                    }
                    operatorStack.Push(c);
                }
                else if (IsLeftParenthesis(c))
                {
                    operatorStack.Push(c);
                }
                else if (IsRightParenthesis(c))
                {
                    while (operatorStack.Count > 0 && !IsLeftParenthesis(operatorStack.Peek()))
                    {
                        output.Add(operatorStack.Pop().ToString());
                    }
                    if (operatorStack.Count == 0 || !IsLeftParenthesis(operatorStack.Peek()))
                    {
                        throw new ArgumentException("Unmatched parentheses in infix expression.");
                    }
                    operatorStack.Pop();
                }
                else
                {
                    throw new ArgumentException("Invalid character in infix expression.");
                }
            }

            while (operatorStack.Count > 0)
            {
                if (IsLeftParenthesis(operatorStack.Peek()))
                {
                    throw new ArgumentException("Unmatched parentheses in infix expression.");
                }
                output.Add(operatorStack.Pop().ToString());
            }

            return string.Join(" ", output);
        }

        private bool IsOperator(char c)
        {
            return this.Precedence.ContainsKey(c);
        }

        private static bool IsLeftParenthesis(char c)
        {
            return c == '(';
        }

        private static bool IsRightParenthesis(char c)
        {
            return c == ')';
        }
    }

}
