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

        public int? PaymentId { get; set; }

        public decimal? Price { get; set; }

        public bool AcceptedByTeacher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Added { get; set; }

        public User Teacher { get; set; }

        public User Learner { get; set; }

        public bool Active { get; set; }

        public bool NotificationNew { get; set; } // if the new appoitment was sent to the teacher 
        public bool NotificationAccepted { get; set; } // if the accepted notification was sent to the student
        public bool NotificationPaid { get; set; } // if the paid notification was sent to the teacher
        public bool NotificationRemainder { get; set; } // if the remainder notification was sent for both teacher and student

        public string SessionId { get; set; }
        public string TokenId { get; set; }
        public virtual ICollection<TeacherRating> Ratings { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
