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
    }
}
