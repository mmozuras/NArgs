using System.Collections.Generic;

namespace NArgs.Marshalers
{
    public class StringListArgumentMarshaler : ArgumentMarshaler<List<string>>
    {
        public StringListArgumentMarshaler()
        {
            Value = new List<string>();
        }

        protected override sealed List<string> Value { get; set; }

        public override void Set(IEnumerator<string> currentArgument)
        {
            currentArgument.MoveNext();
            if (currentArgument.Current == null)
            {
                throw new ArgsException(ErrorCode.MissingString);
            }
            Value.Add(currentArgument.Current);
        }
    }
}