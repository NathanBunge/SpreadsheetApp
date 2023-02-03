using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistinctNum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RunDistinctIntegers();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Runs main code to get distinct number of intgeters from a list of randome integers.
        /// <returns>None</returns>
        /// <param>None</param>
        /// <exceptino>None</exceptino>
        /// </summary>
        private  void RunDistinctIntegers()
        {
            //create list for randome numbers
            Random rnd = new Random();
            List<int> list = new List<int>();

            //fill list with randome numbers
            for (int i = 0; i < 10000; i++)
            {
                int num = rnd.Next(1, 20000);
                list.Add(num);

            }

            //write to consol
            foreach (int num in list)
            {
                Console.WriteLine(num);
            }

            //count returned by functions
            int hashcount = 0;
            int loopCount = 0;
            int sortCount = 0;

            //string builder for output text
            StringBuilder sb = new StringBuilder();

            //call functions to calculate count
            hashcount = Distinct.hashDistinct(list);
            loopCount = Distinct.loopDistinct(list);
            sortCount = Distinct.sortDistinct(list);


            //string builder
            sb.Append("1. Hash method");
            sb.Append(Environment.NewLine);
            sb.Append("\t Result: ");
            sb.Append(hashcount);
            sb.Append("\t Method: Create a hash table and add each number in the list. " +
                "As you add,if the number is already in the hash table" +
                "simply go to the next number. Finally," +
                " Return the count of the items in the hash table");
            sb.Append(Environment.NewLine+ Environment.NewLine);

            sb.Append("2. Loop method");
            sb.Append(Environment.NewLine);
            sb.Append("\t Result: ");
            sb.Append(loopCount);
            sb.Append("\t Method: For each element in the list, compare it to all the elments in front of it." +
                "If you see a number that is the same, stop and go to the next number without increasing total count." +
                "If you reach the end of the list without seeing a number that is the same, increase the total count by 1." +
                "Repeat this for every number in the list. Return the total count.");
            sb.Append(Environment.NewLine + Environment.NewLine);

            sb.Append("1. Sort method");
            sb.Append(Environment.NewLine);
            sb.Append("\t Result: ");
            sb.Append(sortCount);
            sb.Append("\t Method: First, sort the list. Then, iterate through the list one. For each number in the list," +
                "if the number in front of it is different, then increment the total count. Return the total count.");


            textBox1.Text = sb.ToString();


        }


    }
}
