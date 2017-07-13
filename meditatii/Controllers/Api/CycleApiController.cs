using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Helpers;
using System.Collections.Generic;
using System.Web.Http;

namespace meditatii.Controllers.Api
{
    public class CycleApiController : ApiController
    {
        private ICycleService cycleService;

        public CycleApiController(ICycleService cycleService)
        {
            this.cycleService = cycleService;
        }

        [HttpGet]
        [Route("api/cycle/getall")]
        public List<Models.CycleModel> GetAll()
        {
            return MappingHelper.Map<List<CycleModel>>(this.cycleService.GetCycles());
        }
    }
}