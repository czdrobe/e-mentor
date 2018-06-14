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
    public class AppoitmentRepository : IAppoitmentData
    {
        public SearchResult<Appoitment> GetAppoitments(string useremail, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var appoitments = context.Set<Models.Appoitment>().AsNoTracking().Where(x => x.Learner.Email == useremail);

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


        public void SaveNewAppoitment(string useremail, int teacherId, DateTime startDate, DateTime endDate)
        {
            using (var context = new MeditatiiDbContext())
            {
                var user = context.Set<Models.User>().AsNoTracking().Where(x => x.Email == useremail).FirstOrDefault();
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
    }
}
