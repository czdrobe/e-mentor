﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Entities
{
    /// Represents a User
    /// </summary>
    [Serializable]
    public class User : BaseEntity
    {
        public User()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Dob { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public int? Rating { get; set; }

        public decimal? Price { get; set; }

        public int ServiceFee { get; set; }

        public int Tax { get; set; }

        public bool Active { get; set; }

        public int NrOfVisitors { get; set; }

        public int NrOfPhoneViews { get; set; }

        public DateTime Added { get; set; }

        public bool AlsoOnline { get; set; }

        public bool AtTeacher { get; set; }

        public bool AtStudent { get; set; }

        public DateTime? SubscriptionStartDate { get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

        public bool SendNewsletter { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Cycle> Cycles { get; set; }

        public ICollection<City> Cities { get; set; }

        public Experience Experience { get; set; }

        public Occupation Occupation { get; set; }

        public string Studies { get; set; }

        public string YouYubeURL { get; set; }
    }
}
