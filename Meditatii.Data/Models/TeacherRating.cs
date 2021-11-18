using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Models
{
    public class TeacherRating
    {
        public TeacherRating()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? AppoitmentId { get; set; }

        public int TeacherId { get; set; }

        public int StudentId { get; set; }

        public int Rating { get; set; }

        public string RatingText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Added { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("AppoitmentId")]
        public virtual Appoitment Appoitment { get; set; }
        [ForeignKey("TeacherId")]
        public virtual User Teacher { get; set; }
        [ForeignKey("StudentId")]
        public virtual User Student { get; set; }

    }
}
