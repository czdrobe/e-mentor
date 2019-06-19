using Meditatii.Core;
using Meditatii.Core.Entities;
using System;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace meditatii.Controllers
{
    public class RoomController : BaseController
    {
        public RoomController(Func<IAppoitmentService> appoitmentService) : base(appoitmentService)
        {

        }

        private IAppoitmentService appoitmentService => GetService<IAppoitmentService>();

        public ActionResult Index(int appoitmentid)
        {
            string tokenid = string.Empty;
            string sessionid = string.Empty;
            int remainingTime = -1;

            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            Appoitment appoitment = appoitmentService.GetAppoitment(appoitmentid);
            if (appoitment != null &&
                (appoitment.Teacher.Email == System.Web.HttpContext.Current.User.Identity.Name || appoitment.Learner.Email == System.Web.HttpContext.Current.User.Identity.Name)) //security check
            {
                int apiKey = Convert.ToInt32(WebConfigurationManager.AppSettings["OpenTok.apiKey"]);
                string apiSecret = WebConfigurationManager.AppSettings["OpenTok.apiSecret"];
                var OpenTok = new OpenTokSDK.OpenTok(apiKey, apiSecret);

                ViewBag.apiKey = apiKey;

                var jsSerializer = new JavaScriptSerializer();

                if (!String.IsNullOrEmpty(appoitment.SessionId))
                {
                    //get session and tokens from database
                    sessionid = appoitment.SessionId;
                }
                else
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var newSession = OpenTok.CreateSession();

                    sessionid = newSession.Id;
                    appoitment.SessionId = newSession.Id;
                    appoitmentService.SaveAppoitment(appoitment);
                }

                //calculate the seconds expire time for token - based on the the booked time for current meditation
                double expireToken = (DateTime.UtcNow.AddSeconds(60) - UnixEpoch).TotalSeconds; ; ///seconds

                //get the token for the current user
                if (appoitment.Teacher.Email == System.Web.HttpContext.Current.User.Identity.Name) //check if this is a teacher
                {
                    tokenid = OpenTok.GenerateToken(sessionid, OpenTokSDK.Role.PUBLISHER, expireToken, appoitment.Teacher.Id.ToString());
                    ViewBag.Name = appoitment.Teacher.LastName + ' ' + appoitment.Teacher.FirstName;
                }
                else
                {
                    tokenid = OpenTok.GenerateToken(sessionid, OpenTokSDK.Role.PUBLISHER, expireToken, appoitment.Teacher.Id.ToString());
                    ViewBag.Name = appoitment.Learner.LastName + ' ' + appoitment.Learner.FirstName;
                }

                remainingTime = (int)Math.Ceiling(appoitment.EndDate.Subtract(DateTime.Now).TotalMinutes);
            }

            //TODO debug reason:
            if (Request["debug"] == "true")
            {
                remainingTime = 50;
            }

            //read from db first
            ViewBag.AppoitmentId = appoitmentid;
            ViewBag.SessionId = sessionid;
            ViewBag.TokenId = tokenid;

            ViewBag.remainingTime = remainingTime;

            return View();
        }
    }

    public class Tokens
    {
        public string TeacherTokenId { get; set; }
        public string LearnerTokenId { get; set; }
    }
}