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
        [Route("api/messages/getmessages/{mentorId}/{page}")]
        public SearchResult<MessageModel> GetMessages(int mentorId, int page)
        {
            int itemsPerPage = 100;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;
            return MappingHelper.Map<SearchResult<MessageModel>>(this.messagesService.GetMessages(mentorId, System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpGet]
        [Route("api/messages/listofmentors")]
        public List<MentorMessageModel> GetListOfMentors()
        {
            return MappingHelper.Map<List<MentorMessageModel>>(this.messagesService.GetListOfMenters(System.Web.HttpContext.Current.User.Identity.Name));
        }

        [HttpGet]
        [Route("api/messages/getlistofuserswithmessage")]
        public List<MentorMessageModel> GetListOfUsersWithMessage()
        {
            return MappingHelper.Map<List<MentorMessageModel>>(this.messagesService.GetListOfMenters(System.Web.HttpContext.Current.User.Identity.Name));
        }

        [HttpPost]
        [Route("api/messages/savenewmessage")]
        public void SaveNewMessage(MessageModel newMessage)
        {
            this.messagesService.SaveNewMessage(System.Web.HttpContext.Current.User.Identity.Name, newMessage.ToUserId, newMessage.Body);
        }
    }
}