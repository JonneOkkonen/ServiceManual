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
}
