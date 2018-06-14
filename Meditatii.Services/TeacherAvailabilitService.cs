using Meditatii.Core;
using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Services
{
    public class TeacherAvailabilityService : ITeacherAvailabilityService
    {
        private ITeacherAvailabilityData availabilityData;
        public TeacherAvailabilityService(ITeacherAvailabilityData availabilityData)
        {
            this.availabilityData = availabilityData;
        }
        
        public SearchResult<TeacherAvailability> GetAvailability(string useremail, int skip, int take)
        {
            return availabilityData.GetAvailability(useremail, skip, take);
        }

    }
}
