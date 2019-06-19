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

        public UserModel Teacher { get; set; }

        public UserModel Learner { get; set; }

        public decimal? Price { get; set; }

        public bool CanJoin { get; set; }

        public string SessionId { get; set; }
        public string TokenId { get; set; }

        public bool isReadyForRating { get; set; }

        public IList<PaymentModel> Payments { get; set; }
    }
}
