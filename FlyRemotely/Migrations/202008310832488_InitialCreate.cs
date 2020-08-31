namespace FlyRemotely.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Offer",
                c => new
                    {
                        OfferId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.String(),
                        Title = c.String(),
                        Salary = c.String(),
                        Requirements = c.String(),
                        Description = c.String(),
                        CompanyName = c.String(),
                        CompanyWebsite = c.String(),
                        CompanyDescription = c.String(),
                        CompanyPhotoSource = c.String(),
                        Featured = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OfferId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offer", "CategoryId", "dbo.Category");
            DropIndex("dbo.Offer", new[] { "CategoryId" });
            DropTable("dbo.Offer");
            DropTable("dbo.Category");
        }
    }
}
