using Meditatii.Core.Entities;
using Meditatii.CoreNew.Enums;
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
        SearchResult<Appoitment> GetAppoitmentsForNotificationEmail();
        SearchResult<Appoitment> GetAppoitmentsForPaymentId(int paymentId);
        Payment GetPayment(int paymentId);
        Payment GetCurrentPayment(string username);
        SearchResult<Payment> GetPaymentsForUser(string username);
        Appoitment GetAppoitment(int appoitmentId);
        int SaveNewAppoitment(string useremail, int teacherId, DateTime startDate, DateTime endDate);
        void SetAppoitmentNotification(int appoitmentId, AppoitmentNotification typeOfNotification, Boolean value);
        void SaveAppoitment(Appoitment appoitment);
        void UpdateSessionIdOfAppoitment(int appoitmentId, string sessionId);
        void DeleteAppoitment(string appoitmentId);
        void SaveChat(int appoitmentId, string useremail, string message);
        SearchResult<AppoitmentChat> GetChatForAppoitment(int appoitmentId, int skip, int take);
        void CreatePaymentWithStatusPaid(int[] lstAppoitmentId, string useremail);
        Payment CreatePayment(List<int> lstAppoitmentId, string useremail);
        Payment CreatePaymentForSubscription(int product, decimal amout, string useremail);
        void RegisterPayment(int paymentId, string crc, string paymentTimeStamp);
        void RegisterPaymentLog(string paymentId, string message);
        void AcceptByTeacher(int appoitmentId);
    }
}
