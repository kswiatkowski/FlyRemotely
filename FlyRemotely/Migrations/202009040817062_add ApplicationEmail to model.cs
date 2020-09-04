namespace FlyRemotely.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addApplicationEmailtomodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offer", "ApplicationEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offer", "ApplicationEmail");
        }
    }
}
