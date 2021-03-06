﻿using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core
{
    public interface ITeacherAvailabilityService : ILazyLoadable
    {
        SearchResult<TeacherAvailability> GetAvailability(string useremail, int skip, int take);
        SearchResult<TeacherAvailability> GetAvailabilityTeacherAvaiabilityForDay(int userId, DateTime day);
        void SaveAvailability(string username, List<TeacherAvailability> lstAvailability);
    }
}
