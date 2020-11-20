using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ChatApp.CL.Models
{
    /// <summary>
    /// Poco class for Response message
    /// </summary>
    #region ResponseMessage
    [DataContract]
    public class ResponseMessage<T>
    {
        /// <summary>
        /// Gets or sets Status
        /// </summary>
        [DataMember(Name = "Status")]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets Message
        /// </summary>
        [DataMember(Name = "Message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets Data
        /// </summary>
        [DataMember(Name = "Data")]
        public T Data { get; set; }
    }
    #endregion
}
