using NUnit.Framework;
using NotepadApp;

namespace TestFibonacci
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test0Fib()
        {
            FibonacciTextReader fib = new FibonacciTextReader(50);
            Assert.AreEqual("0", fib.ReadLine());
        }
        [Test]
        public void Test1Fib()
        {
            FibonacciTextReader fib = new FibonacciTextReader(50);
            fib.ReadLine();
            Assert.AreEqual("1", fib.ReadLine());
        }
        [Test]
        public void Test2Fib()
        {
            FibonacciTextReader fib = new FibonacciTextReader(50);
            fib.ReadLine();
            fib.ReadLine();
            Assert.AreEqual("1", fib.ReadLine());
        }

        public void Test50Fib()
        {
            FibonacciTextReader fib = new FibonacciTextReader(50);
            for (int i = 0; i < 50;)
            {
                fib.ReadLine();
            }
            Assert.AreEqual("12586269025", fib.ReadLine());
        }
        [Test]
        public void TestNullFib()
        {
            FibonacciTextReader fib = new FibonacciTextReader(5);
            fib.ReadLine();
            fib.ReadLine();
            fib.ReadLine();
            fib.ReadLine();
            fib.ReadLine();
            fib.ReadLine();
            Assert.AreEqual(null, fib.ReadLine());
        }

    }
}