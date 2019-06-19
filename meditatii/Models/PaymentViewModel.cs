using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }

        public int AppoitmentId { get; set; }

        public int LearnerId { get; set; }

        public DateTime Added { get; set; }

        public decimal Amount { get; set; }

        public int Status { get; set; }

    }
}
