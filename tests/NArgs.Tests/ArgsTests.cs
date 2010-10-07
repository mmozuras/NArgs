using System.Collections.Generic;
using NUnit.Framework;

namespace NArgs.Tests
{
    [TestFixture]
    public class ArgsTest
    {
        [TestCase("x##", ErrorCode.InvalidDouble)]
        [TestCase("x#", ErrorCode.InvalidInteger)]
        public void Invalid(string schema, ErrorCode errorCode)
        {
            var exception = Assert.Catch<ArgsException>(() => new Args(schema, new[] {"-x", "Forty two"}));
            exception.ErrorCode.ShouldBe(errorCode);
            exception.ErrorArgumentId.ShouldBe('x');
            exception.ErrorParameter.ShouldBe("Forty two");
        }

        [TestCase("x##", ErrorCode.MissingDouble)]
        [TestCase("x#", ErrorCode.MissingInteger)]
        [TestCase("x*", ErrorCode.MissingString)]
        [TestCase("x[*]", ErrorCode.MissingString)]
        public void Missing(string schema, ErrorCode errorCode)
        {
            var exception = Assert.Catch<ArgsException>(() => new Args(schema, new[] {"-x"}));
            exception.ErrorCode.ShouldBe(errorCode);
            exception.ErrorArgumentId.ShouldBe('x');
        }

        [Test]
        public void CreateWithNoSchemaOrArguments()
        {
            var args = new Args(string.Empty, new string[0]);
            args.NextArgument().ShouldBe(0);
        }

        [Test]
        public void ExtraArguments()
        {
            var args = new Args("x,y*", new[] {"-x", "-y", "alpha", "beta"});
            args.Get<bool>('x').ShouldBeTrue();
            args.Get<string>('y').ShouldBe("alpha");
            args.NextArgument().ShouldBe(3);
        }

        [Test]
        public void ExtraArgumentsThatLookLikeFlags()
        {
            var args = new Args("x,y", new[] {"-x", "alpha", "-y", "beta"});
            args.Has('x').ShouldBeTrue();
            args.Has('y').ShouldBeFalse();
            args.Get<bool>('x').ShouldBeTrue();
            args.Get<bool>('y').ShouldBeFalse();
            args.NextArgument().ShouldBe(1);
        }

        [Test]
        public void SimpleBooleanPresent()
        {
            var args = new Args("x", new[] {"-x"});
            args.Get<bool>('x').ShouldBeTrue();
            args.NextArgument().ShouldBe(1);
        }

        [Test]
        public void SimpleDoublePresent()
        {
            var args = new Args("x##", new[] {"-x", "42.3"});
            args.Has('x').ShouldBeTrue();
            args.Get<double>('x').ShouldBe(42.3);
        }

        [Test]
        public void SimpleIntPresent()
        {
            var args = new Args("x#", new[] {"-x", "42"});
            args.Has('x').ShouldBeTrue();
            args.Get<int>('x').ShouldBe(42);
            args.NextArgument().ShouldBe(2);
        }

        [Test]
        public void SimpleStringPresent()
        {
            var args = new Args("x*", new[] {"-x", "param"});
            args.Has('x').ShouldBeTrue();
            args.Get<string>('x').ShouldBe("param");
            args.NextArgument().ShouldBe(2);
        }

        [Test]
        public void SpacesInFormat()
        {
            var args = new Args("x, y", new[] {"-xy"});
            args.Has('x').ShouldBeTrue();
            args.Has('y').ShouldBeTrue();
            args.NextArgument().ShouldBe(1);
        }

        [Test]
        public void StringArray()
        {
            var args = new Args("x[*]", new[] {"-x", "alpha"});
            args.Has('x').ShouldBeTrue();
            var result = args.Get<List<string>>('x');
            result.Count.ShouldBe(1);
            result[0].ShouldBe("alpha");
        }

        [Test]
        public void WithNoSchemaButWithMultipleArguments()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args(string.Empty, new[] {"-x", "-y"}));
            exception.ErrorCode.ShouldBe(ErrorCode.UnexpectedArgument);
            exception.ErrorArgumentId.ShouldBe('x');
        }

        [Test]
        public void WithNoSchemaButWithOneArgument()
        {
            var exception = Assert.Catch<ArgsException>(() => new Args(string.Empty, new[] {"-x"}));
            exception.ErrorCode.ShouldBe(ErrorCode.UnexpectedArgument);
            exception.ErrorArgumentId.ShouldBe('x');
        }
    }
}