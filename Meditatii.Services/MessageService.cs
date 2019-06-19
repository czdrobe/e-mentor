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
    public class MessageService:  IMessageService
    {
        private IMessageData messageData;
        public MessageService(IMessageData messageData)
        {
            this.messageData = messageData;
        }
        
        public SearchResult<Message> GetMessages(int mentorId, string useremail, int skip, int take)
        {
            return messageData.GetMessages(mentorId, useremail, skip, take);
        }

        public List<MentorMessage> GetListOfMenters(string useremail)
        {
            return messageData.GetListOfMentors(useremail);
        }

        public List<MentorMessage> GetListOfUsersWithMessage(string useremail)
        {
            return messageData.GetListOfMentors(useremail);
        }

        public void SaveNewMessage(string useremail, int toId, string bodyMessage)
        {
            messageData.SaveNewMessage(useremail, toId, bodyMessage);
        }
    }
}
