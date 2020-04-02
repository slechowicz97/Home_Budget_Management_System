namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreditCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DebtCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Limit = c.Single(nullable: false),
                        CountTransactions = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DebtCards");
        }
    }
}
