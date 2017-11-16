using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System.Collections.Generic;
using System.Web.Http;

namespace meditatii.Controllers.Api
{
    public class MessageApiController : ApiController
    {
        private IMessageService messagesService;

        public MessageApiController(IMessageService messagesService)
        {
            this.messagesService = messagesService;
        }

        [HttpGet]
        [Route("api/messages/getmessages")]
        public SearchResult<MessageModel> GetMessages(int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;
            return MappingHelper.Map<SearchResult<MessageModel>>(this.messagesService.GetMessages(System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }
    }
}