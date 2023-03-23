namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public partial class ExpressionTree
    {
        /// <summary>
        /// Operator Node Factory. Creates different types of operator node subclasses.
        /// Since it cannot exist without the expressionTree, it is defined inside the expressionTree class.
        /// </summary>
        public static class OperatorNodeFactory
        {

            public static ExpressionTree.OperatorNode CreateOperatorNode(char op,
                ExpressionNode left,
                ExpressionNode right)
            {
                switch (op)
                {
                    case '+':
                        return new AddOperatorNode(left, right);
                    case '-':
                        return new SubtractOperatorNode(left, right);
                    case '*':
                        return new MultiplyOperatorNode(left, right);
                    case '/':
                        return new DivideOperatorNode(left, right);
                    default:
                        throw new ArgumentException($"Invalid operator: {op}");
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

}
