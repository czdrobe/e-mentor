﻿using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Data
{
    public interface IMessageData: ILazyLoadable
    {
        SearchResult<Message> GetMessages(int userId, string useremail, int skip, int take);
        List<MentorMessage> GetListOfMentors(string useremail);
        List<MentorMessage> GetListOfUsersWithMessage(string useremail);
        void SaveNewMessage(string useremail, int toId, string bodyMessage);
    }
}
