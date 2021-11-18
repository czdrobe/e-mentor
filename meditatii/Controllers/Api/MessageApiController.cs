using meditatii.Models;
using meditatii.web.Utils;
using Meditatii.Core;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace meditatii.Controllers.Api
{
    public class MessageApiController : ApiController
    {
        private IMessageService messagesService;
        private IUsersService usersService;

        public MessageApiController(IMessageService messagesService, IUsersService usersService)
        {
            this.messagesService = messagesService;
            this.usersService = usersService;
        }

        [HttpGet]
        [Route("api/messages/getmessages/{mentorId}/{page}")]
        public SearchResult<MessageModel> GetMessages(int mentorId, int page)
        {
            int itemsPerPage = 100;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;
            return MappingHelper.Map<SearchResult<MessageModel>>(this.messagesService.GetMessages(mentorId, System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpGet]
        [Route("api/messages/getmessagesbymentorcode/{mentorId}/{page}")]
        public SearchResult<MessageModel> GetMessagesByMentorCode(string mentorId, int page)
        {
            int itemsPerPage = 100;
            int skip = (page - 1) * itemsPerPage;
            int take = itemsPerPage;
            return MappingHelper.Map<SearchResult<MessageModel>>(this.messagesService.GetMessages(Security.DecryptID(mentorId), System.Web.HttpContext.Current.User.Identity.Name, skip, take));
        }

        [HttpGet]
        [Route("api/messages/listofmentors")]
        public List<MentorMessageModel> GetListOfMentors()
        {
            return MappingHelper.Map<List<MentorMessageModel>>(this.messagesService.GetListOfMenters(System.Web.HttpContext.Current.User.Identity.Name));
        }

        [HttpGet]
        [Route("api/messages/getlistofuserswithmessage")]
        public List<MentorMessageModel> GetListOfUsersWithMessage()
        {
            return MappingHelper.Map<List<MentorMessageModel>>(this.messagesService.GetListOfMenters(System.Web.HttpContext.Current.User.Identity.Name));
        }

        [HttpPost]
        [Route("api/messages/saveMessageForRequest")]
        public void saveMessageForRequest([FromBody] MessageForRequest newMessage)
        {
            try
            {
                var userfromMessage = this.usersService.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
                var request = this.usersService.GetRequest(Security.DecryptID((newMessage.requestId)));
                if (request != null && userfromMessage != null)
                {
                    this.messagesService.SaveNewMessage(System.Web.HttpContext.Current.User.Identity.Name, request.LearnerId, newMessage.message);

                    string urlreadmessage = ConfigurationManager.AppSettings["WebSite.URL"] + "/u/messages?user=" + Security.EncryptID(userfromMessage.Id);

                    //send notification email
                    string emailbody = EmailHelper.GetEmailTemplate("newmessage");
                    emailbody = emailbody.Replace("<from-firstname>", userfromMessage.FirstName);
                    emailbody = emailbody.Replace("<from-lastname>", userfromMessage.LastName);
                    emailbody = emailbody.Replace("<readmessageurl>", urlreadmessage);


                    //appManager.EmailService.SendAsync(new IdentityMessage { Body = emailbody, Destination = userToSendMessage.Email, Subject = "Mesaj nou - eMeditatii.ro" );
                    var email = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["SendEmail.Address"], ConfigurationManager.AppSettings["SendEmail.Alias"]),
                        new MailAddress(request.Learner.Email))
                    {
                        Subject = "Mesaj nou - eMeditatii.ro",
                        Body = emailbody,
                        IsBodyHtml = true
                    };

                    var client = new SmtpClient();
                    client.SendCompleted += (s, e) =>
                    {
                        client.Dispose();
                    };
                    Task t = Task.Run(async () =>
                    {
                        await client.SendMailAsync(email);
                    });
                    t.Wait();

                }

            }
            catch (Exception ex)
            {
                var path = HostingEnvironment.MapPath("~") + "notification.log";
                File.AppendAllLines(path, new string[] { ex.Message + ex.StackTrace.ToString() });
            }

        }

        [HttpPost]
        [Route("api/messages/savenewmessage")]
        public void SaveNewMessage(MessageModel newMessage)
        {
            try
            {
                var userToSendMessage = this.usersService.GetUser(Security.DecryptID((newMessage.ToUserId)));
                var userfromMessage = this.usersService.GetUser(System.Web.HttpContext.Current.User.Identity.Name);

                if (userToSendMessage != null && userfromMessage != null)
                {
                    this.messagesService.SaveNewMessage(System.Web.HttpContext.Current.User.Identity.Name, Security.DecryptID(newMessage.ToUserId), newMessage.Body);

                    string urlreadmessage = ConfigurationManager.AppSettings["WebSite.URL"] + "/u/messages?user=" + Security.EncryptID(userfromMessage.Id);

                    //send notification email
                    string emailbody = EmailHelper.GetEmailTemplate("newmessage");
                    emailbody = emailbody.Replace("<from-firstname>", userfromMessage.FirstName);
                    emailbody = emailbody.Replace("<from-lastname>", userfromMessage.LastName);
                    emailbody = emailbody.Replace("<readmessageurl>", urlreadmessage);


                    //appManager.EmailService.SendAsync(new IdentityMessage { Body = emailbody, Destination = userToSendMessage.Email, Subject = "Mesaj nou - eMeditatii.ro" );
                    var email = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["SendEmail.Address"], ConfigurationManager.AppSettings["SendEmail.Alias"]),
                        new MailAddress(userToSendMessage.Email))
                    {
                        Subject = "Mesaj nou - eMeditatii.ro",
                        Body = emailbody,
                        IsBodyHtml = true
                    };

                    var client = new SmtpClient();
                    client.SendCompleted += (s, e) =>
                    {
                        client.Dispose();
                    };
                    Task t = Task.Run(async () =>
                    {
                        await client.SendMailAsync(email);
                    });
                    t.Wait();
                }

            }
            catch (Exception ex)
            {
                var path = HostingEnvironment.MapPath("~") + "notification.log";
                File.AppendAllLines(path, new string[] { ex.Message + ex.StackTrace.ToString()});
            }
        }
    }

    public class MessageForRequest
    {
        public string requestId { get; set; }
        public string message { get; set; }
    }
}