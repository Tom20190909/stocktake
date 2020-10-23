namespace stocktake.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BarCodeInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemCode = c.String(unicode: false),
                        ItemName = c.String(unicode: false),
                        CodeBard = c.String(unicode: false),
                        BrandCode = c.String(unicode: false),
                        BrandName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TakeCodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodeBard = c.String(unicode: false),
                        ItemCode = c.String(unicode: false),
                        ItemName = c.String(unicode: false),
                        BrandCode = c.String(unicode: false),
                        TV1 = c.Int(nullable: false),
                        TV2 = c.Int(nullable: false),
                        TV3 = c.Int(nullable: false),
                        BrandName = c.String(unicode: false),
                        TakeArea = c.String(unicode: false),
                        TakeTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TakeCodes");
            DropTable("dbo.BarCodeInfoes");
        }
    }
}
