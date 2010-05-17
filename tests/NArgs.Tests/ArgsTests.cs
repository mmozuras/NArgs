using System.Collections.Generic;
using NUnit.Framework;

namespace NArgs.Tests
{
    [TestFixture]
    public class ArgsTest
    {
        [Test]
        public void CreateWithNoSchemaOrArguments()
        {
            var args = new Args(string.Empty, new string[0]);
            Assert.AreEqual(0, args.NextArgument());
        }

        [Test]
        public void ExtraArguments()
        {
            var args = new Args("x,y*", new[] {"-x", "-y", "alpha", "beta"});
            Assert.IsTrue(args.Get<bool>('x'));
            Assert.AreEqual("alpha", args.Get<string>('y'));
            Assert.AreEqual(3, args.NextArgument());
        }

        [Test]
        public void ExtraArgumentsThatLookLikeFlags()
        {
            var args = new Args("x,y", new[] {"-x", "alpha", "-y", "beta"});
            Assert.IsTrue(args.Has('x'));
            Assert.IsFalse(args.Has('y'));
            Assert.IsTrue(args.Get<bool>('x'));
            Assert.IsFalse(args.Get<bool>('y'));
            Assert.AreEqual(1, args.NextArgument());
        }

        [Test]
        public void InvalidDouble()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args("x##", new[] {"-x", "Forty two"}));
            Assert.AreEqual(ErrorCode.InvalidDouble, exception.ErrorCode);
            Assert.AreEqual('x', exception.ErrorArgumentId);
            Assert.AreEqual("Forty two", exception.ErrorParameter);
        }

        [Test]
        public void InvalidInteger()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args("x#", new[] {"-x", "Forty two"}));
            Assert.AreEqual(ErrorCode.InvalidInteger, exception.ErrorCode);
            Assert.AreEqual('x', exception.ErrorArgumentId);
            Assert.AreEqual("Forty two", exception.ErrorParameter);
        }

        [Test]
        public void MissingDouble()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args("x##", new[] {"-x"}));
            Assert.AreEqual(ErrorCode.MissingDouble, exception.ErrorCode);
            Assert.AreEqual('x', exception.ErrorArgumentId);
        }

        [Test]
        public void MissingInteger()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args("x#", new[] {"-x"}));
            Assert.AreEqual(ErrorCode.MissingInteger, exception.ErrorCode);
            Assert.AreEqual('x', exception.ErrorArgumentId);
        }

        [Test]
        public void MissingStringArgument()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args("x*", new[] {"-x"}));
            Assert.AreEqual(ErrorCode.MissingString, exception.ErrorCode);
            Assert.AreEqual('x', exception.ErrorArgumentId);
        }

        [Test]
        public void MissingStringArrayElement()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args("x[*]", new[] {"-x"}));
            Assert.AreEqual(ErrorCode.MissingString, exception.ErrorCode);
            Assert.AreEqual('x', exception.ErrorArgumentId);
        }

        [Test]
        public void SimpleBooleanPresent()
        {
            var args = new Args("x", new[] {"-x"});
            Assert.AreEqual(true, args.Get<bool>('x'));
            Assert.AreEqual(1, args.NextArgument());
        }

        [Test]
        public void SimpleDoublePresent()
        {
            var args = new Args("x##", new[] {"-x", "42.3"});
            Assert.IsTrue(args.Has('x'));
            Assert.AreEqual(42.3, args.Get<double>('x'), .001);
        }

        [Test]
        public void SimpleIntPresent()
        {
            var args = new Args("x#", new[] {"-x", "42"});
            Assert.IsTrue(args.Has('x'));
            Assert.AreEqual(42, args.Get<int>('x'));
            Assert.AreEqual(2, args.NextArgument());
        }

        [Test]
        public void SimpleStringPresent()
        {
            var args = new Args("x*", new[] {"-x", "param"});
            Assert.IsTrue(args.Has('x'));
            Assert.AreEqual("param", args.Get<string>('x'));
            Assert.AreEqual(2, args.NextArgument());
        }

        [Test]
        public void SpacesInFormat()
        {
            var args = new Args("x, y", new[] {"-xy"});
            Assert.IsTrue(args.Has('x'));
            Assert.IsTrue(args.Has('y'));
            Assert.AreEqual(1, args.NextArgument());
        }

        [Test]
        public void StringArray()
        {
            var args = new Args("x[*]", new[] {"-x", "alpha"});
            Assert.IsTrue(args.Has('x'));
            var result = args.Get<List<string>>('x');
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("alpha", result[0]);
        }

        [Test]
        public void WithNoSchemaButWithMultipleArguments()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args(string.Empty, new[] {"-x", "-y"}));
            Assert.AreEqual(ErrorCode.UnexpectedArgument, exception.ErrorCode);
            Assert.AreEqual('x', exception.ErrorArgumentId);
        }

        [Test]
        public void WithNoSchemaButWithOneArgument()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args(string.Empty, new[] {"-x"}));
            Assert.AreEqual(ErrorCode.UnexpectedArgument, exception.ErrorCode);
            Assert.AreEqual('x', exception.ErrorArgumentId);
        }
    }
}