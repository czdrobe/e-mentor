using meditatii.web.Utils;
using Meditatii.Core;
using Meditatii.Core.Entities;
using MobilpayEncryptDecrypt;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using meditatii.Models;
using Meditatii.Core.Helpers;

namespace meditatii.Controllers
{
    public class PaymentController : BaseController
    {
        /*public PaymentController(Func<IUsersService> usersService, Func<IAppoitmentService> appoitmentService) : base(usersService, appoitmentService)
        {

        }
        private IUsersService usersService => GetService<IUsersService>();
        private IAppoitmentService appoitmentService => GetService<IAppoitmentService>();
        */

        public PaymentController(IAppoitmentService appoitmentService, IUsersService usersService)
        {
            this.appoitmentService = appoitmentService;
            this.usersService = usersService;
        }
        private IAppoitmentService appoitmentService;

        //period can be 31, 93, 365
        //for now the cost is 29 ron for 31, 59 ron for 93, 149 ron for 365
        public ActionResult Index(string period)
        {
            ViewBag.user = CurrentUser;

            string signature = AppSettings.MobilPayIsLive ? AppSettings.MobilPayLiveSignature : AppSettings.MobilPaySandboxSignature;
            string X509CertificateFilePath = AppSettings.MobilPayIsLive ? AppSettings.MobilPayLiveCertFilePath : AppSettings.MobilPaySandboxCertFilePath;
            string paymentUrl = AppSettings.MobilPayIsLive ? AppSettings.MobilPayLiveURL : AppSettings.MobilPaySandboxURL;

            if (String.IsNullOrEmpty(period))
            {
                return View();
            }
            //string[] lstAppoitmentsId = appoitmentsid.Split(',');

            //List<int> ilstAppoitmentsId = lstAppoitmentsId.Select(int.Parse).ToList();
            decimal amount = 0;
            string description = "";

            if (period == "31")
            {
                amount = 29;
                description = "Abonament 1 luna";
            }
            else if (period == "93")
            {
                amount = 59;
                description = "Abonament 3 luni";
            }
            else if (period == "365")
            {
                amount = 149;
                description = "Abonament 1 an";
            }
            
            Payment oPayment = appoitmentService.CreatePaymentForSubscription(int.Parse(period), amount, System.Web.HttpContext.Current.User.Identity.Name);

            User paymentUser = usersService.GetUser(System.Web.HttpContext.Current.User.Identity.Name);

            MobilpayEncrypt encrypt = new MobilpayEncrypt();

            Mobilpay_Payment_Request_Card card = new Mobilpay_Payment_Request_Card();
            Mobilpay_Payment_Invoice invoice = new Mobilpay_Payment_Invoice();
            Mobilpay_Payment_Address billing = new Mobilpay_Payment_Address();
            Mobilpay_Payment_Address shipping = new Mobilpay_Payment_Address();
            Mobilpay_Payment_Invoice_Item itmm = new Mobilpay_Payment_Invoice_Item();
            Mobilpay_Payment_Invoice_Item itmm1 = new Mobilpay_Payment_Invoice_Item();
            Mobilpay_Payment_ItemCollection itmColl = new Mobilpay_Payment_ItemCollection();
            Mobilpay_Payment_Exchange_RateCollection exColl = new Mobilpay_Payment_Exchange_RateCollection();
            Mobilpay_Payment_Exchange_Rate ex = new Mobilpay_Payment_Exchange_Rate();
            Mobilpay_Payment_Request_Contact_Info ctinfo = new Mobilpay_Payment_Request_Contact_Info();
            Mobilpay_Payment_Confirm conf = new Mobilpay_Payment_Confirm();
            Mobilpay_Payment_Request_Url url = new Mobilpay_Payment_Request_Url();

            MobilpayEncryptDecrypt.MobilpayEncryptDecrypt encdecr = new MobilpayEncryptDecrypt.MobilpayEncryptDecrypt();
            card.OrderId = oPayment.Id.ToString();
            card.Type = "card";
            card.Signature = signature;

            url.ConfirmUrl = AppSettings.MobilPayConfirmURL;
            url.ReturnUrl = AppSettings.MobilPayReturnURL;

            //card.Service = service;
            card.Url = url;
            card.TimeStamp = DateTime.Now.ToString("yyyyMMddhhmmss");
            invoice.Amount = oPayment.Amount;
            invoice.Currency = "RON";
            invoice.Details = description;
            billing.FirstName = paymentUser.FirstName;
            billing.LastName = paymentUser.LastName;
            billing.Email = paymentUser.Email;

            shipping.FirstName = paymentUser.FirstName;
            shipping.LastName = paymentUser.LastName;
            shipping.Email = paymentUser.Email;

            ctinfo.Billing = billing;
            ctinfo.Shipping = shipping;
            invoice.ContactInfo = ctinfo;
            card.Invoice = invoice;
            encrypt.Data = encdecr.GetXmlText(card);
            encrypt.X509CertificateFilePath = HttpRuntime.AppDomainAppPath + X509CertificateFilePath;
            encdecr.Encrypt(encrypt);

            System.Collections.Specialized.NameValueCollection coll = new System.Collections.Specialized.NameValueCollection();
            coll.Add("data", encrypt.EncryptedData);
            coll.Add("env_key", encrypt.EnvelopeKey);
            ViewBag.data = encrypt.EncryptedData;
            ViewBag.env_key = encrypt.EnvelopeKey;
            ViewBag.paymentUrl = paymentUrl;

            return View();
        }

