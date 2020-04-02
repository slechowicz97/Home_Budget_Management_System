namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NextSetBudget : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Budgets", "Budgets_ID", "dbo.Budgets");
            DropIndex("dbo.Budgets", new[] { "Budgets_ID" });
            CreateIndex("dbo.Budgets", "KBudgetID");
            AddForeignKey("dbo.Budgets", "KBudgetID", "dbo.KBudgets", "ID", cascadeDelete: true);
            DropColumn("dbo.Budgets", "Place");
            DropColumn("dbo.Budgets", "Price");
            DropColumn("dbo.Budgets", "Date");
            DropColumn("dbo.Budgets", "Budgets_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Budgets", "Budgets_ID", c => c.Int());
            AddColumn("dbo.Budgets", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Budgets", "Price", c => c.Single(nullable: false));
            AddColumn("dbo.Budgets", "Place", c => c.String());
            DropForeignKey("dbo.Budgets", "KBudgetID", "dbo.KBudgets");
            DropIndex("dbo.Budgets", new[] { "KBudgetID" });
            CreateIndex("dbo.Budgets", "Budgets_ID");
            AddForeignKey("dbo.Budgets", "Budgets_ID", "dbo.Budgets", "ID");
        }
    }
}
