using NUnit.Framework;
using SpreadsheetEngine;

namespace TestExpressionTree
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestNumericNodeEvaluate()
        {
            string expression = "1+2";
            var tree = new ExpressionTree(expression);
            var result = tree.Evaluate();
            
            Assert.AreEqual(3, result);
        }
    }
}