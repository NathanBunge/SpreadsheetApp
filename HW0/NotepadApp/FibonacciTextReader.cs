using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestFibonacci")]

namespace NotepadApp
{
    /// <summary>
    /// Class to calculate fibonacci sequence. Constructor sets the length of the sequence to get returned
    /// </summary>
    internal class FibonacciTextReader : System.IO.TextReader
    {
        //number of fib numbers to return
        int max = 0;

        //counter to keep track how many we have returned
        int iter = 0;

        //track current and last fib numbers
        BigInteger current = new BigInteger(0);
        BigInteger last = new BigInteger(0);


        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// constructor to set maxLines (number of fib sequence)
        /// </summary>
        /// <param name="maxLines"> number of fib numbers to return</param>
        public FibonacciTextReader(int maxLines)
        {
            this.max = maxLines;
        }

        /// <summary>
        /// override method to read lines. Each time it is called, 
        /// it returns the next number in the fibanochi sequence
        /// </summary>
        /// <returns>one string from fibinachi sequence</returns>
        public override string ReadLine()
        {
            // if the first time, return 0 and set to 1 iteration
            if (this.iter == 0)
            {
                this.iter = 1;
                return "0";
            }

            // if secont time, set current to one
            else if (this.iter == 1)
            {
                this.current = 1;
                this.iter++;
                return "1";
            }

            // if last time, return null
            if (this.iter == this.max)
            {
                return null;
            }

            // get number
            BigInteger next = this.current + this.last;
            if(iter == 1)
            {
                Console.WriteLine(this.current.ToString());
                Console.WriteLine(this.last.ToString());
                Console.WriteLine(next.ToString());
            }
            this.last = this.current;
            this.current = next;

            // increment iteration
            this.iter++;

            // return number
            return this.current.ToString();

        }

        /// <summary>
        /// Calls readLine until it returns null. Appends all the lines to a single string
        /// and returns that string
        /// </summary>
        /// <returns>String concated from all readLines</returns>
        public override string ReadToEnd()
        {
            StringBuilder str = new StringBuilder();

            string m = "";

            m = this.ReadLine();

            while (m != null)
            {
                str.Append(m);
                str.Append(Environment.NewLine);
                m = this.ReadLine();
            }
            return str.ToString();

        }
    }
}
