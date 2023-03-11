namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to contain expression tree and nodes.
    /// </summary>
    public class ExpressionTree
    {
        private readonly ExpressionNode root;
        private static Dictionary<string, double> variableDict = new Dictionary<string, double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// Constructor for expression tree.
        /// </summary>
        /// <param name="expression">expression to add.</param>
        public ExpressionTree(string expression)
        {
            this.root = this.Compile(expression);
        }


        /// <summary>
        /// Check is sting is alphanumeric. Maybe take this outside class.
        /// </summary>
        /// <param name="str">expression to check.</param>
        /// <returns>true if is all letters or numbrs.</returns>
        public static bool IsAllAlphaNumeric(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Finds index of oporator. May take this outside class.
        /// </summary>
        /// <param name="str">expression string.</param>
        /// <returns>Index of oporator.</returns>
        public static int FindOperatorIndex(string str)
        {
            char[] operators = { '+', '-', '*', '/' };
            int index = -1;
            foreach (char op in operators)
            {
                int i = str.IndexOf(op);
                if (i >= 0 && (index == -1 || i < index))
                {
                    index = i;
                }
            }

            return index;
        }


        /// <summary>
        /// sets variable name, adding to dict.
        /// </summary>
        /// <param name="variableName">name of variable to set.</param>
        /// <param name="variableValue">value of variable to set.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            variableDict[variableName] = variableValue;
        }

        /// <summary>
        /// Evaluate the tree.
        /// </summary>
        /// <returns>value of evaluation</returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }



        private ExpressionNode Compile(string expression)
        {
            // Since we assume ony one oporator at a time, we can just go left to right

            // Return Numeric node if just a vallue
            if (double.TryParse(expression, out double value))
            {
                return new NumericNode(value);
            }

            // If starts with letter, check if just variable
            if (char.IsLetter(expression[0]))
            {
                // If only alphanumeric, its a variable
                if (IsAllAlphaNumeric(expression))
                {
                    return new VariableNode(expression);
                }
            }
            // Find first instance of oporation
            int opIndex = FindOperatorIndex(expression);
            char op = expression[opIndex];

            //create left and rights sides
            string leftside = expression.Substring(0, opIndex);
            string rightside = expression.Substring(opIndex+1);

            if (op == '+')
            {
                return new AddOperatorNode(this.Compile(leftside), this.Compile(rightside));
            }

            if (op == '-')
            {
                return new SubtractOperatorNode(this.Compile(leftside), this.Compile(rightside));
            }

            if (op == '*')
            {
                return new MultiplyOperatorNode(this.Compile(leftside), this.Compile(rightside));
            }

            if (op == '/')
            {
                return new DivideOperatorNode(this.Compile(leftside), this.Compile(rightside));
            }

            // invalid oporaters
            throw new ArgumentException("Invalid operator: " + expression[1]);
        }

        // since nodes don't exist outside the tree, we define them here

        /// <summary>
        /// Base abstract class for nodes.
        /// </summary>
        public abstract class ExpressionNode
        {
            /// <summary>
            /// Each sublcass will have a definition.
            /// </summary>
            /// <returns>Value after the evaluation.</returns>
            public abstract double Evaluate();
        }

        /// <summary>
        /// Subclass for nodes with basic numbers.
        /// </summary>
        public class NumericNode : ExpressionNode
        {
            private double value;

            /// <summary>
            /// Initializes a new instance of the <see cref="NumericNode"/> class.
            /// Constructor with vallue.
            /// </summary>
            /// <param name="value">numerical value of node.</param>
            public NumericNode(double value)
            {
                this.value = value;
            }

            /// <summary>
            /// return the value of the node.
            /// </summary>
            /// <returns>numerical value.</returns>
            public override double Evaluate()
            {
                return this.value;
            }
        }

        /// <summary>
        /// Subclass for nodes with basic numbers.
        /// </summary>
        public class VariableNode : ExpressionNode
        {
            private string variableName;

            /// <summary>
            /// Initializes a new instance of the <see cref="VariableNode"/> class.
            /// Constructor with vallue.
            /// </summary>
            /// <param name="value">name of variable.</param>
            public VariableNode(string value)
            {
                this.variableName = value;
            }

            /// <summary>
            /// return the value of the node.
            /// </summary>
            /// <returns>numerical value.</returns>
            public override double Evaluate()
            {
                // if variable is in dictionary, return the value.
                if (variableDict.ContainsKey(this.variableName))
                {
                    return variableDict[this.variableName];
                }

                // else return error.
                else
                {
                    throw new KeyNotFoundException("Undefined variable: " + this.variableName);
                }

            }
        }

        /// <summary>
        /// Subclass for oporation nodes.
        /// </summary>
        public abstract class OperatorNode : ExpressionNode
        {
            private ExpressionNode left;
            private ExpressionNode right;

            /// <summary>
            /// Gets for left node.
            /// </summary>
            protected ExpressionNode Left
            {
                get { return this.left; }
            }

            /// <summary>
            /// Gets for right node.
            /// </summary>
            protected ExpressionNode Right
            {
                get { return this.right; }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="OperatorNode"/> class.
            /// Constructor
            /// </summary>
            /// <param name="left">left node.</param>
            /// <param name="right">right node.</param>
            public OperatorNode(ExpressionNode left, ExpressionNode right)
            {
                this.left = left;
                this.right = right;
            }
        }

        /// <summary>
        /// Subclass for adding nodes.
        /// </summary>
        public class AddOperatorNode : OperatorNode
        {
            public AddOperatorNode(ExpressionNode left, ExpressionNode right)
                : base(left, right)
            {
            }

            /// <summary>
            /// Adds both sides.
            /// </summary>
            /// <returns>value after adding.</returns>
            public override double Evaluate()
            {
                return this.Left.Evaluate() + this.Right.Evaluate();
            }
        }

        /// <summary>
        /// Subclass for subtracting nodes.
        /// </summary>
        public class SubtractOperatorNode : OperatorNode
        {
            public SubtractOperatorNode(ExpressionNode left, ExpressionNode right)
                : base(left, right)
            {
            }

            /// <summary>
            /// Subtract both sides.
            /// </summary>
            /// <returns>value after subtracting.</returns>
            public override double Evaluate()
            {
                return this.Left.Evaluate() - this.Right.Evaluate();
            }
        }

        /// <summary>
        /// Subclass for multiplication.
        /// </summary>
        public class MultiplyOperatorNode : OperatorNode
        {
            public MultiplyOperatorNode(ExpressionNode left, ExpressionNode right)
                : base(left, right)
            {
            }

            /// <summary>
            /// Multiply each side
            /// </summary>
            /// <returns>value after multiplication</returns>
            public override double Evaluate()
            {
                return this.Left.Evaluate() * this.Right.Evaluate();
            }
        }

        /// <summary>
        /// Subclass for dividing.
        /// </summary>
        public class DivideOperatorNode : OperatorNode
        {
            public DivideOperatorNode(ExpressionNode left, ExpressionNode right)
                : base(left, right)
            {
            }

            /// <summary>
            /// Divide one said from another.
            /// </summary>
            /// <returns>value after eval.</returns>
            public override double Evaluate()
            {
                return this.Left.Evaluate() / this.Right.Evaluate();
            }
        }

    }
}