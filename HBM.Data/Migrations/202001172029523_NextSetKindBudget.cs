namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NextSetKindBudget : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Place = c.String(),
                        Price = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                        KBudgetID = c.Int(nullable: false),
                        Budgets_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Budgets", t => t.Budgets_ID)
                .Index(t => t.Budgets_ID);
            
            CreateTable(
                "dbo.KBudgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Budgets", "Budgets_ID", "dbo.Budgets");
            DropIndex("dbo.Budgets", new[] { "Budgets_ID" });
            DropTable("dbo.KBudgets");
            DropTable("dbo.Budgets");
        }
    }
}
