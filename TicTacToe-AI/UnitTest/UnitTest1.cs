using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TicTacToe_AI;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            State state = new State();
            Assert.IsNotNull(state);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ExceptionTest()
        {
            throw new Exception();
        }
    }
}
