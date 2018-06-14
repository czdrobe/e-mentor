using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System.Collections.Generic;
using System.Web.Http;

namespace meditatii.Controllers.Api
{
    public class AppoitmentApiController : ApiController
    {
        private IAppoitmentService appoitmentService;

        public AppoitmentApiController(IAppoitmentService appoitmentService)
        {
            this.appoitmentService = appoitmentService;
        }

        [HttpGet]
        [Route("api/appoitments/getappoitments/{page}")]
        public SearchResult<AppoitmentModel> GetAppoitments(int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;
            return MappingHelper.Map<SearchResult<AppoitmentModel>>(this.appoitmentService.GetAppoitments(System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpPost]
        [Route("api/appoitments/saveappoitment")]
        public void SaveNewMessage(AppoitmentModel newAppoitment)
        {
            this.appoitmentService.SaveNewAppoitment(System.Web.HttpContext.Current.User.Identity.Name, newAppoitment.TeacherId,newAppoitment.StartDate, newAppoitment.EndDate);
        }
    }
}