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

        public CategoryModel  Category { get; set; }

        public bool CanJoin { get; set; }
    }
}
