namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreditCardST : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DebtCards", "PlannedTimeRefund", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DebtCards", "PlannedTimeRefund");
        }
    }
}
