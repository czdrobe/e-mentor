using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class ReportTeacherAppoitmentModel
    {
        public int AppoitmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal? Price { get; set; }

        public virtual UserModel Learner { get; set; }

        public virtual PaymentModel Payment { get; set; }

        public DateTime? PayOutTeacher { get; set; }

        public DateTime? PayOutPlatform { get; set; }

        public DateTime? PayOutTax { get; set; }
    }
}
