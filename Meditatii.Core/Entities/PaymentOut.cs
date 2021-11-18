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
    public class PaymentOut : BaseEntity
    {
        public PaymentOut()
        {
        }

        public int Id { get; set; }

        public int AppoitmentId { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime Added { get; set; }

    }
}
