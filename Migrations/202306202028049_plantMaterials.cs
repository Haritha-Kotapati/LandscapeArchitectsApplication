namespace LandscapeArchitectsApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plantMaterials : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlantMaterialSites", newName: "SitePlantMaterials");
            DropPrimaryKey("dbo.SitePlantMaterials");
            AddPrimaryKey("dbo.SitePlantMaterials", new[] { "Site_SiteID", "PlantMaterial_Plant_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SitePlantMaterials");
            AddPrimaryKey("dbo.SitePlantMaterials", new[] { "PlantMaterial_Plant_Id", "Site_SiteID" });
            RenameTable(name: "dbo.SitePlantMaterials", newName: "PlantMaterialSites");
        }
    }
}
