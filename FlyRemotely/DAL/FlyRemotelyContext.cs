using FlyRemotely.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FlyRemotely.DAL
{
    public class FlyRemotelyContext : IdentityDbContext<ApplicationUser>
    {
        public FlyRemotelyContext() : base("FlyRemotelyContext")
        { }

        static FlyRemotelyContext()
        {
            Database.SetInitializer<FlyRemotelyContext>(new FlyRemotelyInitializer());
        }

        public static FlyRemotelyContext Create()
        {
            return new FlyRemotelyContext();
        }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<Category> Categories { get; set;}
        public DbSet<Favorite> Favourites { get; set; }


        //Disable the "s" addition conventions at the end of the table name (database)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}