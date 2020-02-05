using System;
namespace ServiceManual.Exceptions
{
    public class NoResultsFoundException : Exception
    {
        /// <summary>
        /// Default Message
        /// </summary>
        public NoResultsFoundException() : base("No results found")
        {
        }

        /// <summary>
        /// Custom Message
        /// </summary>
        /// <param name="message"></param>
        public NoResultsFoundException(string message) : base(message)
        {

        }
    }

    public class IncorrectTypeException : Exception
    {
        /// <summary>
        ///  Type List
        /// </summary>
        public enum Types
        {
            Int,
            String,
            Float,
            DateTime
        }

        /// <summary>
        /// Default Message
        /// </summary>
        public IncorrectTypeException() : base("Incorrect type!")
        {
        }

        /// <summary>
        /// Custom Message
        /// </summary>
        /// <param name="message"></param>
        public IncorrectTypeException(string message) : base(message) {}

        /// <summary>
        /// Predifined message ({variable} has to be {type})
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="type"></param>
        public IncorrectTypeException(string variable, Types type) : base($"{variable} has to be {type.ToString()}") { }
    }
}
