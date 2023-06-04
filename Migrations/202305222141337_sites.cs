namespace LandscapeArchitectsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        SiteID = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                    })
                .PrimaryKey(t => t.SiteID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sites");
        }
    }
}
