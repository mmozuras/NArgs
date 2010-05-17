using NUnit.Framework;

namespace NArgs.Tests
{
    [TestFixture]
    public class ArgsExceptionTest
    {
        [TestCase(ErrorCode.InvalidArgumentName, null, '#', "'#' is not a valid argument name.")]
        [TestCase(ErrorCode.InvalidDouble, "Forty two", 'x', "Argument -x expects a double but was 'Forty two'.")]
        [TestCase(ErrorCode.InvalidArgumentFormat, "$", 'x', "'$' is not a valid argument format.")]
        [TestCase(ErrorCode.InvalidInteger, "Forty two", 'x', "Argument -x expects an integer but was 'Forty two'.")]
        [TestCase(ErrorCode.MissingDouble, null, 'x', "Could not find double parameter for -x.")]
        [TestCase(ErrorCode.MissingInteger, null, 'x', "Could not find integer parameter for -x.")]
        [TestCase(ErrorCode.MissingString, null, 'x', "Could not find string parameter for -x.")]
        [TestCase(ErrorCode.UnexpectedArgument, null, 'x', "Argument -x unexpected.")]
        public void Messages(ErrorCode errorCode, string errorParameter, char argumentId, string expectedMessage)
        {
            var e = new ArgsException(errorCode, errorParameter, argumentId);
            Assert.AreEqual(expectedMessage, e.ErrorMessage());
        }
    }
}