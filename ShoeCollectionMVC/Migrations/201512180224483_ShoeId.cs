namespace ShoeCollectionMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoeId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ShoeCategory", name: "ProductId", newName: "ShoeId");
            RenameIndex(table: "dbo.ShoeCategory", name: "IX_ProductId", newName: "IX_ShoeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ShoeCategory", name: "IX_ShoeId", newName: "IX_ProductId");
            RenameColumn(table: "dbo.ShoeCategory", name: "ShoeId", newName: "ProductId");
        }
    }
}
