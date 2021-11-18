using AutoMapper;
using meditatii.Models;
using meditatii.web.App_Start;
using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Meditatii.Data;
using meditatii.web.Utils;
using Quartz.Impl;
using Quartz;
using System.Threading.Tasks;
using meditatii.web.ScheduledTasks;

namespace meditatii
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(System.Web.Http.GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg => RegisterType(cfg));
/*
            StdSchedulerFactory factory = new StdSchedulerFactory();
            Task<IScheduler> GetSchedulerTask = factory.GetScheduler();
            
            Task.Run(async () => { await JobScheduler.Start(); });

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<SendAppoitmentNotificationJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                //.StartAt(new DateTime(2020, 9, 2, 19, 02, 0))
                .WithSimpleSchedule(x => x
                   .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
            */

            Task.Run(async () => { await JobScheduler.Start(); });

        }

        protected void Application_End()
        {
            JobScheduler.PingServer();
        }

        private void RegisterType(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<TeacherAvailabilityModel, TeacherAvailability>();
            mapper.CreateMap<TeacherAvailability, TeacherAvailabilityModel>();
            mapper.CreateMap<Meditatii.Data.Models.TeacherAvailability, TeacherAvailability>();
            mapper.CreateMap<SearchResult<TeacherAvailability>, SearchResult<TeacherAvailabilityModel>>();

            mapper.CreateMap<Appoitment, AppoitmentModel>()
                .ForMember(x => x.CanJoin, y => y.ResolveUsing(z =>
                {
                    return z.Active && z.AcceptedByTeacher && z.StartDate <= DateTime.Now && z.EndDate > DateTime.Now && z.Payment != null && z.Payment.Status == 1;
                }))
                .ForMember(x => x.isReadyForRating, y => y.ResolveUsing(z =>
                {
                    return z.Active && z.AcceptedByTeacher  && z.EndDate < DateTime.Now && z.Ratings.Count() == 0 && z.Payment != null && z.Payment.Status  == 1;
                }));

            mapper.CreateMap<Meditatii.Data.Models.Appoitment, Appoitment>();

            mapper.CreateMap<ReportTeacherAppoitment, ReportTeacherAppoitmentModel>();

            mapper.CreateMap<AppoitmentChat, AppoitmentChatModel>().ForMember(x => x.IsMine, y => y.ResolveUsing(z =>
            {
                return z.User.Email == System.Web.HttpContext.Current.User.Identity.Name;
            })); ;
            mapper.CreateMap<Meditatii.Data.Models.AppoitmentChat, AppoitmentChat>();

            mapper.CreateMap<Message, MessageModel>()
                .ForMember(x => x.FromUserId, y => y.MapFrom(z => Security.EncryptID(z.FromUserId)))
                .ForMember(x => x.ToUserId, y => y.MapFrom(z => Security.EncryptID(z.ToUserId)));

            mapper.CreateMap<MessageModel, Message>()
                .ForMember(x => x.FromUserId, y => y.MapFrom(z => Security.DecryptID(z.FromUserId)))
                .ForMember(x => x.ToUserId, y => y.MapFrom(z => Security.DecryptID(z.ToUserId)));

            mapper.CreateMap<Meditatii.Data.Models.Message, Message>()
                .ForMember(x => x.SenderName, y => y.MapFrom(z => z.FromUser.LastName + " " + z.FromUser.FirstName))
                .ForMember(x => x.isMy, y => y.MapFrom(z => z.FromUser.Email == System.Web.HttpContext.Current.User.Identity.Name))
                .ForMember(x => x.FromProfileImageUrl, y => y.MapFrom(z => z.FromUser.ProfileImageUrl));

            mapper.CreateMap<Payment, PaymentModel>()
                .ForMember(x => x.PaymentCode, y => y.MapFrom(z => Security.EncryptID(z.Id)))
                .ForMember(x => x.ProductName, y => y.MapFrom(z => (z.Product == 31 ? "1 luna" : (z.Product == 93 ? "3 luni" : "1 an"))))
                .ForMember(x => x.InvoiceNumber, y => y.MapFrom(z => "RO" + z.Added.ToString("yyyyMMdd") + z.Id ));

            mapper.CreateMap<User, UserModel>()
                .ForMember(x => x.UserCode, y => y.MapFrom(z => Security.EncryptID(z.Id)))
                .ForMember(x => x.Rating, y => y.MapFrom(z => z.Rating == null ? 0 : z.Rating))
                .ForMember(x => x.IsSubscriptionOk, y => y.MapFrom(z => z.SubscriptionStartDate <= DateTime.Now && z.SubscriptionEndDate >= DateTime.Now  ? true : false));
            mapper.CreateMap<UserModel, User>().ForMember(x => x.Id, y => y.MapFrom(z => Security.DecryptID(z.UserCode))); 
            mapper.CreateMap <Meditatii.Data.Models.User, User>();
            mapper.CreateMap<User,Meditatii.Data.Models.User>();

            mapper.CreateMap<Meditatii.Data.Models.TeacherRating, TeacherRating>();
            mapper.CreateMap<TeacherRating, TeacherRatingModel>();
            mapper.CreateMap<TeacherRatingModel, TeacherRating>().ForMember(x => x.TeacherId, y => y.MapFrom(z => Security.DecryptID(z.TeacherIdEncoded)));

            mapper.CreateMap<Request, RequestModel>()
                .ForMember(x => x.RequestCode, y => y.MapFrom(z => Security.EncryptID(z.Id)));
            mapper.CreateMap<RequestModel, Request>();
            mapper.CreateMap<Meditatii.Data.Models.Request, Request>();
            mapper.CreateMap<Request, Meditatii.Data.Models.Request>();

            mapper.CreateMap<Category, CategoryModel>();
            mapper.CreateMap<CategoryModel, Category>();
            mapper.CreateMap<Meditatii.Data.Models.Category, Category>();
            mapper.CreateMap<Category, Meditatii.Data.Models.Category>();

            mapper.CreateMap<Cycle, CycleModel>();
            mapper.CreateMap<CycleModel, Cycle>();
            mapper.CreateMap<Meditatii.Data.Models.Cycle, Cycle>();
            mapper.CreateMap<Cycle, Meditatii.Data.Models.Cycle>();

            mapper.CreateMap<City, CityModel>();
            mapper.CreateMap<CityModel, City>();
            mapper.CreateMap<City, Meditatii.Data.Models.City>();
            mapper.CreateMap<Meditatii.Data.Models.City, City>();

            mapper.CreateMap<SearchResult<User>, SearchResult<UserModel>>();
            mapper.CreateMap<SearchResult<Message>, SearchResult<MessageModel>>();

            mapper.CreateMap<UserSats, UserSatsModel>();

            mapper.CreateMap<MentorMessage, MentorMessageModel>().ForMember(x => x.Code, y => y.MapFrom(z => Security.EncryptID(z.Id))); ;

        }
    }
}
