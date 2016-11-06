namespace ShoeCollectionMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverrideCat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoeCategory",
                c => new
                    {
                        ShoeId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoeId, t.CategoryId })
                .ForeignKey("dbo.ShoeItems", t => t.ShoeId, cascadeDelete: true)
                .ForeignKey("dbo.ShoeCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.ShoeId)
                .Index(t => t.CategoryId);
            
            AlterColumn("dbo.ShoeCategories", "CategoryName", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoeCategory", "CategoryId", "dbo.ShoeCategories");
            DropForeignKey("dbo.ShoeCategory", "ShoeId", "dbo.ShoeItems");
            DropIndex("dbo.ShoeCategory", new[] { "CategoryId" });
            DropIndex("dbo.ShoeCategory", new[] { "ShoeId" });
            AlterColumn("dbo.ShoeCategories", "CategoryName", c => c.String(nullable: false));
            DropTable("dbo.ShoeCategory");
        }
    }
}
