using System.ComponentModel;

namespace SpreadsheetEngine
{
    public abstract class Cell : INotifyPropertyChanged
    {
        protected int rowIndex;
        protected int columnIndex;


        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public Cell(int row, int col)
        {
            this.rowIndex = row;
            this.columnIndex = col;

            this.text = string.Empty;
            this.value = string.Empty;
        }



        
        public int RowIndex
        {
            get
            { return this.rowIndex; }
        }

        public int ColumnIndex
        {
            get
            { return this.columnIndex; }
        }

        protected string text;

        public string Text {
            get { return this.text; }

            set { if (value == this.text) { return; }
                this.text = value;

                //notify listeners
                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }

        protected string value;

        public string Value
        {
            get { return this.value; }
        
            internal set
            {
                if (value == this.value) { return; }
                this.value = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));

            }
        }
    }
}