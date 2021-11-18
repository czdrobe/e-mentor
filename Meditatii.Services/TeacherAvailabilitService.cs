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
        private IAppoitmentData appoitmentData;

        public TeacherAvailabilityService(ITeacherAvailabilityData availabilityData, IAppoitmentData appoitmentData)
        {
            this.availabilityData = availabilityData;
            this.appoitmentData = appoitmentData;
        }
        
        public SearchResult<TeacherAvailability> GetAvailability(string useremail, int skip, int take)
        {
            return availabilityData.GetAvailability(useremail, skip, take);
        }

        public SearchResult<TeacherAvailability> GetAvailabilityTeacherAvaiabilityForDay(int userId, DateTime day)
        {
            var avaibility = availabilityData.GetTeacherAvailabilityForDay(userId, (int)day.DayOfWeek, day == DateTime.Now.Date);
            var appoitments = appoitmentData.GetAppoitmentsForDate(userId, day);

            //filter out appoitment times from avaibility 
            if (appoitments.TotalRows > 0)
            {
                foreach (var appoitment in appoitments.Entities)
                {
                    var itemFound = avaibility.Entities.Where(x => x.Time == appoitment.StartDate.Hour).FirstOrDefault();
                    if (itemFound != null)
                    {
                        avaibility.Entities.Remove(itemFound);
                    }
                }
            }

            return avaibility;
        }

        public void SaveAvailability(string username, List<TeacherAvailability> lstAvailability)
        {
            availabilityData.RemoveAllAvailabilityForTeacher(username);
            foreach (var item in lstAvailability)
            {
                availabilityData.SaveNewAvailability(username, item.Day, item.Time);
            }
        }



    }
}
