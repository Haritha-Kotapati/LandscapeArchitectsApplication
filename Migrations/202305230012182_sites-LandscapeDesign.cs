namespace LandscapeArchitectsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sitesLandscapeDesign : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "DesignID", c => c.Int(nullable: false));
            CreateIndex("dbo.Sites", "DesignID");
            AddForeignKey("dbo.Sites", "DesignID", "dbo.LandscapeDesigns", "DesignID", cascadeDelete: true);
        }
        //27.31 https://www.youtube.com/watch?v=V1emgCxxRtI
        public override void Down()
        {
            DropForeignKey("dbo.Sites", "DesignID", "dbo.LandscapeDesigns");
            DropIndex("dbo.Sites", new[] { "DesignID" });
            DropColumn("dbo.Sites", "DesignID");
        }
    }
}
