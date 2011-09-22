using BarcodeScanner.DataLookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for ExcelDataReaderTest and is intended
    ///to contain all ExcelDataReaderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExcelDataReaderTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Lookup
        ///</summary>
        [TestMethod()]
        public void LookupTest()
        {
            string filename = @"C:\Documents and Settings\L.vBeek\My Documents\Scouting\Jotari\jotari planning 2011 laatste versie.xlsx"; // TODO: Initialize to an appropriate value
            ExcelDataReader_Old target = new ExcelDataReader_Old(filename);
            int groupnumber = 5;
            DateTime time = new DateTime(1899,12,30,11,30,00);
            string expected = "radio"; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.Lookup(groupnumber, time);
            Assert.AreEqual(expected, actual);
        }
    }
}
