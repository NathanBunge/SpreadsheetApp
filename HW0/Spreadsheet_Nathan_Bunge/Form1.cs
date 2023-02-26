using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadsheetEngine;

namespace Spreadsheet_Nathan_Bunge
{
    public partial class Form1 : Form
    {
        private Spreadsheet sheet;
        public Form1()
        {
            InitializeComponent();
            InitializeDataGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //gaurentee
            //guarentee
            //apparently
        }

        internal void InitializeDataGrid()
        {
            spreadsheetGrid.Rows.Clear();
            spreadsheetGrid.Columns.Clear();

            this.sheet = new Spreadsheet(25,50);



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



            sheet.CellPropertyChanged += Sheet_CellPropertyChanged;



        }

        private void Sheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CellChanged")
            {
                //sender is cell (i think)
                Cell cell = (Cell)sender;
                int row = cell.RowIndex;
                int col = cell.ColumnIndex;

                spreadsheetGrid.Rows[row].Cells[col].Value = cell.Value;
            }
           // throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cell s = (Cell) this.sheet.getCell(2, 3);
            s.Text = "Sicko mode";
        }
    }
}
