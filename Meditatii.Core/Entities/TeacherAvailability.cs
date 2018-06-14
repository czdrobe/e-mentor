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
    public class TeacherAvailability : BaseEntity
    {
        public TeacherAvailability()
        {
        }

        public int TeacherId { get; set; }

        public int Day { get; set; }

        public int Time { get; set; }

        public User Teacher { get; set; }

    }
}
