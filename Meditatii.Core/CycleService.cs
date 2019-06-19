using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core
{
    public interface ICycleService:ILazyLoadable
    {
        IEnumerable<Cycle> GetCycles();

        void SaveCyclesForUser(string username, List<Cycle> lstCycles);
    }
}
