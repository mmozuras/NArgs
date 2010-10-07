using NUnit.Framework;

namespace NArgs.Tests
{
    [TestFixture]
    public class ArgsSchemaTests
    {
        [TestCase("*", ErrorCode.InvalidArgumentName, '*', Description = "Non letter schema")]
        [TestCase("f~", ErrorCode.InvalidArgumentFormat, 'f', Description = "Invalid argument format")]
        public void Schema(string schema, ErrorCode errorCode, char argumentId)
        {
            var exception = Assert.Catch<ArgsException>(() => new ArgsSchema(schema));
            exception.ErrorCode.ShouldBe(errorCode);
            exception.ErrorArgumentId.ShouldBe(argumentId);
        }
    }
}