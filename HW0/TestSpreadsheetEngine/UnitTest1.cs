using NUnit.Framework;
using SpreadsheetEngine;
using System;
using System.Collections.Generic;

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

        // Get Cell index by Name
        [Test]
        public void TestGetCellByName()
        {
            Spreadsheet spread = new Spreadsheet(5, 5);
            Cell s = spread.GetCell("B5");
            Assert.AreEqual(1, s.ColumnIndex);
            Assert.AreEqual(4, s.RowIndex);
        }

        [Test]
        public void TestGetCellByNameEdge()
        {
            Spreadsheet spread = new Spreadsheet(50, 26);
            Cell s = spread.GetCell("Z50");
            Assert.AreEqual(25, s.ColumnIndex);
            Assert.AreEqual(49, s.RowIndex);
        }
        [Test]
        public void TestGetCellByNameOutside()
        {
            Spreadsheet spread = new Spreadsheet(50, 26);
            Assert.Throws<IndexOutOfRangeException>(() => spread.GetCell("Z51"));
        }
        [Test]
        public void TestGetCellByWrongName()
        {
            Spreadsheet spread = new Spreadsheet(50, 26);
            Assert.Throws<KeyNotFoundException>(() => spread.GetCell("ABC"));
        }

        // Test for Get Name Cell by index
        [Test]
        public void TestGetCellNameByIndex()
        {
            Spreadsheet spread = new Spreadsheet(50, 26);
            string s = spread.GetCellName(5,4);
            Assert.AreEqual("E6", s);
        }

        [Test]
        public void TestGetCellNameByIndexEdge()
        {
            Spreadsheet spread = new Spreadsheet(50, 26);
            string s = spread.GetCellName(49, 25);
            Assert.AreEqual("Z50", s);
        }

        [Test]
        public void TestGetCellNameByIndexOutside()
        {
            Spreadsheet spread = new Spreadsheet(50, 26);
            Assert.Throws<IndexOutOfRangeException>(() => spread.GetCellName(50, 25));
        }



    }
}