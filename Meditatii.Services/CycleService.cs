using Meditatii.Core;
using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Services
{
    public class CycleService:  ICycleService
    {
        private ICycleData cycleData;
        public CycleService(ICycleData cycleData)
        {
            this.cycleData = cycleData;
        }
        
        public IEnumerable<Cycle> GetCycles()
        {
            return cycleData.GetAll();
        }

        public void SaveCyclesForUser(string useremail, List<Cycle> lstCycles)
        {
            cycleData.RemoveAllCyclesForUser(useremail);
            foreach (var cycle in lstCycles)
            {
                cycleData.SaveCycleForUser(useremail, cycle);
            }
        }
    }
}
