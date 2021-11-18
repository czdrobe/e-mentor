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
    public class Request : BaseEntity
    {
        public Request()
        {
        }

        public int LearnerId { get; set; }
        public int CategoryId { get; set; }
        public int? CityId { get; set; }
        public int CycleId { get; set; }
        public bool IsOnline{ get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }

        public virtual Category Category { get; set; }
        public virtual City City { get; set; }
        public virtual Cycle Cycle { get; set; }
        public virtual User Learner { get; set; }
    }
}
