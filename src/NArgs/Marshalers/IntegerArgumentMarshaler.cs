using System;
using System.Collections.Generic;

namespace NArgs.Marshalers
{
    public class IntegerArgumentMarshaler : ArgumentMarshaler<int>
    {
        protected override int Value { get; set; }

        public override void Set(IEnumerator<string> currentArgument)
        {
            string parameter = null;
            try
            {
                currentArgument.MoveNext();
                parameter = currentArgument.Current;
                Value = int.Parse(parameter);
            }
            catch (ArgumentNullException)
            {
                throw new ArgsException(ErrorCode.MissingInteger);
            }
            catch (FormatException)
            {
                throw new ArgsException(ErrorCode.InvalidInteger, parameter);
            }
        }
    }
}