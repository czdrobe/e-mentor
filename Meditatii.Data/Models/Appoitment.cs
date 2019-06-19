using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Models
{
    public class Appoitment
    {
        public Appoitment()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TeacherId { get; set; }

        public int LearnerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate { get; set; }

        public decimal? Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Added { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("TeacherId")]
        public virtual User Teacher { get; set; }
        [ForeignKey("LearnerId")]
        public virtual User Learner { get; set; }

        public string SessionId { get; set; }
        public string TokenId { get; set; }

        public virtual ICollection<TeacherRating> Ratings { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
