namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduledTransactionsDataTypeShortComparersToday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduledTransactions", "CurrentlyData", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduledTransactions", "CurrentlyData");
        }
    }
}
