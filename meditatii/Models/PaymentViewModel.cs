using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }

        public int LearnerId { get; set; }

        public DateTime Added { get; set; }

        public decimal Amount { get; set; }

        public string PaymentTimeStamp { get; set; }

        public string PaymentCRC { get; set; }

        public DateTime? Updated { get; set; }

        public int Status { get; set; }

        public string PaymentCode { get; set; }

        //can be 31, 93, 365 - basically subscription days
        public int Product { get; set; }

        public string ProductName { get; set; }

        public string InvoiceNumber { get; set; }
    }
}
