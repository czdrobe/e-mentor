using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class MessageModel
    {

        /// <summary>
        /// 
        /// </summary>
        public int FromId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ToId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Added { get; set; }
    }
}
