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
    public class Ad : BaseEntity
    {
        public Ad()
        {
        }

        public int Duration { get; set; }

        public int Price { get; set; }

        public DateTime Added { get; set; }

        public ICollection<Cycle> Cycles { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public User Teacher { get; set; }

        public int TeacherId { get; set; }
    }
}
