using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Data
{
    public interface ITeacherAvailabilityData : ILazyLoadable
    {
        SearchResult<TeacherAvailability> GetAvailability(string useremail, int skip, int take);
        SearchResult<TeacherAvailability> GetTeacherAvailabilityForDay(int userId, int day);
        void RemoveAllAvailabilityForTeacher(string username);
        void SaveNewAvailability(string useremail, int day, int Time);
    }
}
