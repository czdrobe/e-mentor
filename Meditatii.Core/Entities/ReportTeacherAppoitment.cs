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
    public class ReportTeacherAppoitment : BaseEntity
    {
        public ReportTeacherAppoitment()
        {
        }
        public int AppoitmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal? Price { get; set; }

        public virtual User Learner { get; set; }

        public virtual Payment Payment { get; set; }

        public DateTime? PayOutTeacher { get; set; }

        public DateTime? PayOutPlatform { get; set; }

        public DateTime? PayOutTax { get; set; }

    }
}
