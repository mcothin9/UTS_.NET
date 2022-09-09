using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Week4LabProgramQuestion;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var circle = new Shapes(4.0);
            Assert.AreEqual(50.272, circle.CircleArea());

        }

        [TestMethod]
        public void TestMethod2()
        {

            var rectangle = new Shapes(5.0, 4.0);
            Assert.AreEqual(20.0, rectangle.RectangleArea());
        }
    }
}
