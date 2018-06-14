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

        public List<MentorMessage> GetListOfMentors(string useremail)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    List<MentorMessage> lstMentorsMessage = new List<MentorMessage>();

                    var mentorsQuery = context.Set<Models.Message>().AsNoTracking().Where(x => x.ToUser.Email == useremail);
                    var lstmentors = mentorsQuery.OrderBy(x => x.Added).Select(x => x.FromUser).Distinct().ToList();

                    foreach (var mentor in lstmentors)
                    {
                        var count = mentorsQuery.Where(x => x.FromUser.Id == mentor.Id).Count();
                        var isRead = mentorsQuery.Where(x => x.FromUser.Id == mentor.Id && x.IsRead == true).Count() > 0;
                        lstMentorsMessage.Add(new MentorMessage() {
                            Id = mentor.Id,
                            Name = mentor.LastName + " " + mentor.FirstName,
                            NrOfMessage = count,
                            isRead = isRead
                        });
                    }

                    return lstMentorsMessage;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveNewMessage(string useremail, int toId, string bodyMessage)
        {
            using (var context = new MeditatiiDbContext())
            {
                var user = context.Set<Models.User>().AsNoTracking().Where(x => x.Email == useremail).FirstOrDefault();
                if (user != null)
                {
                    try
                    {
                        Models.Message newMessage = new Models.Message()
                        {
                            Body = bodyMessage,
                            Added = DateTime.Now,
                            FromUserId = user.Id,
                            ToUserId = toId
                        };

                        context.Message.Add(newMessage);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
