using BarcodeScanner.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for ActivityTimeLineTest and is intended
    ///to contain all ActivityTimeLineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ActivityTimeLineTest
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
        ///A test for getCurrent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BarcodeScanner.exe")]
        public void getCurrentTest()
        {
            ActivityTimeLine atl = new ActivityTimeLine();
            atl.Add(new Activity("TV", new DateTime(2011, 10, 15, 14, 30, 00), new TimeSpan(0, 20, 00), "Pivoruimte"));
            atl.Add(new Activity("Radio", new DateTime(2011, 10, 15, 14, 50, 00), new TimeSpan(0, 20, 00), "Beverruimte"));

            DateTime now = new DateTime(2011, 10, 15, 14, 40, 00);

            Activity expected = new Activity("TV", new DateTime(2011, 10, 15, 14, 30, 00), new TimeSpan(0, 20, 00), "Pivoruimte");
            Activity actual;
            actual = atl.getCurrent(now);
            Assert.AreEqual(expected, actual);
        }
    }
}
