using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class TeacherAvailabilityModel
    {
        public int TeacherId { get; set; }

        public int Day { get; set; }

        public int Time { get; set; }

        public UserModel Teacher { get; set; }
    }
}
