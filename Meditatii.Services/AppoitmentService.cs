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
    public class AppoitmentService:  IAppoitmentService
    {
        private IAppoitmentData appoitmentData;
        public AppoitmentService(IAppoitmentData appoitmentData)
        {
            this.appoitmentData = appoitmentData;
        }
        
        public SearchResult<Appoitment> GetAppoitments(string useremail, int skip, int take)
        {
            return appoitmentData.GetAppoitments(useremail, skip, take);
        }

        public void SaveNewAppoitment(string useremail, int teacherId, DateTime startDate, DateTime endDate)
        {
            appoitmentData.SaveNewAppoitment(useremail,teacherId,startDate,endDate);
        }
    }
}
