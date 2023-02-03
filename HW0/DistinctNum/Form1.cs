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

        private  void RunDistinctIntegers()
        {
            Random rnd = new Random();
            List<int> list = new List<int>();

            for (int i = 0; i < 10000; i++)
            {
                int num = rnd.Next(1, 20000);
                list.Add(num);

            }
            foreach (int num in list)
            {
                Console.WriteLine(num);
            }

            int numDist = 0;
            
            numDist = Distinct.hashDistinct(list);
            textBox1.Text = numDist.ToString();

            numDist = Distinct.loopDistinct(list);
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(numDist.ToString());

            numDist = Distinct.sortDistinct(list);
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(numDist.ToString());


        }


    }
}
