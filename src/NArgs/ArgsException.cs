using System;

namespace NArgs
{
    public class ArgsException : Exception
    {
        public ArgsException(ErrorCode errorCode) : this(errorCode, null, '\0')
        {
        }

        public ArgsException(ErrorCode errorCode, string errorParameter) : this(errorCode, errorParameter, '\0')
        {
        }

        public ArgsException(ErrorCode errorCode, string errorParameter, char errorArgumentId)
        {
            ErrorCode = errorCode;
            ErrorParameter = errorParameter;
            ErrorArgumentId = errorArgumentId;
        }

        public char ErrorArgumentId { get; set; }
        public ErrorCode ErrorCode { get; private set; }
        public string ErrorParameter { get; private set; }

        public string ErrorMessage()
        {
            switch (ErrorCode)
            {
                case ErrorCode.UnexpectedArgument:
                    return string.Format("Argument -{0} unexpected.", ErrorArgumentId);
                case ErrorCode.MissingString:
                    return string.Format("Could not find string parameter for -{0}.", ErrorArgumentId);
                case ErrorCode.InvalidInteger:
                    return string.Format("Argument -{0} expects an integer but was '{1}'.", ErrorArgumentId, ErrorParameter);
                case ErrorCode.MissingInteger:
                    return string.Format("Could not find integer parameter for -{0}.", ErrorArgumentId);
                case ErrorCode.InvalidDouble:
                    return string.Format("Argument -{0} expects a double but was '{1}'.", ErrorArgumentId, ErrorParameter);
                case ErrorCode.MissingDouble:
                    return string.Format("Could not find double parameter for -{0}.", ErrorArgumentId);
                case ErrorCode.InvalidArgumentName:
                    return string.Format("'{0}' is not a valid argument name.", ErrorArgumentId);
                case ErrorCode.InvalidArgumentFormat:
                    return string.Format("'{0}' is not a valid argument format.", ErrorParameter);
                default:
                    return string.Empty;
            }
        }
    }
}