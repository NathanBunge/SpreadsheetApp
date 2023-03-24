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
    public partial class ExpressionTree
    {
        private readonly ExpressionNode root;
        private static Dictionary<string, double> variableDict = new Dictionary<string, double>();
        OperatorNodeFactory opFactory = new OperatorNodeFactory();
        

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// Constructor for expression tree.
        /// </summary>
        /// <param name="expression">expression to add.</param>
        public ExpressionTree(string expression)
        {
            // First convert expression to postfix
            ShuntingYard yard = new ShuntingYard(this.opFactory.Precedences);
            string postfix = yard.ConvertToPostfix(expression);

            // Then create tree
            this.root = this.BuildTree(postfix);
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

        private ExpressionNode BuildTree(string postfix)
        {
            Stack<ExpressionNode> stack = new Stack<ExpressionNode>();

            string[] tokens = postfix.Split(' ');

            foreach (string token in tokens)
            {
                // If token is numeric, just push it
                if(double.TryParse(token, out double value))
                {
                    stack.Push(new NumericNode(value));
                }

                // If token is a variable, just push it
                else if (char.IsLetter(token[0]))
                {
                    return new VariableNode(token);
                }

                // Else it must be an oporator
                else if (char.TryParse(token, out var operatorChar))
                {
                    ExpressionNode right = stack.Pop();
                    ExpressionNode left = stack.Pop();
                    OperatorNode newNode = opFactory.CreateOperatorNode(operatorChar, left, right);
                    stack.Push(newNode);
                }
                else
                {
                    throw new Exception("Unknown token");
                }
            }

            // Return the root node.
            return stack.Pop();
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
            int opIndex = opFactory.FindOperatorIndex(expression);
            char op = expression[opIndex];

            // create left and rights sides
            string leftside = expression.Substring(0, opIndex);
            string rightside = expression.Substring(opIndex + 1);

            // first create left and right sides
            ExpressionNode left = this.Compile(leftside);
            ExpressionNode right = this.Compile(rightside);

            // Set the root to the oporator node using factory
            ExpressionNode opNode = opFactory.CreateOperatorNode(op, left, right);

            return opNode;

            // Old code
            /*
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
            */

        }


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

            private static char operatorType;

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
    }
}