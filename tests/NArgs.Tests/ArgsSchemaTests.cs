using NUnit.Framework;

namespace NArgs.Tests
{
    [TestFixture]
    public class ArgsSchemaTests
    {
        [Test]
        public void NonLetterSchema()
        {
            var exception = Assert.Catch<ArgsException>(() => new ArgsSchema("*"));
            Assert.AreEqual(ErrorCode.InvalidArgumentName, exception.ErrorCode);
            Assert.AreEqual('*', exception.ErrorArgumentId);
        }


        [Test]
        public void InvalidArgumentFormat()
        {
            var exception = Assert.Catch<ArgsException>(() => new ArgsSchema("f~"));
            Assert.AreEqual(ErrorCode.InvalidArgumentFormat, exception.ErrorCode);
            Assert.AreEqual('f', exception.ErrorArgumentId);
        }
    }
}