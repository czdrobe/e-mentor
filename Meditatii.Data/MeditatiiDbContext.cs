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

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Category> Category { get; set; }

        public virtual DbSet<Message> Message { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
               .HasMany<Category>(s => s.Categories)
               .WithMany(c => c.Users)
               .Map(cs =>
               {
                   cs.MapLeftKey("UserId");
                   cs.MapRightKey("CategoryId");
                   cs.ToTable("UserCategory");
               });

            modelBuilder.Entity<User>()
               .HasMany<Cycle>(s => s.Cycles)
               .WithMany(c => c.Users)
               .Map(cs =>
               {
                   cs.MapLeftKey("UserId");
                   cs.MapRightKey("CycleId");
                   cs.ToTable("UserCycle");
               });


        }
    }
}
