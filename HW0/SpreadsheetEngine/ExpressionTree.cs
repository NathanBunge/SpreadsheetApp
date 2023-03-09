using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{

    public class ExpressionTree
    {
        private readonly ExpressionNode _root;
        private Dictionary<string, double> variableDict = new Dictionary<string, double>();

        public ExpressionTree(string expression)
        {
            _root = Compile(expression);
        }

        private ExpressionNode Compile(string expression)
        {
            // Since we assume ony one oporator at a time, we can just go left to right

            // Return Numeric node if just a vallue
            if (double.TryParse(expression, out double value))
            {
                return new NumericNode(value);
            }

            string leftside = expression.Substring(0,1);
            string rightside = expression.Substring(2);

            char op = expression[1];


            if (op == '+')
            {
                return new AddOperatorNode(Compile(leftside), Compile(rightside));
            }

            if (op == '-')
            {
                return new AddOperatorNode(Compile(leftside), Compile(rightside));
            }

            if (op == '*')
            {
                return new AddOperatorNode(Compile(leftside), Compile(rightside));
            }

            if (op == '/')
            {
                return new AddOperatorNode(Compile(leftside), Compile(rightside));
            }

            // invalid oporaters
            throw new ArgumentException("Invalid operator: " + expression[1]);
        }

        public void SetVariable(string variableName, double variableValue)
        {

        }

        public double Evaluate()
        {

            return this._root.Evaluate();
        }

        // since nodes don't exist outside the tree, we define them here
        public abstract class ExpressionNode
        {
            public abstract double Evaluate();
        }

        public class NumericNode : ExpressionNode
        {
            private double _value;

            public NumericNode(double value)
            {
                _value = value;
            }

            public override double Evaluate()
            {
                return _value;
            }
        }

        public abstract class OperatorNode : ExpressionNode
        {
            protected ExpressionNode _left;
            protected ExpressionNode _right;

            public OperatorNode(ExpressionNode left, ExpressionNode right)
            {
                _left = left;
                _right = right;
            }
        }

        public class AddOperatorNode : OperatorNode
        {
            public AddOperatorNode(ExpressionNode left, ExpressionNode right)
                : base(left, right)
            {
            }

            public override double Evaluate()
            {
                return _left.Evaluate() + _right.Evaluate();
            }
        }

        public class SubtractOperatorNode : OperatorNode
        {
            public SubtractOperatorNode(ExpressionNode left, ExpressionNode right)
                : base(left, right)
            {
            }

            public override double Evaluate()
            {
                return _left.Evaluate() - _right.Evaluate();
            }
        }

        public class MultiplyOperatorNode : OperatorNode
        {
            public MultiplyOperatorNode(ExpressionNode left, ExpressionNode right)
                : base(left, right)
            {
            }

            public override double Evaluate()
            {
                return _left.Evaluate() * _right.Evaluate();
            }
        }

        public class DivideOperatorNode : OperatorNode
        {
            public DivideOperatorNode(ExpressionNode left, ExpressionNode right)
                : base(left, right)
            {
            }

            public override double Evaluate()
            {
                return _left.Evaluate() / _right.Evaluate();
            }
        }
    }
}