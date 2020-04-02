namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryPicture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImportantShoppingID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.ImportantShoppings", t => t.ImportantShoppingID, cascadeDelete: true)
                .Index(t => t.ImportantShoppingID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingPictures", "ImportantShoppingID", "dbo.ImportantShoppings");
            DropForeignKey("dbo.ShoppingPictures", "PictureID", "dbo.Pictures");
            DropIndex("dbo.ShoppingPictures", new[] { "PictureID" });
            DropIndex("dbo.ShoppingPictures", new[] { "ImportantShoppingID" });
            DropTable("dbo.Pictures");
            DropTable("dbo.ShoppingPictures");
        }
    }
}
