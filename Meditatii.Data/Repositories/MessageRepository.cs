using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Repositories
{
    public class MessageRepository : IMessageData
    {
        public SearchResult<Message> GetMessages(string useremail, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var messages = context.Set<Models.Message>().AsNoTracking().Where(x => x.ToUser.Email == useremail);

                    totalRows = messages.Count();

                    return
                        new SearchResult<Message> {
                            Entities = MappingHelper.Map<List<Message>>(
                                                                    messages.OrderBy(x => x.Id)
                                                                        .Skip(skip)
                                                                        .Take(take)
                                                                        .ToList()),
                            TotalRows = totalRows
                        };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
    }
}
