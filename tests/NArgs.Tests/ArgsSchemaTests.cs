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
            exception.ErrorCode.ShouldBe(ErrorCode.InvalidArgumentName);
            exception.ErrorArgumentId.ShouldBe('*');
        }


        [Test]
        public void InvalidArgumentFormat()
        {
            var exception = Assert.Catch<ArgsException>(() => new ArgsSchema("f~"));
            exception.ErrorCode.ShouldBe(ErrorCode.InvalidArgumentFormat);
            exception.ErrorArgumentId.ShouldBe('f');
        }
    }
}