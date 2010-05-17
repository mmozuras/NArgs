using System.Collections.Generic;

namespace NArgs.Marshalers
{
    public interface IArgumentMarshaler
    {
        void Set(IEnumerator<string> currentArgument);
    }
}