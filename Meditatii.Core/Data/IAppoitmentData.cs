using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Data
{
    public interface IAppoitmentData: ILazyLoadable
    {
        SearchResult<Appoitment> GetAppoitments(string useremail, int skip, int take);
        void SaveNewAppoitment(string useremail, int teacherId, DateTime startDate, DateTime endDate);
    }
}
