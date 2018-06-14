using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Entities
{
    /// Represents a User
    /// </summary>
    [Serializable]
    public class Message : BaseEntity
    {
        public Message()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int FromUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ToUserId { get; set; }

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

    }
}
