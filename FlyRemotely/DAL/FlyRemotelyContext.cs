using FlyRemotely.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FlyRemotely.DAL
{
    public class FlyRemotelyContext : DbContext
    {
        public FlyRemotelyContext() : base("FlyRemotelyContext")
        { }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<Category> Categories { get; set;}


        //Disable the "s" addition conventions at the end of the table name (database)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}