using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Data
{
    public interface IMessageData: ILazyLoadable
    {
        SearchResult<Message> GetMessages(string useremail, int skip, int take);
    }
}
