namespace FlyRemotely.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favoritemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorite",
                c => new
                    {
                        FavoriteId = c.Int(nullable: false, identity: true),
                        OfferId = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.FavoriteId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Favorite");
        }
    }
}
