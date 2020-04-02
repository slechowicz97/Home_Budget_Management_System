namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseAddssMaybeGoodSSESES : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.KindOfExpenses", newName: "KindsExpenses");
            RenameTable(name: "dbo.Kinds", newName: "KindsEs");
            DropForeignKey("dbo.KindOfExpenses", "KindsID", "dbo.Kinds");
            DropIndex("dbo.KindsExpenses", new[] { "KindsID" });
            AddColumn("dbo.KindsExpenses", "KindsID", c => c.Int());
            CreateIndex("dbo.KindsExpenses", "KindsID");
            AddForeignKey("dbo.KindsExpenses", "KindsID", "dbo.KindsEs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KindsExpenses", "KindsID", "dbo.KindsEs");
            DropIndex("dbo.KindsExpenses", new[] { "KindsID" });
            DropColumn("dbo.KindsExpenses", "KindsID");
            CreateIndex("dbo.KindsExpenses", "KindsID");
            AddForeignKey("dbo.KindOfExpenses", "KindsID", "dbo.Kinds", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.KindsEs", newName: "Kinds");
            RenameTable(name: "dbo.KindsExpenses", newName: "KindOfExpenses");
        }
    }
}
