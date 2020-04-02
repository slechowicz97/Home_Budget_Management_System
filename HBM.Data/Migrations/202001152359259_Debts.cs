namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Debts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Debts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DebtPerson = c.String(),
                        Executor = c.String(),
                        Price = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                        KindOfDebtsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KindOfDebts", t => t.KindOfDebtsID, cascadeDelete: true)
                .Index(t => t.KindOfDebtsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Debts", "KindOfDebtsID", "dbo.KindOfDebts");
            DropIndex("dbo.Debts", new[] { "KindOfDebtsID" });
            DropTable("dbo.Debts");
        }
    }
}
