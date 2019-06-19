using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core
{
    public interface IMessageService:ILazyLoadable
    {
        SearchResult<Message> GetMessages(int mentorId, string useremail, int skip, int take);
        List<MentorMessage> GetListOfMenters(string useremail);
        List<MentorMessage> GetListOfUsersWithMessage(string useremail);
        void SaveNewMessage(string useremail, int toId, string bodyMessage);
    }
}
