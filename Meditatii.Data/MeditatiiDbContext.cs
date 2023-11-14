using Meditatii.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data
{


    public class MeditatiiDbContext : DbContext
    {
        public MeditatiiDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual DbSet<Ad> Ad { get; set; }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Category> Category { get; set; }

        public virtual DbSet<Message> Message { get; set; }

        public virtual DbSet<Appoitment> Appoitment { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<PaymentOut> PaymentsOut { get; set; }

        public virtual DbSet<AppoitmentChat> AppoitmentChat { get; set; }

        public virtual DbSet<TeacherRating> TeacherRatings { get; set; }

        public virtual DbSet<TeacherAvailability> TeacherAvailabilities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Ad>().Ignore(c => c.Added);

            modelBuilder.Entity<Ad>()
               .HasMany<Cycle>(s => s.Cycles)
               .WithMany(c => c.Ads)
               .Map(cs =>
               {
                   cs.MapLeftKey("AdId");
                   cs.MapRightKey("CycleId");
                   cs.ToTable("AdCycle");
               });

            modelBuilder.Entity<User>()
               .HasMany<Roles>(s => s.Roles)
               .WithMany(c => c.Users)
               .Map(cs =>
               {
                   cs.MapLeftKey("UserId");
                   cs.MapRightKey("RoleId");
                   cs.ToTable("UserRoles");
               });

            modelBuilder.Entity<User>()
               .HasMany<City>(s => s.Cities)
               .WithMany(c => c.Users)
               .Map(cs =>
               {
                   cs.MapLeftKey("UserId");
                   cs.MapRightKey("CityId");
                   cs.ToTable("CityUser");
               });
        }
    }
}
