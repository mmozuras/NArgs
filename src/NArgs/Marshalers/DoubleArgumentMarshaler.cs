using System;
using System.Collections.Generic;
using System.Globalization;

namespace NArgs.Marshalers
{
    public class DoubleArgumentMarshaler : ArgumentMarshaler<double>
    {
        protected override double Value { get; set; }

        public override void Set(IEnumerator<string> currentArgument)
        {
            string parameter = null;
            try
            {
                currentArgument.MoveNext();
                parameter = currentArgument.Current;
                Value = double.Parse(parameter, new CultureInfo("en-US"));
            }
            catch (ArgumentNullException)
            {
                throw new ArgsException(ErrorCode.MissingDouble);
            }
            catch (FormatException)
            {
                throw new ArgsException(ErrorCode.InvalidDouble, parameter);
            }
        }
    }
}