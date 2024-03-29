﻿using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core
{
    public interface IReportService:ILazyLoadable
    {
        SearchResult<ReportTeacherAppoitment> GetReportTeacherAppoitment(string useremail, int skip, int take);

        decimal GetBalanceForTeacher(string useremail);
    }
}
