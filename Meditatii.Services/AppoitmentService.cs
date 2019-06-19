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
    public class AppoitmentService : IAppoitmentService
    {
        private IAppoitmentData appoitmentData;
        public AppoitmentService(IAppoitmentData appoitmentData)
        {
            this.appoitmentData = appoitmentData;
        }

        public SearchResult<Appoitment> GetAppoitments(bool isTeacher, string useremail, int skip, int take)
        {
            return appoitmentData.GetAppoitments(isTeacher, useremail, skip, take);
        }

        public SearchResult<Appoitment> GetActiveAppoitments(bool isTeacher, string useremail, int skip, int take)
        {
            return appoitmentData.GetActiveAppoitments(isTeacher, useremail, skip, take);
        }

        public SearchResult<Appoitment> GetOldAppoitments(bool isTeacher, string useremail, int skip, int take)
        {
            return appoitmentData.GetOldAppoitments(isTeacher, useremail, skip, take);
        }

        public SearchResult<Appoitment> GetAppoitmentsForDate(int userId, DateTime day)
        {
            return appoitmentData.GetAppoitmentsForDate(userId, day);
        }

        public void SaveNewAppoitment(string useremail, int teacherId, DateTime startDate, DateTime endDate)
        {
            appoitmentData.SaveNewAppoitment(useremail, teacherId, startDate, endDate);
        }

        public void DeleteAppoitment(string appoitmentId)
        {
            appoitmentData.DeleteAppoitment(appoitmentId);
        }

        public Appoitment GetAppoitment(int appoitmentId)
        {
            return appoitmentData.GetAppoitment(appoitmentId);
        }

        public void SaveAppoitment(Appoitment appoitment)
        {
            appoitmentData.SaveAppoitment(appoitment);
        }

        public void SaveChat(int appoitmentId, string useremail, string message)
        {
            appoitmentData.SaveChat(appoitmentId,useremail,message);
        }

        public SearchResult<AppoitmentChat> GetChatForAppoitment(int appoitmentId, int skip, int take)
        {
            return appoitmentData.GetChatForAppoitment(appoitmentId, skip, take);
        }

        public void Payment(int[] lstAppoitmentId, string useremail)
        {
            appoitmentData.Payment(lstAppoitmentId, useremail);
        }

    }
}
