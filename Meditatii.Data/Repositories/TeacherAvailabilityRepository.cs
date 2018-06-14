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
    public class TeacherAvailabilityRepository : ITeacherAvailabilityData
    {
        public SearchResult<TeacherAvailability> GetAvailability(string useremail, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var availabilities = context.Set<Models.TeacherAvailability>().AsNoTracking().Where(x => x.Teacher.Email == useremail);

                    totalRows = availabilities.Count();

                    return
                        new SearchResult<TeacherAvailability> {
                            Entities = MappingHelper.Map<List<TeacherAvailability>>(
                                                                    availabilities.OrderBy(x => x.Id)
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
