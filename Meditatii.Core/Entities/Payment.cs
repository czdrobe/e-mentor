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
    public class Payment : BaseEntity
    {
        public Payment()
        {
        }
        public int Id { get; set; }

        public int AppoitmentId { get; set; }

        public int LearnerId { get; set; }

        public DateTime Added { get; set; }

        public decimal Amount { get; set; }

        public int Status { get; set; }
        
        public virtual User Learner { get; set; }

    }
}
