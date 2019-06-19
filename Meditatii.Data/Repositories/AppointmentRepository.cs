using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Repositories
{
    public class AppoitmentRepository : IAppoitmentData
    {
        public SearchResult<Appoitment> GetAppoitments(bool isTeacher, string useremail, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => (isTeacher ? x.Teacher.Email == useremail : x.Learner.Email == useremail));

                    totalRows = appoitments.Count();

                    return
                        new SearchResult<Appoitment> {
                            Entities = MappingHelper.Map<List<Appoitment>>(
                                                                    appoitments.OrderBy(x => x.Id)
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

        public SearchResult<Appoitment> GetActiveAppoitments(bool isTeacher, string useremail, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x =>  DateTime.Now <= x.EndDate && (isTeacher ? x.Teacher.Email == useremail : x.Learner.Email == useremail));

                    totalRows = appoitments.Count();

                    return
                        new SearchResult<Appoitment>
                        {
                            Entities = MappingHelper.Map<List<Appoitment>>(
                                                                    appoitments.OrderBy(x => x.Id)
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

        public SearchResult<Appoitment> GetOldAppoitments(bool isTeacher, string useremail, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.EndDate < DateTime.Now && (isTeacher ? x.Teacher.Email == useremail : x.Learner.Email == useremail));

                    totalRows = appoitments.Count();

                    return
                        new SearchResult<Appoitment>
                        {
                            Entities = MappingHelper.Map<List<Appoitment>>(
                                                                    appoitments.OrderByDescending(x => x.Id)
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

        public Appoitment GetAppoitment(int appoitmentId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var appoitment = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Id == appoitmentId).FirstOrDefault();

                    return MappingHelper.Map<Appoitment>(appoitment);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public SearchResult<Appoitment> GetAppoitmentsForDate(int userId, DateTime day)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.TeacherId == userId && DbFunctions.TruncateTime(x.StartDate) == DbFunctions.TruncateTime(day));

                    totalRows = appoitments.Count();

                    return
                        new SearchResult<Appoitment>
                        {
                            Entities = MappingHelper.Map<List<Appoitment>>(
                                                                    appoitments.OrderBy(x => x.Id).ToList()),
                            TotalRows = totalRows
                        };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveAppoitment(Appoitment appoitment)
        {
            using (var context = new MeditatiiDbContext())
            {
                Models.Appoitment appoitmentEntry = MappingHelper.Map<Models.Appoitment>(appoitment);
                
                context.Appoitment.Add(appoitmentEntry);
                context.Entry(appoitmentEntry).State = EntityState.Modified;
                context.Entry(appoitmentEntry.Learner).State = EntityState.Detached;
                context.Entry(appoitmentEntry.Teacher).State = EntityState.Detached;
                context.SaveChanges();
            }
        }

        public void SaveNewAppoitment(string useremail, int teacherId, DateTime startDate, DateTime endDate)
        {
            using (var context = new MeditatiiDbContext())
            {
                var user = context.Set<Models.User>().AsNoTracking().Where(x => x.Email == useremail).FirstOrDefault();
                var teacher = context.Set<Models.User>().AsNoTracking().Where(x => x.Id == teacherId).FirstOrDefault();
                if (user != null)
                {
                    try
                    {
                        Models.Appoitment newAppoitment = new Models.Appoitment()
                        {
                            TeacherId = teacherId,
                            LearnerId = user.Id,
                            StartDate = startDate,
                            EndDate = endDate,
                            Added = DateTime.Now,
                            Price = teacher.Rating
                        };

                        context.Appoitment.Add(newAppoitment);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public void DeleteAppoitment(string appoitmentId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("delete from Appoitment where Id = " + appoitmentId);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveChat(int appoitmentId, string useremail, string message)
        {
            using (var context = new MeditatiiDbContext())
            {
                var user = context.Set<Models.User>().AsNoTracking().Where(x => x.Email == useremail).FirstOrDefault();
                if (user != null)
                {
                    try
                    {
                        Models.AppoitmentChat newChat = new Models.AppoitmentChat()
                        {
                            AppoitmentId = appoitmentId,
                            Message =message,
                            UserId = user.Id,
                            Added = DateTime.Now,
                        };

                        context.AppoitmentChat.Add(newChat);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public SearchResult<AppoitmentChat> GetChatForAppoitment(int appoitmentId, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var appoitmentsChat = context.Set<Models.AppoitmentChat>().AsNoTracking().Where(x => x.AppoitmentId == appoitmentId);

                    totalRows = appoitmentsChat.Count();

                    return
                        new SearchResult<AppoitmentChat>
                        {
                            Entities = MappingHelper.Map<List<AppoitmentChat>>(
                                                                    appoitmentsChat.OrderBy(x => x.Id)
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

        public void Payment(int[] lstAppoitmentId, string useremail)
        {
            if (lstAppoitmentId == null || lstAppoitmentId.Length <= 0)
                return;

            using (var context = new MeditatiiDbContext())
            {
                var user = context.Set<Models.User>().AsNoTracking().Where(x => x.Email == useremail).FirstOrDefault();
                
                if (user != null)
                {

                    foreach (int appoitmentId in lstAppoitmentId)
                    {
                        try
                        {
                            var appoitment = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Id == appoitmentId).FirstOrDefault();

                            if (appoitment != null && appoitment.LearnerId == user.Id) // security reason - check if the current user is the student - just the student can pay the appoitment
                            {
                                Models.Payment newPayment = new Models.Payment()
                                {
                                    Added = DateTime.Now,
                                    Amount = appoitment.Price.Value,
                                    AppoitmentId = appoitmentId,
                                    LearnerId = user.Id,
                                    Status = 1
                                };

                                context.Payments.Add(newPayment);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                    context.SaveChanges();

                }
            }
        }
    }
}
