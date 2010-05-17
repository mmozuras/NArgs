using System.Collections.Generic;
using System.Linq;
using NArgs.Marshalers;

namespace NArgs
{
    public class Args
    {
        private readonly IList<string> args;
        private readonly IList<char> argsFound;
        private readonly ArgsSchema schema;
        
        private IEnumerator<string> currentArgument;

        public Args(string schema, IEnumerable<string> args)
        {
            this.args = new List<string>(args);
            argsFound = new List<char>();

            this.schema = new ArgsSchema(schema);
            ParseArgumentStrings(this.args);
        }

        private void ParseArgumentStrings(IEnumerable<string> argsList)
        {
            for (currentArgument = argsList.GetEnumerator(); currentArgument.MoveNext();)
            {
                var argString = currentArgument.Current;
                if (argString.StartsWith("-"))
                {
                    ParseArgumentCharacters(argString.Substring(1));
                }
                else
                {
                    break;
                }
            }
        }

        private void ParseArgumentCharacters(IEnumerable<char> argChars)
        {
            foreach (var argChar in argChars)
            {
                ParseArgumentCharacter(argChar);
            }
        }

        private void ParseArgumentCharacter(char argChar)
        {
            IArgumentMarshaler marshaler;
            if (schema.ContainsMarshalerFor(argChar))
            {
                marshaler = schema.GetMarshalerFor(argChar);
                argsFound.Add(argChar);
            }
            else
            {
                throw new ArgsException(ErrorCode.UnexpectedArgument, null, argChar);
            }

            try
            {
                marshaler.Set(currentArgument);
            }
            catch (ArgsException e)
            {
                e.ErrorArgumentId = argChar;
                throw;
            }
        }

        public bool Has(char arg)
        {
            return argsFound.Contains(arg);
        }

        public int NextArgument()
        {
            return currentArgument.Current == null ? args.Count() : args.IndexOf(currentArgument.Current);
        }

        public T Get<T>(char arg)
        {
            return ArgumentMarshaler<T>.GetValue(schema.GetMarshalerFor(arg));
        }
    }
}