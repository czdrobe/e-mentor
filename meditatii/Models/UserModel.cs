using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class UserModel
    {
        //public int Id { get; set; } //do not send the id of user
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

        public string UserCode { get; set; }
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

        public DateTime? SubscriptionStartDate { get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

        public bool IsSubscriptionOk { get; set; }

        public ICollection<CategoryModel> Categories { get; set; }

        public ICollection<CycleModel> Cycles { get; set; }

        public ICollection<CityModel> Cities { get; set; }

        public bool IsTeacher {get;set;}

        public bool AtTeacher { get; set; }

        public bool AtStudent { get; set; }

        public string YouYubeURL { get; set; }

        public string Studies { get; set; }

        public ExperienceModel Experience { get; set; }
        
        public OccupationModel Occupation { get; set; }
    }
}
