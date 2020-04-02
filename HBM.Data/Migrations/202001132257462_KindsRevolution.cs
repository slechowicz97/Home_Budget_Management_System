namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KindsRevolution : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KindOfExpenses", "KindsID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KindOfExpenses", "KindsID");
        }
    }
}
