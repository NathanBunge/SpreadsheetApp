﻿// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Xml.Linq;

    /// <summary>
    /// TODO.
    /// </summary>
    public class Spreadsheet
    {
        private RealCell[,] cellSheet;
        private int rowCount;
        private int colCount;
        private Dictionary<Cell, List<Cell>> subscriptions = new Dictionary<Cell, List<Cell>>();

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
            if (row >= this.RowCount || row < 0)
            {
                throw new IndexOutOfRangeException(nameof(row));
            }

            if (col >= this.ColCount || row < 0)
            {
                throw new IndexOutOfRangeException(nameof(row));
            }

            return this.cellSheet[row, col];
        }

        /// <summary>
        /// Gets the cell reference.
        /// </summary>
        /// <param name="cellName">name of cell from string name.</param>
        /// <returns>cell.</returns>
        public Cell GetCell(string cellName)
        {
            int row = 0;
            int col = 0;
            this.GetCellIndex(cellName, out row, out col);
            return this.GetCell(row, col);
        }

        /// <summary>
        /// Basicllay inverse of GetCellIndex. Gets name from index.
        /// </summary>
        /// <param name="row">row index.</param>
        /// <param name="col">colum index. </param>
        /// <returns>string cell name. </returns>
        public string GetCellName(int row, int col)
        {
            if (row >= this.RowCount || row < 0)
            {
                throw new IndexOutOfRangeException(nameof(row));
            }

            if (col >= this.ColCount || row < 0)
            {
                throw new IndexOutOfRangeException(nameof(row));
            }

            StringBuilder cellName = new StringBuilder();

            // Convert col index to a letter
            char colLetter = (char)('A' + col);

            // Convert row index to string representation
            string rowLetter = (row + 1).ToString();

            cellName.Append(colLetter);
            cellName.Append(rowLetter);

            return cellName.ToString();
        }

        /// <summary>
        /// subscribes to a target cell.
        /// </summary>
        /// <param name="target">cell to subscribe to.</param>
        /// <param name="source">cell that is subscribing.</param>
        public void SubscribeToCell(Cell target, Cell source)
        {
            // Check fir self reference
            if (HasSelfReference(source, target))
            {
                throw new ArgumentException("Self reference detected.");
            }

            // Check for circular reference
            if (HasCircularReference(source, target))
            {
                throw new ArgumentException("Circular reference detected.");
            }

            // check if the source cell is already subscribed to the target cell
            if (this.subscriptions.ContainsKey(target) && this.subscriptions[target].Contains(source))
            {
                return; // source cell is already subscribed to the target cell, so return without subscribing again
            }

            target.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Value" || e.PropertyName == "Text")
                {
                    string originalText = source.Text;
                    source.Text = "0";
                    source.Text = originalText;
                }
            };

            // add the source cell to the list of subscribed cells for the target cell
            if (!this.subscriptions.ContainsKey(target))
            {
                this.subscriptions[target] = new List<Cell>();
            }

            this.subscriptions[target].Add(source);
        }

        private bool HasSelfReference(Cell target, Cell source)
        {
            if (target == source)
            {
                return true;
            }

            return false;
        }

        private bool HasCircularReference(Cell target, Cell source, HashSet<Cell> visitedCells = null)
        {
            if (visitedCells == null)
            {
                visitedCells = new HashSet<Cell>();
            }

            if (target == source)
            {
                return true;
            }

            visitedCells.Add(target);

            if (!this.subscriptions.ContainsKey(target))
            {
                return false;
            }

            foreach (var subscription in this.subscriptions[target])
            {
                if (!visitedCells.Contains(subscription) && this.HasCircularReference(subscription, source, visitedCells))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Taakes s stream and creates an xml file to send to it.
        /// </summary>
        /// <param name="stream">stream to send created file to.</param>
        public void SaveToXml(Stream stream)
        {
            XDocument doc = new XDocument(new XElement("spreadsheet"));
            for (int row = 0; row < this.RowCount; row++)
            {
                for (int col = 0; col < this.ColCount; col++)
                {
                    RealCell cell = this.cellSheet[row, col] as RealCell;
                    if (cell != null)
                    {
                        if (!string.IsNullOrEmpty(cell.Text) || cell.BGColor != 0xFFFFFFFF)
                        {
                            XElement cellElem = new XElement(
                                "cell",
                                new XAttribute("name", this.GetCellName(row, col)),
                                new XElement("bgcolor", cell.BGColor),
                                new XElement("text", cell.Text));
                            doc.Root.Add(cellElem);
                        }
                    }
                }
            }

            doc.Save(stream);
        }

        /// <summary>
        /// Loads information form an XML file from a given stream.
        /// </summary>
        /// <param name="stream">stream to load the xml file from.</param>
        public void LoadFromXml(Stream stream)
        {
            XDocument doc = XDocument.Load(stream);
            foreach (XElement cellElem in doc.Root.Elements("cell"))
            {
                string name = cellElem.Attribute("name").Value;
                Cell cell = this.GetCell(name);

                if (cellElem.Element("bgcolor") != null)
                {
                    try
                    {
                        cell.BGColor = Convert.ToUInt32((string)cellElem.Element("bgcolor"));
                    }
                    catch (FormatException)
                    {
                        cell.BGColor = 4294967040;
                    }
                }

                if (cellElem.Element("text") != null)
                {
                    cell.Text = (string)cellElem.Element("text");
                }
            }
        }

        /// <summary>
        /// Clears each cell by resetsting it to it's default values.
        /// </summary>
        public void ClearAllCells()
        {
            for (int row = 0; row < this.rowCount; row++)
            {
                for (int col = 0; col < this.ColCount; col++)
                {
                    Cell cell = this.GetCell(row, col);
                    cell.Reset();
                }
            }
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
                if (cell.Text == string.Empty)
                {
                    cell.Value = string.Empty;
                }
                else if (cell.Text.First() == '=')
                {
                    string expression = cell.Text.Substring(1);

                    // Make expression tree
                    try
                    {
                        // Make tree
                        ExpressionTree eTree = new ExpressionTree(expression, this.GetCellValue);

                        // Get list of variables used
                        List<string> vs = this.GetVariables(expression);

                        // Subscribe to list of cells
                        foreach (string v in vs)
                        {
                            this.SubscribeToCell(this.GetCell(v), cell);
                        }

                        // set value of cell to evaluated expression
                        cell.Value = eTree.Evaluate().ToString();
                    }
                    catch (KeyNotFoundException ex)
                    {
                        // If variable not found, set error text
                        cell.Value = "Cell not found";
                        Console.WriteLine(ex.ToString());
                        this.SheetPropertyChanged(sender, new PropertyChangedEventArgs("CellChanged"));
                        return;
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        // If variable not found, set error text
                        cell.Value = "Cell out of range";
                        Console.WriteLine(ex.ToString());
                        this.SheetPropertyChanged(sender, new PropertyChangedEventArgs("CellChanged"));
                        return;
                    }
                    catch (ArgumentException ex)
                    {
                        // If variable not found, set error text
                        if (ex.Message.Contains("Circular reference"))
                        {
                            cell.Value = "!(Circular reference found)";
                        }
                        else if (ex.Message.Contains("Self reference"))
                        {
                            cell.Value = "!(Self reference found)";
                        }
                        else
                        {
                            cell.Value = ex.Message;
                        }

                        Console.WriteLine(ex.ToString());
                        this.SheetPropertyChanged(sender, new PropertyChangedEventArgs("CellChanged"));
                        return;
                    }


                }
                else
                {
                    Console.WriteLine("something else changed");
                    cell.Value = cell.Text;
                }

                // notify listeners
                this.SheetPropertyChanged(sender, new PropertyChangedEventArgs("CellChanged"));
            }
            else if (e.PropertyName == "BGColor")
            {
                this.SheetPropertyChanged(sender, new PropertyChangedEventArgs("CellColorChanged"));
            }
            else
            {
                Console.WriteLine("Unkonwn cell property changed (maybe value?");
            }
        }

        /// <summary>
        ///  should add saftly checks for this.
        ///  Gets a cells numerical index given it's name.
        /// </summary>
        /// <param name="cellName">Cell nice i.e. A1.</param>
        /// <param name="row">row int to return.</param>
        /// <param name="col">col int to return.</param>
        /// <returns>True if found.</returns>
        private bool GetCellIndex(string cellName, out int row, out int col)
        {
            char colName = cellName[0];
            string rowName = cellName.Substring(1);

            // convert colname to a integer
            int colIndex = char.ToUpper(colName) - 64;

            // convert rowname to integer
            if (!int.TryParse(rowName, out int rowIndex))
            {
                throw new KeyNotFoundException();
            }

            col = colIndex - 1;
            row = rowIndex - 1;

            return true;
        }

        /// <summary>
        /// Gets a cell's value given it's name.
        /// </summary>
        /// <param name="cellName">string name.</param>
        /// <returns>double the value if it has one.</returns>
        /// <exception cref="KeyNotFoundException">Exception for cells outside spreadsheet. </exception>
        private double GetCellValue(string cellName)
        {
            int rowIndex = 0;
            int colIndex = 0;
            double cellValue = 0;
            try
            {
                this.GetCellIndex(cellName, out rowIndex, out colIndex);
                double.TryParse(this.cellSheet[rowIndex, colIndex].Value, out cellValue);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw new KeyNotFoundException();
            }

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
