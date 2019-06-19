using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class AppoitmentChatModel
    {

        public int AppoitmentId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public DateTime Added { get; set; }

        public AppoitmentModel Appoitment { get; set; }

        public UserModel User { get; set; }

        public bool IsMine { get; set; }
    }
}
