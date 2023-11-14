using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Meditatii.Services;
using Meditatii.Core;
using Meditatii.Core.Data;
using Meditatii.Data.Repositories;
using meditatii.Controllers;
using Unity;
using Unity.Injection;
using Microsoft.AspNet.Identity;
using meditatii.Models;
using System.Data.Entity;
using Unity.Lifetime;
using Microsoft.AspNet.Identity.EntityFramework;

namespace meditatii.web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            RegisterServices(container);
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<ITeacherAvailabilityData, TeacherAvailabilityRepository>();
            container.RegisterType<ITeacherAvailabilityService, TeacherAvailabilityService>();

            container.RegisterType<IAdData, AdRepository>();
            container.RegisterType<IAdService, AdService>();

            container.RegisterType<IAppoitmentData, AppoitmentRepository>();
            container.RegisterType<IAppoitmentService, AppoitmentService>();

            container.RegisterType<IReportData, ReportRepository>();
            container.RegisterType<IReportService, ReportService>();

            container.RegisterType<IMessageData, MessageRepository>();
            container.RegisterType<IMessageService, MessageService>();

            container.RegisterType<IUserData, UserRepository>();
            container.RegisterType<IUsersService, UsersService>();

            container.RegisterType<ICategoryData, CategoryRepository>();
            container.RegisterType<ICategoryService, CategoryService>();

            container.RegisterType<ICycleData, CycleRepository>();
            container.RegisterType<ICycleService, CycleService>();

            container.RegisterType<ITeacherRatingData, TeacherRatingRepository>();
            container.RegisterType<ITeacherRatingService, TeacherRatingService>();

            //The current type, Microsoft.AspNet.Identity.IUserStore`2[meditatii.Models.ApplicationUser,System.Int32], is an interface and cannot be constructed. Are you missing a type mapping?

            //container.RegisterType<AccountController>(new InjectionConstructor());

        }
    }
}