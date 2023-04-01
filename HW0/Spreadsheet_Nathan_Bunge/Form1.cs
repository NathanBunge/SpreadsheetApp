// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Spreadsheet_Nathan_Bunge
{
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

    /// <summary>
    /// Main Form.
    /// </summary>
    public partial class Form1 : Form
    {
        private Spreadsheet sheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// Documented.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
        }

        /// <summary>
        /// create data grid.
        /// </summary>
        internal void InitializeDataGrid()
        {
            this.spreadsheetGrid.Rows.Clear();
            this.spreadsheetGrid.Columns.Clear();

            this.sheet = new Spreadsheet(50, 25);

            // Add 26 columns
            StringBuilder sb = new StringBuilder("col");
            for (char c = 'A'; c <= 'Z'; c++)
            {
                // add current charater
                sb.Append(c);

                // create colum
                this.spreadsheetGrid.Columns.Add(sb.ToString(), c.ToString());

                // delte last charater
                sb.Length = sb.Length - 1;
            }

            // add 50 rows
            for (int i = 1; i <= 50; i++)
            {
                // add row
                this.spreadsheetGrid.Rows.Add();
            }

            // label rows
            // got from :(https://stackoverflow.com/questions/9581626/show-row-number-in-row-header-of-a-datagridview)
            foreach (DataGridViewRow row in this.spreadsheetGrid.Rows)
            {
                row.HeaderCell.Value = string.Format("{0}", row.Index + 1);
            }

            this.sheet.SheetPropertyChanged += this.Sheet_CellPropertyChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Sheet_CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CellChanged")
            {
                // sender is cell (i think)
                Cell cell = (Cell)sender;
                int row = cell.RowIndex;
                int col = cell.ColumnIndex;

                this.spreadsheetGrid.Rows[row].Cells[col].Value = cell.Value;
            }
        }

        /// <summary>
        /// Generates test values for demo.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">event.</param>
        private void Button1_Click(object sender, EventArgs e)
        {
            // Define cell to save reference to
            Cell s = (Cell)this.sheet.GetCell(0, 0);

            // Set 50 randome cells
            Random randome = new Random(50);
            for (int i = 0; i < 50; i++)
            {
                s = this.sheet.GetCell(randome.Next(0, 50), randome.Next(0, 25));

                s.Text = "Sicko mode";
            }

            // Set all cells in column B
            for (int i = 0; i < 50; i++)
            {
                s = (Cell)this.sheet.GetCell(i, 1);
                s.Text = "This is cell B" + (i + 1).ToString();
            }

            // Set all cells in column A
            for (int i = 0; i < 50; i++)
            {
                s = (Cell)this.sheet.GetCell(i, 0);
                s.Text = "=B" + (i + 1).ToString();
            }
        }

        private void spreadsheetGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            this.spreadsheetGrid.Rows[row].Cells[col].Value = this.sheet.GetCell(row, col).Text;
        }

        private void spreadsheetGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            this.sheet.GetCell(row, col).Text = this.spreadsheetGrid.Rows[row].Cells[col].Value.ToString();
        }
    }
}
