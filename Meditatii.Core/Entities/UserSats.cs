using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Entities
{
    [Serializable]
    public class UserSats : BaseEntity
    {
        public int TotalTeacher { get; set; }
        public int TotalUser { get; set; }
        public int TotalMeetingMinutes { get; set; }
    }
}
