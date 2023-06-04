namespace LandscapeArchitectsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LandscapeDesign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LandscapeDesigns",
                c => new
                    {
                        DesignID = c.Int(nullable: false, identity: true),
                        LeadArhitect = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DesignID);
            
            AddColumn("dbo.Sites", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "Address");
            DropTable("dbo.LandscapeDesigns");
        }
    }
}
