using System.Collections.Generic;
using System.Linq;
using NArgs.Marshalers;

namespace NArgs
{
    public class ArgsSchema
    {
        private readonly Dictionary<char, IArgumentMarshaler> marshalers;

        public ArgsSchema(string schema)
        {
            marshalers = new Dictionary<char, IArgumentMarshaler>();
            ParseSchema(schema);
        }

        public bool ContainsMarshalerFor(char key)
        {
            return marshalers.ContainsKey(key);
        }

        public IArgumentMarshaler GetMarshalerFor(char key)
        {
            return marshalers[key];
        }

        private void ParseSchema(string schema)
        {
            var elements = schema.Split(',').Where(element => element.Length > 0);
            foreach (var element in elements)
            {
                ParseSchemaElement(element.Trim());
            }
        }

        private void ParseSchemaElement(string element)
        {
            var elementId = element[0];
            var elementTail = element.Substring(1);
            ValidateSchemaElementId(elementId);
            switch (elementTail)
            {
                case "":
                    marshalers.Add(elementId, new BooleanArgumentMarshaler());
                    break;
                case "*":
                    marshalers.Add(elementId, new StringArgumentMarshaler());
                    break;
                case "#":
                    marshalers.Add(elementId, new IntegerArgumentMarshaler());
                    break;
                case "##":
                    marshalers.Add(elementId, new DoubleArgumentMarshaler());
                    break;
                case "[*]":
                    marshalers.Add(elementId, new StringListArgumentMarshaler());
                    break;
                default:
                    throw new ArgsException(ErrorCode.InvalidArgumentFormat, elementTail, elementId);
            }
        }

        private static void ValidateSchemaElementId(char elementId)
        {
            if (!char.IsLetter(elementId))
            {
                throw new ArgsException(ErrorCode.InvalidArgumentName, null, elementId);
            }
        }
    }
}