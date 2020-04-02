namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ala2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.KindsExpenses", "KindOfExpenseID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KindsExpenses", "KindOfExpenseID", c => c.Int(nullable: false));
        }
    }
}
