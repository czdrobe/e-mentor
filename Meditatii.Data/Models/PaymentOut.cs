using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Models
{
    public class PaymentOut
    {
        public PaymentOut()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AppoitmentId { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime Added { get; set; }

        [ForeignKey("AppoitmentId")]
        public virtual Appoitment Appoitment { get; set; }


    }
}
