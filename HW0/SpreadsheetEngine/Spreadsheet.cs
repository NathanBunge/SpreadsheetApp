using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class Spreadsheet
    {
        //subclass to use cell class
        internal class RealCell : Cell
        {
            public RealCell(int row, int col) : base(row, col)
            {
            }
        }


        private int RowCount { get; set; }
        private int ColCount { get; set; } = 0;

        List<List<RealCell>> cellSheet = new List<List<RealCell>>();
        //RealCell[][] sheet = new RealCell;

        public Spreadsheet(int rows, int cols)
        {
            

            //populate 2D array with cells and labels
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    RealCell c = new RealCell(i, j);
                    c.PropertyChanged += Cell_PropertyChanged;
                    cellSheet[i][j] = c;

                }
            }
        
        }

        private void Cell_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {   
            
            // this will update the value of a cell when it see's that text is changed
            if (sender.GetType() == typeof(RealCell))
            {
                RealCell cell = (RealCell)sender;
                if (cell.Text.First() == '=')
                {
                    cell.Value = cell.Text.Substring(1);
                }
                else
                {
                    cell.Value = cell.Text;
                }

                
            }
            
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler CellPropertyChanged = delegate { };

        internal Cell getCell(int row, int col)
        {
            return cellSheet[row][col];
        }


    }


}
