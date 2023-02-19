using System.ComponentModel;

namespace SpreadsheetEngine
{
    public abstract class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public Cell(int row, int col)
        {
            RowIndex = row;
            ColumnIndex = col;
        }
        

        private int RowIndex { get; }
        private int ColumnIndex { get; }

        private String Text {
            get { return Text; }

            set { if (value == Text) { return; }
                Text = value;

                //notify listeners
                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }

        private int Value
        {
            get { return Value; }

            set { if (value == Value) { return; }
            Value = value;
            PropertyChangedEventArgs e = new PropertyChangedEventArgs("Value");}
        }
    }
}