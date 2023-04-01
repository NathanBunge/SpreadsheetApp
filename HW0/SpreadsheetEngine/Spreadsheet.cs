﻿// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// TODO.
    /// </summary>
    public class Spreadsheet
    {
        private RealCell[,] cellSheet;
        private int rowCount;
        private int colCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// Speadsheet.
        /// </summary>
        /// <param name="rows">number of rows.</param>
        /// <param name="cols">number of columns.</param>
        public Spreadsheet(int rows, int cols)
        {
            this.rowCount = rows;
            this.colCount = cols;
            this.cellSheet = new RealCell[rows, cols];

            // populate 2D array with cells and labels
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    RealCell c = new RealCell(row, col);
                    c.PropertyChanged += this.Cell_PropertyChanged;
                    this.cellSheet[row, col] = c;
                }
            }
        }

        /// <summary>
        /// Cell changed event.
        /// </summary>
        public event PropertyChangedEventHandler SheetPropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets count of rows.
        /// </summary>
        public int RowCount
        {
            get { return this.rowCount; }
        }

        /// <summary>
        /// Gets count of  columns.
        /// </summary>
        public int ColCount
        {
            get { return this.colCount; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="row">row number.</param>
        /// <param name="col">column number.</param>
        /// <returns>returns a cell.</returns>
        public Cell GetCell(int row, int col)
        {
            if (row > this.RowCount || row < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (col > this.ColCount || row < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            return this.cellSheet[row, col];
        }

        public Cell GetCell(string cellName)
        {
            int row=0;
            int col = 0;
            this.GetCellIndex(cellName, ref row, ref col);
            return this.GetCell(row, col);
        }

        public void SubscribeToCell(Cell target, Cell source)
        {
            target.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Value" || e.PropertyName == "Text")
                {
                    string originalText = source.Text;
                    source.Text = "0";
                    source.Text = originalText;
                }
            };
        }

        private List<string> GetVariables(string inputString)
        {
            List<string> variables = new List<string>();
            Regex regex = new Regex(@"[A-Za-z]\d+");

            foreach (Match match in regex.Matches(inputString))
            {
                variables.Add(match.Value);
            }

            return variables;
        }

        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // this will update the value of a cell when it see's that text is changed
            if (e.PropertyName == "Text")
            {
                Cell cell = (Cell)sender;
                if (cell.Text.First() == '=')
                {
                    string expression = cell.Text.Substring(1);

                    // Make expression tree
                    ExpressionTree eTree = new ExpressionTree(expression, this.GetCellValue);

                    // set value of cell to evaluated expression
                    cell.Value = eTree.Evaluate().ToString();

                    // Get list of variables used
                    List<string> vs = this.GetVariables(expression);

                    // Subscribe to list of cells
                    foreach (string v in vs)
                    {
                        this.SubscribeToCell(this.GetCell(v), cell);
                    }
                }
                else
                {
                    Console.WriteLine("something else changed");
                    cell.Value = cell.Text;
                }
            }

            // notify listeners
            this.SheetPropertyChanged(sender, new PropertyChangedEventArgs("CellChanged"));
        }

        /// <summary>
        ///  should add saftly checks for this
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool GetCellIndex(string cellName, ref int row, ref int col)
        {
            char colName = cellName[0];
            string rowName = cellName.Substring(1);

            // convert colname to a integer
            int colIndex = char.ToUpper(colName) - 64;

            // convert rowname to integer
            int rowIndex = int.Parse(rowName);

            col = colIndex-1;
            row = rowIndex-1;

            return true;
        }

        private double GetCellValue(string cellName)
        {
            int rowIndex = 0;
            int colIndex = 0;
            this.GetCellIndex(cellName, ref rowIndex, ref colIndex);
            double cellValue = 0;

            double.TryParse(this.cellSheet[rowIndex, colIndex].Value, out cellValue);

            return cellValue;
        }

        /// <summary>
        /// subcclass to make instatiation.
        /// </summary>
        internal class RealCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RealCell"/> class.
            /// Constructor.
            /// </summary>
            /// <param name="row">number of rows.</param>
            /// <param name="col">number of columns.</param>
            public RealCell(int row, int col)
                : base(row, col)
            {
            }
        }

        
    }
}
