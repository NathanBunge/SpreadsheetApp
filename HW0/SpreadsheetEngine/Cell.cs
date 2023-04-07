// <copyright file="Cell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.ComponentModel;

    /// <summary>
    /// Abstract class cell.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        private int rowIndex;
        private int columnIndex;
        private string text;
        private string value;
        private uint bGColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="row">row number.</param>
        /// <param name="col">column number.</param>
        public Cell(int row, int col)
        {
            this.rowIndex = row;
            this.columnIndex = col;

            this.text = string.Empty;
            this.value = string.Empty;

            this.bGColor = 0xFFFFFFFF;
        }

        /// <summary>
        /// Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets row index.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets column index.
        /// </summary>
        public int ColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Gets or sets text.
        /// Sets Text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value == this.text)
                {
                    return;
                }

                this.text = value;

                // notify listeners
                this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }

        /// <summary>
        /// Gets value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }

            internal set
            {
                if (value == this.value)
                {
                    return;
                }

                this.value = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        /// <summary>
        /// Gets or sets color for cell.
        /// </summary>
        public uint BGColor
        {
            get
            {
                return this.bGColor;
            }

            set
            {
                if (this.bGColor == value)
                {
                    return;
                }

                this.bGColor = value;
                this.PropertyChanged(this, new PropertyChangedEventArgs("BGColor"));
            }
        }
    }
}