using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class TeacherRatingModel
    {
        public int Id { get; set; }

        public int AppoitmentId { get; set; }

        public int TeacherId { get; set; }

        public int StudentId { get; set; }

        public int Rating { get; set; }

        public DateTime Added { get; set; }

        public virtual Appoitment Appoitment { get; set; }

        public virtual User Teacher { get; set; }

        public virtual User Student { get; set; }
    }
}
