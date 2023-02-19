using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreadsheet_Nathan_Bunge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDataGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        internal void InitializeDataGrid()
        {
            spreadsheetGrid.Rows.Clear();
            spreadsheetGrid.Columns.Clear();



            // Add 26 columns 
            StringBuilder sb = new StringBuilder("col");
            for (char c = 'A'; c <= 'Z'; c++)
            {
                // add current charater
                sb.Append(c);

                // create colum
                spreadsheetGrid.Columns.Add(sb.ToString(), c.ToString());

                // delte last charater
                sb.Length = sb.Length - 1;
            }

            // add 50 rows
            for (int i = 1; i<=50; i++)
            {
                // add row
                spreadsheetGrid.Rows.Add();

            }

            // label rows
            // got from :(https://stackoverflow.com/questions/9581626/show-row-number-in-row-header-of-a-datagridview)
            foreach (DataGridViewRow row in spreadsheetGrid.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }

                




        }
    }
}
