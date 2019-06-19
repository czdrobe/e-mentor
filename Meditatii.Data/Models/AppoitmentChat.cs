using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Models
{
    public class AppoitmentChat
    {
        public AppoitmentChat()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AppoitmentId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Added { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("AppoitmentId")]
        public virtual Appoitment Appoitment { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
