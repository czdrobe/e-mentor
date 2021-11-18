using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using Meditatii.CoreNew.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => (isTeacher ? x.Teacher.Email == useremail : x.Learner.Email == useremail) && x.Active);

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

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Active && DateTime.Now <= x.EndDate && (isTeacher ? x.Teacher.Email == useremail : x.Learner.Email == useremail));

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

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Active && x.EndDate < DateTime.Now && (isTeacher ? x.Teacher.Email == useremail : x.Learner.Email == useremail));

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
                    var appoitment = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Active && x.Id == appoitmentId).FirstOrDefault();

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

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Active && x.TeacherId == userId && DbFunctions.TruncateTime(x.StartDate) == DbFunctions.TruncateTime(day));

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

        public SearchResult<Appoitment> GetAppoitmentsForNotificationEmail()
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Active && x.AcceptedByTeacher && x.Payment != null && x.Payment.Status == 1 && x.StartDate > DateTime.Now && x.StartDate <= DbFunctions.AddMinutes(DateTime.Now, 30));

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
        public Payment GetPayment(int paymentId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var payment = context.Set<Models.Payment>().AsNoTracking().Where(x => x.Id == paymentId).FirstOrDefault();

                    return MappingHelper.Map<Payment>(payment);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public Payment GetCurrentPayment(string username)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var payment = context.Set<Models.Payment>().AsNoTracking().Where(x => x.Learner.Email == username && x.Status == 1).OrderByDescending(x => x.Id).FirstOrDefault();

                    return MappingHelper.Map<Payment>(payment);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public SearchResult<Payment> GetPaymentsForUser(string username)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;
                    //get just the payments from the last 12 months
                    DateTime oneYearBack = DateTime.Now.AddMonths(-12);
                    var payments = context.Set<Models.Payment>().AsNoTracking().Where(x => x.Learner.Email == username && x.Status == 1 && x.Added >= oneYearBack);

                    totalRows = payments.Count();

                    return
                        new SearchResult<Payment>
                        {
                            Entities = MappingHelper.Map<List<Payment>>(
                                                                    payments.OrderByDescending(x => x.Id).ToList()),
                            TotalRows = totalRows
                        };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public SearchResult<Appoitment> GetAppoitmentsForPaymentId(int paymentId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Active && x.PaymentId == paymentId);

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

        public void SetAppoitmentNotification(int appoitmentId, AppoitmentNotification typeOfNotification, Boolean value)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    string field = "[NotificationNew]";
                    switch (typeOfNotification)
                    {
                        case AppoitmentNotification.NotificationNew:
                            field = "[NotificationNew]";
                            break;
                        case AppoitmentNotification.NotificationAccepted:
                            field = "[NotificationAccepted]";
                            break;
                        case AppoitmentNotification.NotificationPaid:
                            field = "[NotificationPaid]";
                            break;
                        case AppoitmentNotification.NotificationRemainder:
                            field = "[NotificationRemainder]";
                            break;
                        default:
                            break;
                    }
                    string sqlcommand = "update Appoitment set " + field + " = " + ( value ? "1" : "0" ) + " where Id = " + appoitmentId;
                    context.Database.ExecuteSqlCommand(sqlcommand);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveAppoitment(Appoitment appoitment)
        { //TODO: review this code - not working
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

        public void UpdateSessionIdOfAppoitment(int appoitmentId, string sessionId)
        {
            using (var context = new MeditatiiDbContext())
            {
                var appoitment = context.Set<Models.Appoitment>().Where(x => x.Id == appoitmentId).FirstOrDefault();

                if (appoitment != null)
                {
                    appoitment.SessionId = sessionId;
                    context.SaveChanges();
                }
            }
        }

        public int SaveNewAppoitment(string useremail, int teacherId, DateTime startDate, DateTime endDate)
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
                            Price = teacher.Price,
                            Active = true,
                            NotificationAccepted = false,
                            NotificationNew = false,
                            NotificationPaid = false,
                            NotificationRemainder = false
                        };

                        context.Appoitment.Add(newAppoitment);
                        context.SaveChanges();

                        return newAppoitment.Id;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return 0;
            }
        }

        public void DeleteAppoitment(string appoitmentId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("update Appoitment set active = 0 where Id = " + appoitmentId);
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

        public void CreatePaymentWithStatusPaid(int[] lstAppoitmentId, string useremail)
        {
            decimal totalAmoutToPay = 0;
            if (lstAppoitmentId == null || lstAppoitmentId.Length <= 0)
                return;

            using (var context = new MeditatiiDbContext())
            {
                var user = context.Set<Models.User>().AsNoTracking().Where(x => x.Email == useremail).FirstOrDefault();

                if (user != null)
                {
                    Models.Payment newPayment = new Models.Payment()
                    {
                        Added = DateTime.Now,
                        Amount = 0,
                        LearnerId = user.Id,
                        Status = 0
                    };

                    context.Payments.Add(newPayment);
                    context.SaveChanges();

                    foreach (int appoitmentId in lstAppoitmentId)
                    {
                        try
                        {
                            var appoitment = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Id == appoitmentId).FirstOrDefault();

                            if (appoitment != null && appoitment.LearnerId == user.Id) // security reason - check if the current user is the student - just the student can pay the appoitment
                            {
                                appoitment.PaymentId = newPayment.Id;
                                totalAmoutToPay += appoitment.Price.Value;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    newPayment.Amount = totalAmoutToPay;

                    context.SaveChanges();

                }
            }
        }

        public Payment CreatePaymentForSubscription(int product, decimal amout, string useremail)
        {

            if (product <= 0)
                return null; 

            using (var context = new MeditatiiDbContext())
            {

                var user = context.Set<Models.User>().AsNoTracking().Where(x => x.Email == useremail).FirstOrDefault();

                if (user != null)
                {
                    try
                    {
                        Models.Payment newPayment = new Models.Payment()
                        {
                            Added = DateTime.Now,
                            Amount = amout,
                            LearnerId = user.Id,
                            Product = product,
                            Status = 0
                        };

                        context.Payments.Add(newPayment);
                        context.SaveChanges();

                        return MappingHelper.Map<Payment>(newPayment);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return null;

        }
        public Payment CreatePayment(List<int> lstAppoitmentId, string useremail)
        {
           // Payment status can be 
           // 0
            decimal totalAmoutToPay = 0;
            if (lstAppoitmentId == null || lstAppoitmentId.Count <= 0)
                return null;

            using (var context = new MeditatiiDbContext())
            {

                var user = context.Set<Models.User>().AsNoTracking().Where(x => x.Email == useremail).FirstOrDefault();

                    if (user != null)
                    {
                    try
                    {
                        Models.Payment newPayment = new Models.Payment()
                        {
                            Added = DateTime.Now,
                            Amount = 0,
                            LearnerId = user.Id,
                            Status = 0
                        };

                        context.Payments.Add(newPayment);
                        context.SaveChanges();

                        foreach (int appoitmentId in lstAppoitmentId)
                        {
                            try
                            {
                                var appoitment = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Id == appoitmentId).FirstOrDefault();

                                if (appoitment != null && appoitment.LearnerId == user.Id) // security reason - check if the current user is the student - just the student can pay the appoitment
                                {
                                    totalAmoutToPay += appoitment.Price.Value;
                                    
                                    context.Database.ExecuteSqlCommand("Update Appoitment Set PaymentId=@paymentId where Id=@id",
                                        new SqlParameter("@paymentId", newPayment.Id),
                                        new SqlParameter("@id", appoitmentId));
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        newPayment.Amount = totalAmoutToPay;
               
                        context.SaveChanges();

                        return MappingHelper.Map<Payment>(newPayment);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return null;
        }

        public void RegisterPayment(int paymentId, string crc, string paymentTimeStamp)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand(
                        "update[dbo].[Payment] set status=1, PaymentCRC=@PaymentCRC, PaymentTimeStamp=@PaymentTimeStamp,Updated= @Updated where Id=@Id",
                        new SqlParameter("@PaymentCRC", crc),
                        new SqlParameter("@PaymentTimeStamp", paymentTimeStamp),
                        new SqlParameter("@Updated", DateTime.Now),
                        new SqlParameter("@Id", paymentId));

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void RegisterPaymentLog(string paymentId, string message)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand(
                        "INSERT INTO [dbo].[PaymentLogs] ([message], [paymentId]) VALUES(@message,@paymentid)", 
                        new SqlParameter("@message", message),
                        new SqlParameter("@paymentid", paymentId));

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void AcceptByTeacher(int appoitmentId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand(
                        "Update [Appoitment] set [AcceptedByTeacher] = 1 where Id = @id",
                        new SqlParameter("@id", appoitmentId));

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
