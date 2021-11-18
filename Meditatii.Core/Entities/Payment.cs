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

        public int LearnerId { get; set; }

        public DateTime Added { get; set; }

        public decimal Amount { get; set; }

        public int Status { get; set; }

        public string PaymentTimeStamp { get; set; }

        public string PaymentCRC { get; set; }

        public DateTime? Updated { get; set; }

        //can be 31, 93, 365 - basically subscription days
        public int Product { get; set; }

        public virtual User Learner { get; set; }

    }
}
