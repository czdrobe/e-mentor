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
    public class CycleRepository : ICycleData
    {
        public IEnumerable<Cycle> GetAll()
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var cycles = context.Set<Models.Cycle>()
                        .AsNoTracking()
                        .ToList();
                    return MappingHelper.Map<List<Cycle>>(cycles);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveCycleForUser(string username, Cycle cycle)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.User>()
                        .AsQueryable();
                    var user = MappingHelper.Map<User>(users.Where(x => x.Email == username).FirstOrDefault());

                    context.Database.ExecuteSqlCommand("insert into UserCycle (UserId, CycleId) Values (" + user.Id + ", " + cycle.Id + ")");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void RemoveAllCyclesForUser(string useremail)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("delete from UserCycle where UserId in (select id from [user] where UserName = '" + useremail + "')");
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
