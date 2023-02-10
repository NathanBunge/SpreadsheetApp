using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadApp
{
    internal class FibonacciTextReader : System.IO.TextReader
    {
        int max = 0;
        int sequence = 0;

        ulong current = 0;
        ulong last = 0;

        public FibonacciTextReader(int maxLines)
        {
            max = maxLines;
        }

        public override string ReadLine()
        {
            if (sequence == 0)
            {
                sequence++;
                return "0";
            }
            else if (sequence == 1)
            {
                current = 1;
            }

            if (sequence == max)
            {
                return null;
            }

            ulong next = current + last;
            last = current;
            current = next;

            sequence++;

            return current.ToString();

            //return base.ReadLine();
        }


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
