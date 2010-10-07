using NUnit.Framework;

namespace NArgs.Tests
{
    public static class ShouldExtensions
    {
        public static void ShouldBeTrue(this bool expected)
        {
            Assert.IsTrue(expected);
        }

        public static void ShouldBeFalse(this bool expected)
        {
            Assert.IsFalse(expected);
        }

        public static void ShouldBe<T>(this T expected, T actual)
        {
            Assert.AreEqual(expected, actual);
        }

        public static void ShouldBe(this double expected, double actual)
        {
            Assert.AreEqual(expected, actual, .001);
        }
    }
}