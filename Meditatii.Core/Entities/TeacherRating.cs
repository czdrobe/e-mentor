using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Entities
{
    /// Represents a User
    /// </summary>
    [Serializable]
    public class TeacherRating : BaseEntity
    {
        public TeacherRating()
        {
        }

        public int Id { get; set; }

        public int? AppoitmentId { get; set; }

        public int TeacherId { get; set; }

        public int StudentId { get; set; }

        public int Rating { get; set; }

        public string RatingText { get; set; }

        public DateTime Added { get; set; }

        public virtual Appoitment Appoitment { get; set; }

        public virtual User Teacher { get; set; }

        public virtual User Student { get; set; }

    }
}
