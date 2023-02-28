using NUnit.Framework;
using SpreadsheetEngine;
using System;

namespace TestSpreadsheetEngine
{
    public class Tests
    {


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
            Assert.Throws<IndexOutOfRangeException>(() => spread.GetCell(5,4));

        }


        [Test]
        public void TestColCount()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            Assert.AreEqual(5, spread.ColCount);
        }

        [Test]
        public void TestColIndex()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            Cell s = spread.GetCell(2, 2);
            Assert.AreEqual(2, s.ColumnIndex);
        }

        [Test]
        public void TestColIndexBound()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            Cell s = spread.GetCell(4, 4);
            Assert.AreEqual(4, s.ColumnIndex);
        }


        [Test]
        public void TestColIndexOut()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            Assert.Throws<IndexOutOfRangeException>(() => spread.GetCell(4, 5));

        }



    }
}