using BarcodeScanner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for UtilitiesTest and is intended
    ///to contain all UtilitiesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UtilitiesTest
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
        ///A test for Later
        ///</summary>
        [TestMethod()]
        public void LaterTest()
        {
            DateTime q1 = new DateTime(1899, 12, 30, 10, 31, 00);
            DateTime q2 = new DateTime(1899, 12, 30, 12, 29, 00);
            DateTime q3 = new DateTime(1899, 12, 30, 11, 20, 00);
            DateTime q4 = new DateTime(1899, 12, 30, 11, 40, 00);
            DateTime compareTo = new DateTime(2011, 6, 15, 11, 30, 00);

            Assert.AreEqual(false, Utilities.Later(q1, compareTo));
            Assert.AreEqual(true, Utilities.Later(q2, compareTo));
            Assert.AreEqual(false, Utilities.Later(q3, compareTo));
            Assert.AreEqual(true, Utilities.Later(q4, compareTo));
        }
    }
}
