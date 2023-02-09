using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadText("test.txt");
        }

        private void LoadText(string pathname)
        {
            StreamReader sr = new StreamReader(pathname);
            LoadText(sr);
        }

        private void LoadText(TextReader sr)
        {
            string s = "";
            string message = "";


            message = sr.ReadToEnd();

            fileText.Text = message;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Save(getUserFilename(), fileText.Text);
        }

        private string getUserFilename()
        {
            //string filename = System.Console.ReadLine();
            string filename = "123.txt";

            return filename;
        }
        private void Save(string filename, string message)
        {

            StreamWriter sw = null;
            sw = new StreamWriter(filename);


            Save(sw, message);
        }


        private void Save(StreamWriter s, string message)
        {
 
            s.WriteLine(message);

            s.Dispose();
        }
    }
}
