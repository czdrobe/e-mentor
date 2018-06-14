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
    public class Appoitment : BaseEntity
    {
        public Appoitment()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LearnerId { get; set; }

        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Added { get; set; }

        public User Teacher { get; set; }
        public Category Category { get; set; }

    }
}
