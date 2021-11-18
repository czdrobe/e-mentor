using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class RequestModel
    {

        public int Id { get; set; }

        public int LearnerId { get; set; }
        public int CategoryId { get; set; }
        public int CycleId { get; set; }
        public int? CityId { get; set; }
        public bool IsOnline { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public string RequestCode { get; set; }

        public int Duration { get; set; }

        public CategoryModel Category { get; set; }
        public CityModel City { get; set; }
        public CycleModel Cycle { get; set; }
        public UserModel Learner { get; set; }
    }
}
