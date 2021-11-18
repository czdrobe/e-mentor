using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Repositories
{
    public class ReportRepository : IReportData
    {
        public SearchResult<ReportTeacherAppoitment> GetReportTeacherAppoitment(string useremail, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    List<ReportTeacherAppoitment> reportAppointmentList = new List<ReportTeacherAppoitment>();
                    int totalRows = 0;
                    var query = from a in context.Appoitment
                                where a.Payment.Status == 1 && a.Teacher.Email == useremail
                                select a;

                    //var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Teacher.Email == useremail);
                    var appoitments = query.ToList();

                    totalRows = appoitments.Count();

                    foreach (Appoitment appoitment in MappingHelper.Map<List<Appoitment>>(appoitments.OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList()))
                    {
                        ReportTeacherAppoitment app = new ReportTeacherAppoitment()
                        {
                            AppoitmentId = appoitment.Id,
                            StartDate = appoitment.StartDate,
                            EndDate = appoitment.EndDate,
                            Price = appoitment.Price,
                            Learner =   appoitment.Learner,
                            Payment = appoitment.Payment
                        };

                        //search for payouts
                        var payouts = context.Set<Models.PaymentOut>().AsNoTracking().Where(x => x.AppoitmentId == appoitment.Id).ToList();
                        
                        foreach (Models.PaymentOut pay in payouts)
                        {
                            switch (pay.Type)
                            {
                                case 1:
                                    app.PayOutTeacher = pay.Added;
                                    break;
                                case 2:
                                    app.PayOutPlatform = pay.Added;
                                    break;
                                case 3:
                                    app.PayOutTax = pay.Added;
                                    break;
                            }
                        }

                        reportAppointmentList.Add(app);
                    }

                    return
                        new SearchResult<ReportTeacherAppoitment>
                        {
                            Entities = MappingHelper.Map<List<ReportTeacherAppoitment>>(reportAppointmentList)
                                                                   ,
                            TotalRows = totalRows
                        };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public decimal GetBalanceForTeacher(string useremail)
        {
            decimal balance = 0;
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    decimal? appoitmentAmount = context.Database.SqlQuery<decimal?>(@"
                                                        select 
                                                            sum(amount) 
                                                        from 
                                                            Appoitment 
                                                            inner join Payment on Appoitment.PaymentId = Payment.Id 
                                                            inner join [User] as Teacher on Teacher.Id = Appoitment.TeacherId 
                                                         where 
                                                            Payment.Status = 1 and
                                                            Teacher.email=@email", new SqlParameter("@email", useremail)).FirstOrDefault<decimal?>();
                    if (appoitmentAmount == null)
                        return 0;

                    decimal? payoutAmount = context.Database.SqlQuery<decimal?>(@"
                                                        select 
                                                            sum(Paymentout.Amount)
                                                        from 
                                                            Appoitment 
                                                            inner join PaymentOut on Appoitment.Id = PaymentOut.AppoitmentId 
                                                            inner join Payment on Appoitment.PaymentId = Payment.Id
                                                            inner join [User] as Teacher on Teacher.Id = Appoitment.TeacherId 
                                                        where 
                                                            Payment.Status = 1 and
                                                            Teacher.email=@email", new SqlParameter("@email", useremail)).FirstOrDefault<decimal?>();
                    if (payoutAmount == null)
                        return appoitmentAmount.Value;

                    balance = appoitmentAmount.Value - payoutAmount.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return balance;
        }

    }
}