        [ActionName("thank-you")]
        public ActionResult ThankYou()
        {
            ViewBag.user = CurrentUser;
            return View();
        }

        [ActionName("confirm")]
        [HttpPost]
        public ActionResult Confirm()
        {
            //ViewBag.user = CurrentUser;
            string privateKeyFileName = AppSettings.MobilPayIsLive ? AppSettings.MobilPayLivePrivateKeyFileName : AppSettings.MobilPaySandboxPrivateKeyFileName;

            string errorCode = "0";
            string errorMessage = "";
            string errorType = "";
            string message;

            string paymentId = "";

            if (((Request.Form.Get("data") == null || Request.Form.Get("data") == "")) & (((Request.Form.Get("env_key") == null || Request.Form.Get("env_key") == ""))))
            {
                errorType = "0x02";
                errorCode = "0x300000f5";
                errorMessage = "mobilpay.ro posted invalid parameters";
            }
            else
            {
                try
                {
                    string textxml = Request.Form.Get("data");
                    string env_key = Request.Form.Get("env_key");
                    MobilpayEncryptDecrypt.MobilpayEncryptDecrypt encderypt = new MobilpayEncryptDecrypt.MobilpayEncryptDecrypt();
                    MobilpayDecrypt decrypt = new MobilpayDecrypt();
                    decrypt.Data = textxml;
                    decrypt.EnvelopeKey = env_key;
                    decrypt.PrivateKeyFilePath = HttpRuntime.AppDomainAppPath + privateKeyFileName;
                    encderypt.Decrypt(decrypt);
                    Mobilpay_Payment_Request_Card card = new Mobilpay_Payment_Request_Card();
                    card = encderypt.GetCard(decrypt.DecryptedData);

                    paymentId = card.OrderId;

                    switch (card.Confirm.Action)
                    {
                        case "confirmed":
                            {
                                errorMessage = card.Confirm.Crc;
                                if (card.Confirm.Original_Amount == card.Confirm.Processed_Amount)
                                {
                                    int ipaymentId = 0;
                                    int.TryParse(paymentId, out ipaymentId);

                                    appoitmentService.RegisterPayment(ipaymentId, card.Confirm.Crc, card.Confirm.TimeStamp);

                                    //send email to the teacher - as the meditation has been paid
                                    //TODO finishing the sending email
                                    //var lstAppoitments = appoitmentService.GetAppoitmentsForPaymentId(ipaymentId);
                                    var payment = appoitmentService.GetPayment(ipaymentId);
                                    var user = usersService.UpdateSubscriptionPeriod(System.Web.HttpContext.Current.User.Identity.Name, payment.Product);

                                    //TODO:need to send confirmation email - the subscription
                                    try
                                    {
                                        string emailbody = EmailHelper.GetEmailTemplate("subscription-paid");

                                        string newBody = emailbody.Replace("<firstname>", user.FirstName);
                                        newBody = newBody.Replace("<nextdate>", user.SubscriptionEndDate.Value.ToShortDateString());
                                        
                                        //appManager.EmailService.SendAsync(new IdentityMessage { Body = emailbody, Destination = userToSendMessage.Email, Subject = "Mesaj nou - eMeditatii.ro" );
                                        var email = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["SendEmail.Address"], ConfigurationManager.AppSettings["SendEmail.Alias"]),
                                            new MailAddress(user.Email))
                                        {
                                            Subject = "Confirmare de plată",
                                            Body = newBody,
                                            IsBodyHtml = true
                                        };

                                        var client = new SmtpClient();
                                        client.SendCompleted += (s, e) =>
                                        {
                                            client.Dispose();
                                            //this.appoitmentService.SetAppoitmentNotification(appoitment.Id, Meditatii.CoreNew.Enums.AppoitmentNotification.NotificationPaid, true);
                                        };
                                        Task t = Task.Run(async () =>
                                        {
                                            await client.SendMailAsync(email);
                                        });
                                        t.Wait();
                                    }
                                    catch (Exception ex)
                                    {
                                        var path = HostingEnvironment.MapPath("~") + "notification.log";
                                        System.IO.File.AppendAllLines(path, new string[] { ex.Message + ex.StackTrace.ToString() });
                                    }

                                    /*if (lstAppoitments.TotalRows > 0)
                                    {
                                        try
                                        {
                                            string emailbody = EmailHelper.GetEmailTemplate("appoitment-paid");
                                            string urlappoitments = ConfigurationManager.AppSettings["WebSite.URL"] + "/u/appoitments";

                                            foreach (Appoitment appoitment in lstAppoitments.Entities)
                                            {
                                                if (!appoitment.NotificationPaid)
                                                {
                                                    string newBody = emailbody.Replace("<<student-firstname>", appoitment.Learner.FirstName);
                                                    newBody = newBody.Replace("<student-lastname>", appoitment.Learner.LastName);
                                                    newBody = newBody.Replace("<dateandtime>", appoitment.StartDate.ToString());

                                                    newBody = newBody.Replace("<appoitmentsurl>", urlappoitments);

                                                    //appManager.EmailService.SendAsync(new IdentityMessage { Body = emailbody, Destination = userToSendMessage.Email, Subject = "Mesaj nou - eMeditatii.ro" );
                                                    var email = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["SendEmail.Address"], ConfigurationManager.AppSettings["SendEmail.Alias"]),
                                                        new MailAddress(appoitment.Teacher.Email))
                                                    {
                                                        Subject = "Meditatie achitata - eMeditatii.ro",
                                                        Body = newBody,
                                                        IsBodyHtml = true
                                                    };

                                                    var client = new SmtpClient();
                                                    client.SendCompleted += (s, e) =>
                                                    {
                                                        client.Dispose();
                                                        this.appoitmentService.SetAppoitmentNotification(appoitment.Id, Meditatii.CoreNew.Enums.AppoitmentNotification.NotificationPaid, true);
                                                    };
                                                    Task t = Task.Run(async () =>
                                                    {
                                                        await client.SendMailAsync(email);
                                                    });
                                                    t.Wait();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            var path = HostingEnvironment.MapPath("~") + "notification.log";
                                            System.IO.File.AppendAllLines(path, new string[] { ex.Message + ex.StackTrace.ToString() });
                                        }
                                    }*/
                                }

                                break;
                            }
                        default:
                            {
                                errorType = "0x02";
                                errorCode = "0x300000f6";
                                errorMessage = "mobilpay_refference_action paramaters is invalid" + card.Confirm.Crc;
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    errorType = "0x01";
                    errorCode = "1032";
                    errorMessage = ex.Message;
                }
            }

            message = "";
            if (errorCode == "0")
            {
                message = message + "<crc>" + errorMessage + "</crc>";
            }
            else
            {
                message = message + "<crc error_type=\"" + errorType + "\" error_code=\"" + errorCode + "\"> " + errorMessage + "</crc>";
            }
            appoitmentService.RegisterPaymentLog(paymentId, message);
            return new XmlResult(message);
        }


