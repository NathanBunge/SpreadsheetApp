using NUnit.Framework;
using SpreadsheetEngine;
using System;
using System.Collections.Generic;

namespace TestExpressionTree
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        /// <summary>
        /// Testing addition 
        /// </summary>
        [Test]
        public void TestAdditionNodeEvaluate()
        {
            string expression = "1+2";
            var tree = new ExpressionTree(expression);
            var result = tree.Evaluate();
            
            Assert.AreEqual(3, result);
        }
        [Test]
        public void TestAdditionNodeEvaluateBoundary()
        {
            string expression = "0+0";
            var tree = new ExpressionTree(expression);
            var result = tree.Evaluate();

            Assert.AreEqual(0, result);
        }
        [Test]
        public void TestAdditionNodeEvaluateException()
        {
            string expression = (double.MaxValue-1).ToString() + "+9";
            var tree = new ExpressionTree(expression);
            Assert.Throws<System.OverflowException>(() => tree.Evaluate());
        }

        /// <summary>
        /// Testing Subtraction
        /// </summary>
        [Test]
        public void TestSubtractionNodeEvaluate()
        {
            string expression = "1-2";
            var tree = new ExpressionTree(expression);
            var result = tree.Evaluate();

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void TestSubtractionNodeEvaluateBoundary()
        {
            string expression = "0-0";
            var tree = new ExpressionTree(expression);
            var result = tree.Evaluate();

            Assert.AreEqual(0, result);
        }
        [Test]
        public void TestSubtractionNodeEvaluateException()
        {
            string expression = double.MinValue.ToString() + "-9";
            var tree = new ExpressionTree(expression);
            Assert.Throws<System.OverflowException>(() => tree.Evaluate());
        }

        /// <summary>
        /// Test Multiplication
        /// </summary>
        [Test]
        public void TestMultiplicationNodeEvaluate()
        {
            string expression = "5*6";
            var tree = new ExpressionTree(expression);
            var result = tree.Evaluate();

            Assert.AreEqual(30, result);
        }

        [Test]
        public void TestMultiplicationNodeEvaluateBoundary()
        {
            string expression = "9999*0";
            var tree = new ExpressionTree(expression);
            var result = tree.Evaluate();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestMultiplicationNodeEvaluateException()
        {
            string expression = double.MaxValue.ToString() + "*9";
            var tree = new ExpressionTree(expression);
            Assert.Throws<System.OverflowException>(() => tree.Evaluate());
        }


        /// <summary>
        /// Test divide
        /// </summary>
        [Test]
        public void TestDivisionNodeEvaluate()
        {
            string expression = "5/2";
            var tree = new ExpressionTree(expression);
            var result = tree.Evaluate();

            Assert.AreEqual(2.5, result);
        }

        [Test]
        public void TestDivisionNodeEvaluateBoundary()
        {
            string expression = "0/7";
            var tree = new ExpressionTree(expression);
            var result = tree.Evaluate();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestDivisionNodeEvaluateException()
        {
            string expression = "10/0";
            var tree = new ExpressionTree(expression);
            Assert.Throws<DivideByZeroException>(() => tree.Evaluate());
        }



        [Test]
        public void TestVariableNodeEvaluate()
        {
            string expression = "A";
            var tree = new ExpressionTree(expression);
            tree.SetVariable("A", 5);
            var result = tree.Evaluate();

            Assert.AreEqual(5, result);
        }

        [Test]
        public void TestVariableNodeAddEvaluate()
        {
            string expression = "A+A";
            var tree = new ExpressionTree(expression);
            tree.SetVariable("A", 5);
            var result = tree.Evaluate();

            Assert.AreEqual(10, result);
        }
        [Test]
        public void TestVariableNodeAddNumEvaluate()
        {
            string expression = "A+6";
            var tree = new ExpressionTree(expression);
            tree.SetVariable("A", 5);
            var result = tree.Evaluate();

            Assert.AreEqual(11, result);
        }
        [Test]
        public void TestVariableNodeEvaluateException()
        {
            string expression = "ZZZ";
            var tree = new ExpressionTree(expression);
            tree.SetVariable("A", 5);
            Assert.Throws<KeyNotFoundException>(() => tree.Evaluate());


        }

    }
}