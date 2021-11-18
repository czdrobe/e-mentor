using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class AppoitmentModel
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int LearnerId { get; set; }

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

        public int? PaymentId { get; set; }

        public UserModel Teacher { get; set; }

        public UserModel Learner { get; set; }

        public decimal? Price { get; set; }

        public bool AcceptedByTeacher { get; set; }

        public bool CanJoin { get; set; }

        public bool Active { get; set; }

        public bool NotificationNew { get; set; } // if the new appoitment was sent to the teacher 
        public bool NotificationAccepted { get; set; } // if the accepted notification was sent to the student
        public bool NotificationPaid { get; set; } // if the paid notification was sent to the teacher
        public bool NotificationRemainder { get; set; } // if the remainder notification was sent for both teacher and student

        public string SessionId { get; set; }
        public string TokenId { get; set; }

        public bool isReadyForRating { get; set; }

        public PaymentModel Payment { get; set; }
    }
}
