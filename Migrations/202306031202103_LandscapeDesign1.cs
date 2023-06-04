namespace LandscapeArchitectsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LandscapeDesign1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LandscapeDesigns", "LeadArchitect", c => c.String());
            DropColumn("dbo.LandscapeDesigns", "LeadArhitect");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LandscapeDesigns", "LeadArhitect", c => c.String());
            DropColumn("dbo.LandscapeDesigns", "LeadArchitect");
        }
    }
}
