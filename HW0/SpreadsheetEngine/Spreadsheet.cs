// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// TODO.
    /// </summary>
    public class Spreadsheet
    {
        private RealCell[,] cellSheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// TODO.
        /// </summary>
        /// <param name="rows">number of rows.</param>
        /// <param name="cols">number of columns.</param>
        public Spreadsheet(int rows, int cols)
        {
            this.RowCount = rows;
            this.ColCount = cols;
            this.cellSheet = new RealCell[rows, cols];

            // populate 2D array with cells and labels
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    RealCell c = new RealCell(i, j);
                    c.PropertyChanged += this.Cell_PropertyChanged;
                    this.cellSheet[i, j] = c;
                }
            }
        }

        /// <summary>
        /// Cell changed event.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged = (sender, e) => { };

        private int RowCount { get; set; }

        private int ColCount { get; set; } = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="row">row number.</param>
        /// <param name="col">column number.</param>
        /// <returns>returns a cell.</returns>
        public Cell GetCell(int row, int col)
        {
            return this.cellSheet[row, col];
        }

        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // this will update the value of a cell when it see's that text is changed
            if (e.PropertyName == "Text")
            {
                Cell cell = (Cell)sender;
                if (cell.Text.First() == '=')
                {
                    cell.Value = cell.Text.Substring(1);
                }
                else
                {
                    cell.Value = cell.Text;
                }
            }

            // notify listeners
            this.CellPropertyChanged(sender, new PropertyChangedEventArgs("CellChanged"));
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
