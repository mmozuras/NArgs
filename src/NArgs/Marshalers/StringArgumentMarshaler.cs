using System.Collections.Generic;

namespace NArgs.Marshalers
{
    public class StringArgumentMarshaler : ArgumentMarshaler<string>
    {
        protected override string Value { get; set; }

        public override void Set(IEnumerator<string> currentArgument)
        {
            currentArgument.MoveNext();
            Value = currentArgument.Current;

            if (Value == null)
            {
                throw new ArgsException(ErrorCode.MissingString);
            }
        }
    }
}