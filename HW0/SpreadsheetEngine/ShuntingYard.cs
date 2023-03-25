using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("TestExpressionTree")]

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
            var currentToken = "";

            foreach (char c in infix)
            {
                if (Char.IsLetter(c))
                {
                    currentToken += c;
                }
                else if (Char.IsDigit(c))
                {
                    currentToken += c;
                }
                else if (IsOperator(c))
                {
                    if (currentToken != "")
                    {
                        output.Add(currentToken);
                        currentToken = "";
                    }
                    while (operatorStack.Count > 0 && IsOperator(operatorStack.Peek()) && Precedence[operatorStack.Peek()] >= Precedence[c])
                    {
                        output.Add(operatorStack.Pop().ToString());
                    }
                    operatorStack.Push(c);
                }
                else if (IsLeftParenthesis(c))
                {
                    if (currentToken != "")
                    {
                        output.Add(currentToken);
                        currentToken = "";
                    }
                    operatorStack.Push(c);
                }
                else if (IsRightParenthesis(c))
                {
                    if (currentToken != "")
                    {
                        output.Add(currentToken);
                        currentToken = "";
                    }
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

            if (currentToken != "")
            {
                output.Add(currentToken);
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
