namespace ShoeCollectionMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesEtc : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShoeItems", "ShoeTypeId");
            RenameColumn(table: "dbo.ShoeItems", name: "ShoeTypes_ID", newName: "ShoeTypeId");
            RenameIndex(table: "dbo.ShoeItems", name: "IX_ShoeTypes_ID", newName: "IX_ShoeTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ShoeItems", name: "IX_ShoeTypeId", newName: "IX_ShoeTypes_ID");
            RenameColumn(table: "dbo.ShoeItems", name: "ShoeTypeId", newName: "ShoeTypes_ID");
            AddColumn("dbo.ShoeItems", "ShoeTypeId", c => c.Int());
        }
    }
}
