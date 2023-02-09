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
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    Save(myStream);
                    myStream.Close();
                }
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Save(Stream s)
        {
            string message = fileText.Text;
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(message);
            s.Write(byteData, 0, byteData.Length);

            s.Dispose();
        }


        private void Save(Stream s, string message)
        {
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(message);
            s.Write(byteData, 0, byteData.Length);

            s.Dispose();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Open the selected file to read.
                System.IO.Stream fileStream = openFileDialog1.OpenFile();

                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                {
                    LoadText(reader);
                }
                fileStream.Close();
            }
        }
    }
}
