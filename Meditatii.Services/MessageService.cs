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
        
        public SearchResult<Message> GetMessages(string useremail, int skip, int take)
        {
            return messageData.GetMessages(useremail, skip, take);
        }
    }
}
