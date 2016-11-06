namespace ShoeCollectionMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoeCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        LastModified = c.DateTime(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ShoeItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ShoeName = c.String(nullable: false),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Enabled = c.Boolean(nullable: false),
                        StockCount = c.Int(nullable: false),
                        LastOrderDate = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.String(),
                        Comments = c.String(),
                        ShoeTypeId = c.Int(),
                        ShoeTypes_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ShoeTypes", t => t.ShoeTypes_ID)
                .Index(t => t.ShoeTypes_ID);
            
            CreateTable(
                "dbo.ShoeTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                        LastModified = c.DateTime(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoeItems", "ShoeTypes_ID", "dbo.ShoeTypes");
            DropIndex("dbo.ShoeItems", new[] { "ShoeTypes_ID" });
            DropTable("dbo.ShoeTypes");
            DropTable("dbo.ShoeItems");
            DropTable("dbo.ShoeCategories");
        }
    }
}
