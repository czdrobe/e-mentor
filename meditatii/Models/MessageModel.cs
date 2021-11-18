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
        public string FromUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ToUserId { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public String SenderName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool isMy { get; set; }

        public string FromProfileImageUrl { get; set; }
    }
}
