using meditatii.web.Utils;
using Meditatii.Core;
using Meditatii.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace meditatii.web.ScheduledTasks
{
    public class SendAppoitmentNotificationJob : IJob
    {
        private IAppoitmentService appoitmentService;

        public SendAppoitmentNotificationJob(IAppoitmentService appoitmentService)
        {
            this.appoitmentService = appoitmentService;
        }

        public SendAppoitmentNotificationJob()
        {
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //get all appoitment which start in the half an hour
            //if the time is 17 pm start date > current date and <
            // 

            var path = HostingEnvironment.MapPath("~") + "notification.log";

            this.appoitmentService = DependencyResolver.Current.GetService<IAppoitmentService>();

            var lstAppoitments = appoitmentService.GetAppoitmentsForNotificationEmail();

            File.AppendAllLines(path, new string[] { "Start at " + DateTime.Now.ToString() + " appoitments:" + lstAppoitments.TotalRows.ToString() });

            if (lstAppoitments.TotalRows > 0)
            {
                try
                {
                    string sendemail_address = ConfigurationManager.AppSettings["SendEmail.Address"];
                    string sendemail_alias = ConfigurationManager.AppSettings["SendEmail.Alias"];

                    var client = new SmtpClient();

                    string emailbody = EmailHelper.GetEmailTemplate("appointmentnotification");

                    foreach (var appoitment in lstAppoitments.Entities)
                    {
                        if (!appoitment.NotificationRemainder)
                        {
                            string subject = "Notificare incepere meditaie: " + appoitment.StartDate.ToString("yyyy-MM-dd HH:mm");
                            //student
                            string student_emailbody = emailbody.Replace("<startdateandtime>", appoitment.StartDate.ToString("yyyy-MM-dd HH:mm"));
                            student_emailbody = student_emailbody.Replace("<nameofparticipant>", appoitment.Teacher.FirstName + " " + appoitment.Teacher.LastName);
                            var emailStudent = new MailMessage(new MailAddress(sendemail_address, sendemail_alias), new MailAddress(appoitment.Learner.Email))
                            {
                                Subject = subject,
                                Body = student_emailbody,
                                IsBodyHtml = true
                            };

                            await client.SendMailAsync(emailStudent);

                            //teacher
                            string teacher_emailbody = emailbody.Replace("<startdateandtime>", appoitment.StartDate.ToString("yyyy-MM-dd HH:mm"));
                            teacher_emailbody = teacher_emailbody.Replace("<nameofparticipant>", appoitment.Learner.FirstName + " " + appoitment.Learner.LastName);
                            var emailTeacher = new MailMessage(new MailAddress(sendemail_address, sendemail_alias), new MailAddress(appoitment.Teacher.Email))
                            {
                                Subject = subject,
                                Body = teacher_emailbody,
                                IsBodyHtml = true
                            };

                            client.SendCompleted += (s, e) =>
                            {
                                client.Dispose();
                                this.appoitmentService.SetAppoitmentNotification(appoitment.Id, Meditatii.CoreNew.Enums.AppoitmentNotification.NotificationRemainder, true);
                            };

                            await client.SendMailAsync(emailTeacher);
                        }

                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllLines(path, new string[] { ex.Message + ex.StackTrace.ToString() });
                }
            }

            File.AppendAllLines(path, new string[] { "End at " + DateTime.Now.ToString() });

        }
    }
}