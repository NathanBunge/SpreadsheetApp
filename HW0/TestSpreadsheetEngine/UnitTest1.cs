using NUnit.Framework;
using SpreadsheetEngine;

namespace TestSpreadsheetEngine
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestRowCount()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            Assert.AreEqual(5, spread.RowCount);
        }

        [Test]
        public void TestRowIndex()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            Cell s = spread.GetCell(2, 2);
            Assert.AreEqual(2, s.RowIndex);
        }

        [Test]
        public void TestRowIndexBound()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            Cell s = spread.GetCell(4, 4);
            Assert.AreEqual(4, s.RowIndex);
        }


        [Test]
        public void TestRowIndexOut()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            //Assert.Catch( (spread.GetCell(5, 5));
            
        }



        [Test]
        public void TestColIndex()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            Cell s = spread.GetCell(2, 2);
            Assert.AreEqual(2, s.ColumnIndex);
        }



    }
}