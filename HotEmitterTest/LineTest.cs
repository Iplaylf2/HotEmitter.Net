using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HotEmitter;

namespace HotEmitterTest
{
    [TestClass]
    public class LineTest
    {
        [TestMethod]
        public void BaseMethod()
        {
            (var emit, var line) = new Emitter<Int32>();

            var increment = 10;
            var result = 0;

            emit(increment);

            var disconnect = line.Connect(increase);

            emit(increment);
            Assert.AreEqual(increment, result);

            disconnect();

            emit(increment);
            Assert.AreEqual(increment, result);

            void increase(Int32 value)
            {
                result += value;
            }
        }
    }
}
