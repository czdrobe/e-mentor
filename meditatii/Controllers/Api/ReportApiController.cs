using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using Meditatii.Core.Enums;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace meditatii.Controllers.Api
{
    public class ReportApiController : ApiController
    {
        private IReportService reportService;

        public ReportApiController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet]
        [Route("api/report/getteacherappoitmentreport/{page}")]
        public SearchResult<ReportTeacherAppoitmentModel> GetTeacherAppoitmentReport(int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            //check if the current user is teacher or pupil
            bool isteacher = HttpContext.Current.User.IsInRole(UserType.Teacher.ToString());

            if (!isteacher)
            {
                throw new Exception("You're not a teacher");
            }

            return MappingHelper.Map<SearchResult<ReportTeacherAppoitmentModel>>(this.reportService.GetReportTeacherAppoitment(System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }


        [HttpGet]
        [Route("api/report/getbalance")]
        public decimal GetBalance()
        {
            //check if the current user is teacher or pupil
            bool isteacher = HttpContext.Current.User.IsInRole(UserType.Teacher.ToString());

            if (!isteacher)
            {
                throw new Exception("You're not a teacher");
            }

            return this.reportService.GetBalanceForTeacher(HttpContext.Current.User.Identity.Name);
        }

    }
}