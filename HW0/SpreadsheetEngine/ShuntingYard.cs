// <copyright file="ShuntingYard.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestExpressionTree")]

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Will probably move to it's file.
    /// </summary>
    internal class ShuntingYard
    {
        private Dictionary<char, int> precedence;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShuntingYard"/> class.
        /// Algorithm.
        /// </summary>
        /// <param name="operatorPrecedence">precedence dictionary. </param>
        internal ShuntingYard(Dictionary<char, int> operatorPrecedence)
        {
            this.precedence = operatorPrecedence;
        }

        /// <summary>
        /// Gets Dictinoary of predeence.
        /// </summary>
        public Dictionary<char, int> Precedence
        {
            get { return this.precedence; }
        }

        /// <summary>
        /// Converts infix to postfix expression.
        /// </summary>
        /// <param name="infix">input string.</param>
        /// <returns>postfix string.</returns>
        /// <exception cref="ArgumentException">exception.</exception>
        public string ConvertToPostfix(string infix)
        {
            var output = new List<string>();
            var operatorStack = new Stack<char>();
            var currentToken = string.Empty;

            foreach (char c in infix)
            {
                if (char.IsLetter(c))
                {
                    currentToken += c;
                }
                else if (char.IsDigit(c))
                {
                    currentToken += c;
                }
                else if (this.IsOperator(c))
                {
                    if (currentToken != string.Empty)
                    {
                        output.Add(currentToken);
                        currentToken = string.Empty;
                    }

                    while (operatorStack.Count > 0 && this.IsOperator(operatorStack.Peek()) && this.Precedence[operatorStack.Peek()] >= this.Precedence[c])
                    {
                        output.Add(operatorStack.Pop().ToString());
                    }

                    operatorStack.Push(c);
                }
                else if (IsLeftParenthesis(c))
                {
                    if (currentToken != string.Empty)
                    {
                        output.Add(currentToken);
                        currentToken = string.Empty;
                    }

                    operatorStack.Push(c);
                }
                else if (IsRightParenthesis(c))
                {
                    if (currentToken != string.Empty)
                    {
                        output.Add(currentToken);
                        currentToken = string.Empty;
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

            if (currentToken != string.Empty)
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

        private static bool IsLeftParenthesis(char c)
        {
            return c == '(';
        }

        private static bool IsRightParenthesis(char c)
        {
            return c == ')';
        }

        private bool IsOperator(char c)
        {
            return this.Precedence.ContainsKey(c);
        }
    }
}