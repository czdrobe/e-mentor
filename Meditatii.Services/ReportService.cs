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
    public class ReportService : IReportService
    {
        private IReportData  reportData;
        public ReportService(IReportData reportData)
        {
            this.reportData = reportData;
        }

        public SearchResult<ReportTeacherAppoitment> GetReportTeacherAppoitment(string useremail, int skip, int take)
        {
            return reportData.GetReportTeacherAppoitment(useremail, skip, take);
        }

        public decimal GetBalanceForTeacher(string useremail)
        {
            return reportData.GetBalanceForTeacher(useremail);
        }

    }
}
