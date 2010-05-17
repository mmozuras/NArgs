using System.Collections.Generic;

namespace NArgs.Marshalers
{
    public class BooleanArgumentMarshaler : ArgumentMarshaler<bool>
    {
        protected override bool Value { get; set; }

        public override void Set(IEnumerator<string> currentArgument)
        {
            Value = true;
        }
    }
}