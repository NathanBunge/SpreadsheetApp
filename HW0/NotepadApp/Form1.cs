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
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadText("test.txt");
        }

        /// <summary>
        /// Loads text from a file with a given pathname
        /// </summary>
        /// <param name="pathname">pathnae to the input file</param>
        private void LoadText(string pathname)
        {
            StreamReader sr = new StreamReader(pathname);
            this.LoadText(sr);
        }


        /// <summary>
        /// loads the text from a given text streams and displays it to the main form
        /// </summary>
        /// <param name="sr">textreadr object</param>
        private void LoadText(TextReader sr)
        {
            string s = string.Empty;
            string message = string.Empty;


            message = sr.ReadToEnd();

            this.fileText.Text = message;
        }

        /// <summary>
        /// Save the text in the current form to a file of the user's choice.
        /// Is run when "save" button is clicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            //---from miscrosoft documentaion: https://learn.microsoft.com

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

        /// <summary>
        /// Save text from the Form to a file
        /// </summary>
        /// <param File to be saved to="s"></param>
        private void Save(Stream s)
        {
            string message = fileText.Text;
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(message);
            s.Write(byteData, 0, byteData.Length);

            s.Dispose();
        }

        /// <summary>
        /// Save text from a given message to a file
        /// </summary>
        /// <param name="s">file to save to</param>
        /// <param name="message">text to be saved</param>
        private void Save(Stream s, string message)
        {
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(message);
            s.Write(byteData, 0, byteData.Length);

            s.Dispose();
        }

        /// <summary>
        /// Loads text from a file and displays to form when "load" button is clicked
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">event</param>
        private void LoadFileBtn(object sender, EventArgs e)
        {
            //---from miscrosoft documentaion: https://learn.microsoft.com

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

        /// <summary>
        /// Gets 50 numbers in fib sequence and sends to form textbox
        /// Is called when get 50 fib is pressed
        /// </summary>
        /// <param name="sender">senter</param>
        /// <param name="e">event</param>
        private void Load50FibBtn(object sender, EventArgs e)
        {
            FibonacciTextReader fib = new FibonacciTextReader(50);
            this.LoadText(fib);
        }

        /// <summary>
        /// Gets 100 numbers in fib sequence and sends to form textbox
        /// Is called when get 100 fib is pressed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void Load100FibBtn(object sender, EventArgs e)
        {
            FibonacciTextReader fib = new FibonacciTextReader(100);
            this.LoadText(fib);
        }
    }
}
