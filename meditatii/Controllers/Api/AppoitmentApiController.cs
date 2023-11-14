using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using Meditatii.Core.Enums;
using System.Collections.Generic;
using System.IO;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using meditatii.web.Utils;
using Meditatii.CoreNew.Enums;

namespace meditatii.Controllers.Api
{
    public class AppoitmentApiController : ApiController
    {
        private IAppoitmentService appoitmentService;
        private IUsersService usersService;

        public AppoitmentApiController(IAppoitmentService appoitmentService, IUsersService usersService)
        {
            this.appoitmentService = appoitmentService;
            this.usersService = usersService;
        }

        [HttpGet]
        [Route("api/appoitments/getappoitments/{page}")]
        public SearchResult<AppoitmentModel> GetAppoitments(int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            //check if the current user is teacher or pupil
            bool isteacher = HttpContext.Current.User.IsInRole(UserType.Teacher.ToString());

            return MappingHelper.Map<SearchResult<AppoitmentModel>>(this.appoitmentService.GetAppoitments(isteacher, System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpGet]
        [Route("api/appoitments/getactiveappoitments/{page}")]
        public SearchResult<AppoitmentModel> GetActiveAppoitments(int page)
        {
            int itemsPerPage = 10;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            //check if the current user is teacher or pupil
            bool isteacher = HttpContext.Current.User.IsInRole(UserType.Teacher.ToString());

            return MappingHelper.Map<SearchResult<AppoitmentModel>>(this.appoitmentService.GetActiveAppoitments(isteacher, System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpGet]
        [Route("api/appoitments/getoldappoitments/{page}")]
        public SearchResult<AppoitmentModel> GetOldAppoitments(int page)
        {
            int itemsPerPage = 100;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            //check if the current user is teacher or pupil
            bool isteacher = HttpContext.Current.User.IsInRole(UserType.Teacher.ToString());

            return MappingHelper.Map<SearchResult<AppoitmentModel>>(this.appoitmentService.GetOldAppoitments(isteacher, System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpPost]
        [Route("api/appoitments/saveappoitment")]
        public void SaveAppoitment(AppoitmentModel newAppoitment)
        {
            this.appoitmentService.SaveNewAppoitment(System.Web.HttpContext.Current.User.Identity.Name, newAppoitment.TeacherId, newAppoitment.StartDate, newAppoitment.EndDate);
        }

        [HttpGet]
        [Route("api/appoitments/deleteappoitment/{appoitmentId}")]
        public void DeleteAppoitment(string appoitmentId)
        {
            //todo check if the user is logged in and if it a teacher or pupil of the appoitment - or admin
            this.appoitmentService.DeleteAppoitment(appoitmentId);
        }

        [HttpPost]
        [Route("api/appoitments/saveappoitmentchat")]
        public void SaveAppoitmentChat(AppoitmentChatModel newChat)
        {
            this.appoitmentService.SaveChat(newChat.AppoitmentId, System.Web.HttpContext.Current.User.Identity.Name, newChat.Message);
        }

        [HttpGet]
        [Route("api/appoitments/GetChatForAppoitment/{appoitmentid}")]
        public SearchResult<AppoitmentChatModel> GetChatForAppoitment(int appoitmentid)
        {
            int page = 1;
            int itemsPerPage = 100;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;

            return MappingHelper.Map<SearchResult<AppoitmentChatModel>>(this.appoitmentService.GetChatForAppoitment(appoitmentid, skip, take));
        }
        
        [HttpGet]
        [Route("api/appoitments/acceptByTeacher/{appoitmentId}")]
        public void AcceptByTeacher(int appoitmentId)
        {
            this.appoitmentService.AcceptByTeacher(appoitmentId);
            //send email to student
            //TODO get the info about the appoitment - about the student and send email
            var appoitment = this.appoitmentService.GetAppoitment(appoitmentId);

            string urlappoitment = ConfigurationManager.AppSettings["WebSite.URL"] + "/u/appoitments";
            string emailbody = EmailHelper.GetEmailTemplate("appoitment-accepted");
            emailbody = emailbody.Replace("<teacher-firstname>", appoitment.Teacher.FirstName);
            emailbody = emailbody.Replace("<teacher-lastname>", appoitment.Teacher.LastName);
            emailbody = emailbody.Replace("<dateandtime>", appoitment.StartDate.ToString());
            emailbody = emailbody.Replace("<appoitmentsurl>", urlappoitment);
            

            //appManager.EmailService.SendAsync(new IdentityMessage { Body = emailbody, Destination = userToSendMessage.Email, Subject = "Mesaj nou - eMeditatii.ro" );
            var email = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["SendEmail.Address"], ConfigurationManager.AppSettings["SendEmail.Alias"]),
                new MailAddress(appoitment.Learner.Email))
            {
                Subject = "Meditatie acceptata de profesor - eMeditatii.ro",
                Body = emailbody,
                IsBodyHtml = true
            };

            email.Bcc.Add(new MailAddress("sentemails@emeditatii.ro"));

            var client = new SmtpClient();
            client.SendCompleted += (s, e) =>
            {
                client.Dispose();
                this.appoitmentService.SetAppoitmentNotification(appoitmentId, AppoitmentNotification.NotificationAccepted, true);
            };
            Task t = Task.Run(async () =>
            {
                await client.SendMailAsync(email);
            });
            t.Wait();
        }

        [HttpGet]
        [Route("api/appoitments/GetRemaningTimeForAppoitment/{appoitmentId}")]
        public int GetRemaningTimeForAppoitment(int appoitmentId)
        {
            //return 50;
            var appoitment = this.appoitmentService.GetAppoitment(appoitmentId);
            return (int)Math.Ceiling(appoitment.EndDate.Subtract(DateTime.Now).TotalMinutes);
        }

        [HttpPost]
        [Route("api/appoitments/savepayments")]
        public void SavePayments([FromBody]int[] lstappoitmentid)
        {
            this.appoitmentService.CreatePaymentWithStatusPaid(lstappoitmentid, HttpContext.Current.User.Identity.Name);
        }

        [Route("api/appoitments/attachment")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadSingleFile()
        {
            var root = HttpContext.Current.Server.MapPath("~/img/attachments");

            if (!Request.Content.IsMimeMultipartContent())
            {
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            }

            var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();

            foreach (var stream in filesReadToProvider.Contents)
            {
                var fileBytes = await stream.ReadAsByteArrayAsync();
            }

            return StatusCode(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/appoitments/getcurrentpayment")]
        public PaymentModel GetCurrentPayment()
        {
            return MappingHelper.Map<PaymentModel>(this.appoitmentService.GetCurrentPayment(HttpContext.Current.User.Identity.Name));
        }


        [HttpGet]
        [Route("api/appoitments/getpaymentforuser")]
        public SearchResult<PaymentModel> GetPaymentsForUser()
        {
            return MappingHelper.Map<SearchResult<PaymentModel>>(this.appoitmentService.GetPaymentsForUser(System.Web.HttpContext.Current.User.Identity.Name));
        }
    }
}


    public class AppoitmentIdMocker
    {
        int[] Lstappoitmentid { get; set;}
    }