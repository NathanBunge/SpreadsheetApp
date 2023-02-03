using NUnit.Framework;
using DistinctNum;
using System.Collections.Generic;

namespace TestDistinct
{
    public class Tests
    {
        List<int> l0 = new List<int>() { };
        List<int> l1 = new List<int>() { 1, 1 };
        List<int> l1b = new List<int>() { 1};
        List<int> l2 = new List<int>() { 1, 2, 2, 1 };
        List<int> l5 = new List<int>() { 1, 3, 2, 3, 1, 3, 2, 4, 5 };


        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests 5 differnt lists on the hash distinct function to make sure the count is correctly returned.
        /// <returns> True if passed, flase is failed</returns>
        /// <param>none</param>
        /// <exception>None</exception>
        /// </summary>
        /// 
        [Test]
        public void TestHashZero()
        {
            Assert.AreEqual(Distinct.hashDistinct(l0), 0);
        }
        [Test]
        public void TestHashOne()
        {
            Assert.AreEqual(Distinct.hashDistinct(l1), 1);
        }
        [Test]
        public void TestHashOneb()
        {
            Assert.AreEqual(Distinct.hashDistinct(l1b), 1);
        }

        [Test]
        public void TestHashTwo()
        {
            Assert.AreEqual(Distinct.hashDistinct(l2), 2);
        }
        [Test]
        public void TestHashFive()
        {
            Assert.AreEqual(Distinct.hashDistinct(l5), 5);
        }


        /// <summary>
        /// Tests 5 differnt lists on the Loop distinct function to make sure the count is correctly returned.
        /// <returns> True if passed, flase is failed</returns>
        /// <param>none</param>
        /// <exception>None</exception>
        /// </summary>
        public void TestLoopZero()
        {
            Assert.AreEqual(Distinct.loopDistinct(l0), 0);
        }
        [Test]
        public void TestLoopOne()
        {
            Assert.AreEqual(Distinct.loopDistinct(l1), 1);
        }
        [Test]
        public void TestLoopOneb()
        {
            Assert.AreEqual(Distinct.loopDistinct(l1b), 1);
        }

        [Test]
        public void TestLoopTwo()
        {
            Assert.AreEqual(Distinct.loopDistinct(l2), 2);
        }
        [Test]
        public void TestLoopFive()
        {
            Assert.AreEqual(Distinct.loopDistinct(l5), 5);
        }


        /// <summary>
        /// Tests 5 differnt lists on the sorting distinct function to make sure the count is correctly returned.
        /// <returns> True if passed, flase is failed</returns>
        /// <param>none</param>
        /// <exception>None</exception>
        /// </summary>
        public void TestSortZero()
        {
            Assert.AreEqual(Distinct.sortDistinct(l0), 0);
        }
        [Test]
        public void TestSortpOne()
        {
            Assert.AreEqual(Distinct.sortDistinct(l1), 1);
        }
        [Test]
        public void TestSortOneb()
        {
            Assert.AreEqual(Distinct.sortDistinct(l1b), 1);
        }

        [Test]
        public void TestSortTwo()
        {
            Assert.AreEqual(Distinct.sortDistinct(l2), 2);
        }
        [Test]
        public void TestSortFive()
        {
            Assert.AreEqual(Distinct.sortDistinct(l5), 5);
        }
    }
}