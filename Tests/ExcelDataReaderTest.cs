using BarcodeScanner.DataLookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
        ///A test for Filename
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BarcodeScanner.exe")]
        public void FilenameTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ExcelDataReader_Accessor target = new ExcelDataReader_Accessor(param0); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Filename = expected;
            actual = target.Filename;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ParseCellContents
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BarcodeScanner.exe")]
        public void ParseCellContentsTest()
        {
            List<int> expected = new List<int>(new int[]{7,8,9,10,11,12});
            List<int> actual = ExcelDataReader.ParseCellContents("7 + 8 + 9 + 10 + 11 + 12");

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (int exp in expected)
            {
                Assert.IsTrue(actual.Contains(exp));
            }
        }

        /// <summary>
        ///A test for Lookup
        ///</summary>
        [TestMethod()]
        public void LookupTest()
        {
            string filename = @"planning kinderen en leiding.xlsx"; 
            ExcelDataReader target = new ExcelDataReader(filename); 
            int groupnumber = 26;
            DateTime time = new DateTime(2011, 10, 15, 10, 00, 00);//(15-10-2011  10:00:00); 
            string expected = "Klimtoren"; //See cell G6,7,8 on Kleine speltakken
            string actual = target.Lookup(groupnumber, time);

            Assert.AreEqual(expected, actual);
        }
    }
}
