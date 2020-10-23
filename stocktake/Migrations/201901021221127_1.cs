namespace stocktake.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NoexitsBars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodeBard = c.String(unicode: false),
                        ItemName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NoexitsBars");
        }
    }
}
