namespace FlyRemotely.Migrations
{
    using FlyRemotely.DAL;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<FlyRemotely.DAL.FlyRemotelyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FlyRemotely.DAL.FlyRemotelyContext";
        }

        protected override void Seed(FlyRemotely.DAL.FlyRemotelyContext context)
        {
            FlyRemotelyInitializer.SeedFlyRemotelyData(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
