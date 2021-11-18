using Meditatii.Core;
using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.CoreNew.Enums;
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

        public SearchResult<Appoitment> GetAppoitmentsForNotificationEmail()
        {
            return appoitmentData.GetAppoitmentsForNotificationEmail();
        }

        public SearchResult<Appoitment> GetAppoitmentsForPaymentId(int paymentId)
        {
            return appoitmentData.GetAppoitmentsForPaymentId(paymentId);
        }

        public Payment GetPayment(int paymentId)
        {
            return appoitmentData.GetPayment(paymentId);
        }

        public Payment GetCurrentPayment(string username)
        {
            return appoitmentData.GetCurrentPayment(username);
        }

        public SearchResult<Payment> GetPaymentsForUser(string username)
        {
            return appoitmentData.GetPaymentsForUser(username);
        }

        public void SetAppoitmentNotification(int appoitmentId, AppoitmentNotification typeOfNotification, Boolean value)
        {
            appoitmentData.SetAppoitmentNotification(appoitmentId, typeOfNotification, value);
        }

        public int SaveNewAppoitment(string useremail, int teacherId, DateTime startDate, DateTime endDate)
        {
            return appoitmentData.SaveNewAppoitment(useremail, teacherId, startDate, endDate);
        }

        public void DeleteAppoitment(string appoitmentId)
        {
            appoitmentData.DeleteAppoitment(appoitmentId);
        }

        public Appoitment GetAppoitment(int appoitmentId)
        {
            return appoitmentData.GetAppoitment(appoitmentId);
        }

        public void UpdateSessionIdOfAppoitment(int appoitmentId, string sessionId)
        {
            appoitmentData.UpdateSessionIdOfAppoitment(appoitmentId, sessionId);
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

        public void CreatePaymentWithStatusPaid(int[] lstAppoitmentId, string useremail)
        {
            appoitmentData.CreatePaymentWithStatusPaid(lstAppoitmentId, useremail);
        }

        public Payment CreatePaymentForSubscription(int product, decimal amout, string useremail)
        {
            return appoitmentData.CreatePaymentForSubscription(product, amout, useremail);
        }

        public Payment CreatePayment(List<int> lstAppoitmentId, string useremail)
        {
            return appoitmentData.CreatePayment(lstAppoitmentId, useremail);
        }

        public void RegisterPayment(int paymentId, string crc, string paymentTimeStamp)
        {
            appoitmentData.RegisterPayment(paymentId, crc, paymentTimeStamp);
        }

        public void RegisterPaymentLog(string paymentId, string message)
        {
            appoitmentData.RegisterPaymentLog(paymentId, message);
        }


        public void AcceptByTeacher(int appoitmentId)
        {
            appoitmentData.AcceptByTeacher(appoitmentId);
        }
    }
}
