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
                TraverseAvailibleOperators((op, pres, Type) => { operators.Add(op, Type); precedences.Add(op, pres); });
            }

            private Dictionary<char, Type> operators = new Dictionary<char, Type>() { };
            private Dictionary<char, int> precedences = new Dictionary<char, int>() { };

            public Dictionary<char, int> Precedences
            { get { return this.precedences; } }

            private delegate void OnOperator(char op, int precedence, Type type);

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
                        PropertyInfo precedenceField = type.GetProperty("OperatorPrecedence");
                        if (operatorField != null && precedenceField != null)
                        {
                            object opValue = operatorField.GetValue(type);
                            object opPrec = precedenceField.GetValue(type);

                            if (opValue is char && opPrec is int)
                            {
                                char operatorSymbol = (char)opValue;
                                int operatorPrecedence = (int)opPrec;

                                onOperator(operatorSymbol, operatorPrecedence, type);
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
                public AddOperatorNode(ExpressionNode left, ExpressionNode right): base(left, right)
                {
                }

                private static char operatorType = '+';
                private static int operatorPrecedence = 2;

                public static char OperatorType
                {
                    get { return operatorType; }
                }
                public static int OperatorPrecedence
                {
                    get { return operatorPrecedence; }
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
                private static char operatorType = '-';
                private static int operatorPrecedence = 2;

                public static char OperatorType
                {
                    get { return operatorType; }
                }
                public static int OperatorPrecedence
                {
                    get { return operatorPrecedence; }
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

                private static char operatorType = '*';
                private static int operatorPrecedence = 3;

                public static char OperatorType
                {
                    get { return operatorType; }
                }
                public static int OperatorPrecedence
                {
                    get { return operatorPrecedence; }
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

                private static char operatorType = '/';
                private static int operatorPrecedence = 3;

                public static char OperatorType
                {
                    get { return operatorType; }
                }
                public static int OperatorPrecedence
                {
                    get { return operatorPrecedence; }
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
