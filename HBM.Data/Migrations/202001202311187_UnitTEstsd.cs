namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitTEstsd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KindsExpenses", "Kind_ID", "dbo.KindsEs");
            DropIndex("dbo.KindsExpenses", new[] { "Kind_ID" });
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.KindsExpenses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KindsID = c.Int(nullable: false),
                        Name = c.String(),
                        Category = c.String(),
                        Kind_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.KindsEs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.KindsExpenses", "Kind_ID");
            AddForeignKey("dbo.KindsExpenses", "Kind_ID", "dbo.KindsEs", "ID");
        }
    }
}
