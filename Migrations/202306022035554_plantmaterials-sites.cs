namespace LandscapeArchitectsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plantmaterialssites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlantMaterials",
                c => new
                    {
                        Plant_Id = c.Int(nullable: false, identity: true),
                        Plant_Name = c.String(),
                        Plant_Type = c.String(),
                    })
                .PrimaryKey(t => t.Plant_Id);
            
            CreateTable(
                "dbo.PlantMaterialSites",
                c => new
                    {
                        PlantMaterial_Plant_Id = c.Int(nullable: false),
                        Site_SiteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlantMaterial_Plant_Id, t.Site_SiteID })
                .ForeignKey("dbo.PlantMaterials", t => t.PlantMaterial_Plant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sites", t => t.Site_SiteID, cascadeDelete: true)
                .Index(t => t.PlantMaterial_Plant_Id)
                .Index(t => t.Site_SiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlantMaterialSites", "Site_SiteID", "dbo.Sites");
            DropForeignKey("dbo.PlantMaterialSites", "PlantMaterial_Plant_Id", "dbo.PlantMaterials");
            DropIndex("dbo.PlantMaterialSites", new[] { "Site_SiteID" });
            DropIndex("dbo.PlantMaterialSites", new[] { "PlantMaterial_Plant_Id" });
            DropTable("dbo.PlantMaterialSites");
            DropTable("dbo.PlantMaterials");
        }
    }
}
