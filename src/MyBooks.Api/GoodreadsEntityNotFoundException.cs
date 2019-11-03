using System;

namespace MyBooks.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodreadsEntityNotFoundException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public GoodreadsEntityNotFoundException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public GoodreadsEntityNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public GoodreadsEntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}