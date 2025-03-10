using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ex0204.Utilities;
using System;
using System.IO;
using System.Threading;

namespace Ex0204Test
{
    [TestClass]
    public class TimedInputTests
    {
        public class BlockingTextReader : TextReader
        {
            public override string ReadLine()
            {
                Thread.Sleep(Timeout.Infinite);
                return null;
            }
        }

        [TestMethod]
        public void ReadInputWithTimeout_ShouldReturnInput_WhenInputIsProvidedBeforeTimeout()
        {
            var originalIn = Console.In;
            try
            {
                Console.SetIn(new StringReader("Answer"));
                var result = TimedInput.ReadInputWithTimeout(5);
                Assert.AreEqual("Answer", result);
            }
            finally
            {
                Console.SetIn(originalIn);
            }
        }

        [TestMethod]
        public void ReadInputWithTimeout_ShouldReturnEmptyString_WhenTimeoutOccurs()
        {
            var originalIn = Console.In;
            try
            {
                Console.SetIn(new BlockingTextReader());
                var result = TimedInput.ReadInputWithTimeout(1);
                Assert.AreEqual(string.Empty, result);
            }
            finally
            {
                Console.SetIn(originalIn);
            }
        }
    }
}

