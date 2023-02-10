using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

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
        /// Constructor to set maxLines (number of fib sequence)
        /// </summary>
        /// <param number of fib numbers to return="maxLines"></param>
        public FibonacciTextReader(int maxLines)
        {
            max = maxLines;
        }

        /// <summary>
        /// override method to read lines. Each time it is called, 
        /// it returns the next number in the fibanochi sequence
        /// </summary>
        /// <returns>one string from fibinachi sequence</returns>
        public override string ReadLine()
        {
            //if the first time, return 0 and set to 1 iteration
            if (iter == 0)
            {
                iter = 1;
                return "0";
            }

            //if secont time, set current to one
            else if (iter == 1)
            {
                current = 1;
            }

            //if last time, return null
            if (iter == max)
            {
                return null;
            }

            //get number
            BigInteger next = current + last;
            last = current;
            current = next;

            //increment iteration
            iter++;

            //return number
            return current.ToString();

            //return base.ReadLine();
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

            m = ReadLine();

            while (m != null)
            {
                str.Append(m);
                str.Append(Environment.NewLine);
                m = ReadLine();
            }
            return str.ToString();

            //return base.ReadToEnd();
        }
    }
}
