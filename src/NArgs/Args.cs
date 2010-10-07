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
            argChars.Select(ParseArgumentCharacter)
                .ToList()
                .ForEach(argsFound.Add);
        }

        private char ParseArgumentCharacter(char argChar)
        {
            if (!schema.ContainsMarshalerFor(argChar))
            {
                throw new ArgsException(ErrorCode.UnexpectedArgument, null, argChar);
            }

            try
            {
                var marshaler = schema.GetMarshalerFor(argChar);
                marshaler.Set(currentArgument);
                return argChar;
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