using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Models
{
    public class Request
    {
        public Request()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int LearnerId { get; set; }
        public int CategoryId { get; set; }
        public int CycleId { get; set; }
        public int? CityId { get; set; }
        public bool IsOnline { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [ForeignKey("LearnerId")]
        public virtual User Learner { get; set; }
        [ForeignKey("CycleId")]
        public virtual Cycle Cycle { get; set; }
    }
}
