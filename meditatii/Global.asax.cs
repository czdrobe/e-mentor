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
                    return z.StartDate <= DateTime.Now && z.EndDate > DateTime.Now && z.Payments.Count(p => p.Status == 1) > 0;
                }))
                .ForMember(x => x.isReadyForRating, y => y.ResolveUsing(z =>
                {
                    return z.EndDate < DateTime.Now && z.Ratings.Count() == 0 && z.Payments.Count(p => p.Status == 1) > 0;
                }));

            mapper.CreateMap<Meditatii.Data.Models.Appoitment, Appoitment>();

            mapper.CreateMap<AppoitmentChat, AppoitmentChatModel>().ForMember(x => x.IsMine, y => y.ResolveUsing(z =>
            {
                return z.User.Email == System.Web.HttpContext.Current.User.Identity.Name;
            })); ;
            mapper.CreateMap<Meditatii.Data.Models.AppoitmentChat, AppoitmentChat>();

            mapper.CreateMap<Message, MessageModel>();
            mapper.CreateMap<Meditatii.Data.Models.Message, Message>()
                .ForMember(x => x.SenderName, y => y.MapFrom(z => z.FromUser.LastName + " " + z.FromUser.FirstName))
                .ForMember(x => x.isMy, y => y.MapFrom(z => z.FromUser.Email == System.Web.HttpContext.Current.User.Identity.Name))
                .ForMember(x => x.FromProfileImageUrl, y => y.MapFrom(z => z.FromUser.ProfileImageUrl));

            mapper.CreateMap<User, UserModel>();
            mapper.CreateMap<UserModel, User>();
            mapper.CreateMap <Meditatii.Data.Models.User, User>();
            mapper.CreateMap<User,Meditatii.Data.Models.User>();

            mapper.CreateMap<Meditatii.Data.Models.TeacherRating, TeacherRating>();
            mapper.CreateMap<TeacherRating, TeacherRatingModel>();

            mapper.CreateMap<Category, CategoryModel>();
            mapper.CreateMap<CategoryModel, Category>();
            mapper.CreateMap<Meditatii.Data.Models.Category, Category>();
            mapper.CreateMap<Category, Meditatii.Data.Models.Category>();

            mapper.CreateMap<Cycle, CycleModel>();
            mapper.CreateMap<CycleModel, Cycle>();
            mapper.CreateMap<Meditatii.Data.Models.Cycle, Cycle>();
            mapper.CreateMap<Cycle, Meditatii.Data.Models.Cycle>();

            mapper.CreateMap<SearchResult<User>, SearchResult<UserModel>>();
            mapper.CreateMap<SearchResult<Message>, SearchResult<MessageModel>>();

            mapper.CreateMap<MentorMessage, MentorMessageModel>();

        }
    }
}
