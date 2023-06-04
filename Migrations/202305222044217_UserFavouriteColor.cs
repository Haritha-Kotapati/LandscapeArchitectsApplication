namespace LandscapeArchitectsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFavouriteColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FavouriteColor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FavouriteColor");
        }
    }
}
