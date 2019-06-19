using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core
{
    public interface IAppoitmentService:ILazyLoadable
    {
        SearchResult<Appoitment> GetAppoitments(bool isTeacher, string useremail, int skip, int take);
        SearchResult<Appoitment> GetActiveAppoitments(bool isTeacher, string useremail, int skip, int take);
        SearchResult<Appoitment> GetOldAppoitments(bool isTeacher, string useremail, int skip, int take);
        SearchResult<Appoitment> GetAppoitmentsForDate(int userId, DateTime day);
        Appoitment GetAppoitment(int appoitmentId);
        void SaveNewAppoitment(string useremail, int teacherId, DateTime startDate, DateTime endDate);
        void SaveAppoitment(Appoitment appoitment);
        void DeleteAppoitment(string appoitmentId);
        void SaveChat(int appoitmentId, string useremail, string message);
        SearchResult<AppoitmentChat> GetChatForAppoitment(int appoitmentId, int skip, int take);
        void Payment(int[] lstAppoitmentId, string useremail);
    }
}
