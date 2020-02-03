using System;
namespace ServiceManual.Exceptions
{
    public class ErrorMessage
    {
        public string ErrorCode { get; set; }
        public string Msg { get; set; }

        /// <summary>
        /// ErrorMessage Constructor with message
        /// </summary>
        /// <param name="msg"></param>
        public ErrorMessage(string msg)
        {
            Msg = msg;
        }

        /// <summary>
        /// ErrorMessage Constructor with ErrorCode and Message
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="msg"></param>
        public ErrorMessage(string errorCode, string msg)
        {
            ErrorCode = errorCode;
            Msg = msg;
        }
    }
}
