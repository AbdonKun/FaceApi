using System;
using System.Runtime.Serialization;

namespace BF.POC.FaceAPI.Domain.Exceptions
{
    [Serializable]
    public class BusinessException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        public BusinessException()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public BusinessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BusinessException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerException"></param>
        public BusinessException(System.Exception innerException)
            : base(innerException.Message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
