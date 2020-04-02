namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CashFlows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KindOfExpenseID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KindOfExpenses", t => t.KindOfExpenseID, cascadeDelete: true)
                .Index(t => t.KindOfExpenseID);
            
            CreateTable(
                "dbo.KindOfExpenses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ScheduledTransactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TransactionName = c.String(),
                        Recipient = c.String(),
                        Price = c.Double(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CashFlows", "KindOfExpenseID", "dbo.KindOfExpenses");
            DropIndex("dbo.CashFlows", new[] { "KindOfExpenseID" });
            DropTable("dbo.ScheduledTransactions");
            DropTable("dbo.KindOfExpenses");
            DropTable("dbo.CashFlows");
        }
    }
}
