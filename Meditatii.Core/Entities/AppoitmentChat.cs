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
    public class AppoitmentChat : BaseEntity
    {
        public AppoitmentChat()
        {
        }
        public int AppoitmentId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public DateTime Added { get; set; }

        public Appoitment Appoitment { get; set; }

        public User User { get; set; }

    }
}
