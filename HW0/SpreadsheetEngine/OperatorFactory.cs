namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection;


    public partial class ExpressionTree
    {
        /// <summary>
        /// Operator Node Factory. Creates different types of operator node subclasses.
        /// Since it cannot exist without the expressionTree, it is defined inside the expressionTree class.
        /// </summary>
        public class OperatorNodeFactory
        {

            public OperatorNodeFactory()
            {
                TraverseAvailibleOperators((op, Type) => operators.Add(op, Type));
            }

            private Dictionary<char, Type> operators = new Dictionary<char, Type>() { };

            private delegate void OnOperator(char op, Type type);

            public OperatorNode CreateOperatorNode(char op, ExpressionNode left, ExpressionNode right)
            {
                if (this.operators.ContainsKey(op))
                {
                    object operatorObject = System.Activator.CreateInstance(this.operators[op], left, right);
                    if(operatorObject is OperatorNode)
                    {
                        return (OperatorNode)operatorObject;
                    }
                }

                throw new Exception("unhandled operator");

            }

            private void TraverseAvailibleOperators(OnOperator onOperator)
            {
                Type operatorNodeType = typeof(OperatorNode);
                foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    IEnumerable<Type> operatorType = assembly.GetTypes().Where(t => t.IsSubclassOf(operatorNodeType));
                    foreach(var type in operatorType)
                    {
                        PropertyInfo operatorField = type.GetProperty("OperatorType");
                        if(operatorField != null)
                        {
                            object value = operatorField.GetValue(type);

                            if(value is char)
                            {
                                char operatorSymbol = (char)value;

                                onOperator(operatorSymbol, type);
                            }
                        }
                    }
                }
            }

            /// <summary>
            /// Finds index of oporator. May take this outside class.
            /// </summary>
            /// <param name="str">expression string.</param>
            /// <returns>Index of oporator.</returns>
            public int FindOperatorIndex(string str)
            {
                int index = -1;
                foreach (char op in this.operators.Keys)
                {
                    int i = str.LastIndexOf(op);
                    if (i >=0)
                    {
                        return i;
                    }
                }
                throw new Exception("unhandled operator");
                return index;
            }



            /// <summary>
            /// Subclass for adding nodes.
            /// </summary>
            public class AddOperatorNode : OperatorNode
            {

                private static char operatorType = '+';

                public static char OperatorType
                {
                    get { return operatorType; }
                }

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
                private static char operatorType = '-';

                public static char OperatorType
                {
                    get { return operatorType; }
                }
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
                private static char operatorType = '*';

                public static char OperatorType
                {
                    get { return operatorType; }
                }
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
                private static char operatorType = '/';

                public static char OperatorType
                {
                    get { return operatorType; }
                }
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
