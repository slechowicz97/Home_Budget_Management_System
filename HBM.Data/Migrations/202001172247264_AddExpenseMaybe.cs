namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpenseMaybe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TransactionName = c.String(),
                        Price = c.Single(nullable: false),
                        Place = c.String(),
                        Date = c.DateTime(nullable: false),
                        BudgetsID = c.Int(nullable: false),
                        BudgetsName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Budgets", t => t.BudgetsID, cascadeDelete: true)
                .Index(t => t.BudgetsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "BudgetsID", "dbo.Budgets");
            DropIndex("dbo.Transactions", new[] { "BudgetsID" });
            DropTable("dbo.Transactions");
        }
    }
}
