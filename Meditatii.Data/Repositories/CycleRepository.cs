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
    }
}
