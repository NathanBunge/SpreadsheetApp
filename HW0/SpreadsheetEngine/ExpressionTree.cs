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
        private Dictionary<string, double> variableDict = new Dictionary<string, double>();

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
        /// sets variable name, adding to dict.
        /// </summary>
        /// <param name="variableName">name of variable to set.</param>
        /// <param name="variableValue">value of variable to set.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variableDict.Add(variableName, variableValue);
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

            // If states with a letter, lookup in dictionary
            // TODO: 

            string leftside = expression.Substring(0,1);
            string rightside = expression.Substring(2);

            char op = expression[1];

            if (op == '+')
            {
                return new AddOperatorNode(this.Compile(leftside), this.Compile(rightside));
            }

            if (op == '-')
            {
                return new AddOperatorNode(this.Compile(leftside), this.Compile(rightside));
            }

            if (op == '*')
            {
                return new AddOperatorNode(this.Compile(leftside), this.Compile(rightside));
            }

            if (op == '/')
            {
                return new AddOperatorNode(this.Compile(leftside), this.Compile(rightside));
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