        [ActionName("download")]
        [HttpGet]
        public ActionResult DownloadInvoice(string paymentCode)
        {
            //get payment
            PaymentModel payment = MappingHelper.Map<PaymentModel>(appoitmentService.GetPayment(Security.DecryptID(paymentCode)));

            if (payment != null)
            {
                var filename = payment.InvoiceNumber + ".pdf";
                var relativePathToInvoice = "~/invoices/" + filename;
                var absolutePathToInvoice = HttpContext.Server.MapPath(relativePathToInvoice);
                if (System.IO.File.Exists(absolutePathToInvoice))
                {
                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        // for example foo.bak
                        FileName = filename,

                        // always prompt the user for downloading, set to true if you want 
                        // the browser to try to show the file inline
                        Inline = false,
                    };

                    Response.AppendHeader("Content-Disposition", cd.ToString());

                    return File(absolutePathToInvoice, "application/pdf");
                }
                else
                {
                    //just return now not found - will generate this manually until will be more clear how to generate this
                    return HttpNotFound();
                    //GenerateInvoice(payment, absolutePathToInvoice);
                }

            }
            else
            {
                return HttpNotFound();
            }
        }

        public void GenerateInvoice(PaymentModel payment, string fileWithPath)
        {
            PdfWriter writer = new PdfWriter(fileWithPath);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("HEADER")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            document.Add(header);
            document.Close();
        }
    }



}