namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchedDeleteCurrently : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ScheduledTransactions", "CurrentlyData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScheduledTransactions", "CurrentlyData", c => c.DateTime(nullable: false));
        }
    }
}
