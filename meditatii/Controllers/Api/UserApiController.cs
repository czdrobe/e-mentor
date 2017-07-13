using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System.Collections.Generic;
using System.Web.Http;

namespace meditatii.Controllers.Api
{
    public class UserApiController : ApiController
    {
        private IUsersService usersService;

        public UserApiController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        [Route("api/users/getusers")]
        public SearchResult<UserModel> GetUsers(int? categoryid, int? cycleid, int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;
            return MappingHelper.Map<SearchResult<UserModel>>(this.usersService.GetUsers(categoryid, cycleid, skip, take));
        }
    }
}