using System.Collections.Generic;

namespace NArgs.Marshalers
{
    public abstract class ArgumentMarshaler<T> : IArgumentMarshaler
    {
        public static T GetValue(IArgumentMarshaler argumentMarshaler)
        {
            var marshaler = argumentMarshaler as ArgumentMarshaler<T>;
            return marshaler != null ? marshaler.Value : default(T);
        }

        protected abstract T Value { get; set; }
        public abstract void Set(IEnumerator<string> currentArgument);
    }
}