using System.Collections.Generic;

namespace NArgs.Marshalers
{
    public abstract class ArgumentMarshaler<T> : IArgumentMarshaler
    {
        public static T GetValue(IArgumentMarshaler argumentMarshaler)
        {
            var integerArgumentMarshaler = argumentMarshaler as ArgumentMarshaler<T>;
            return integerArgumentMarshaler != null ? integerArgumentMarshaler.Value : default(T);
        }

        protected abstract T Value { get; set; }
        public abstract void Set(IEnumerator<string> currentArgument);
    }
}