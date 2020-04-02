namespace HBM.Data.
    
    s
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CashFlows", "Data", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ScheduledTransactions", "Data", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ScheduledTransactions", "Data", c => c.String());
            AlterColumn("dbo.CashFlows", "Data", c => c.String());
        }
    }
}
