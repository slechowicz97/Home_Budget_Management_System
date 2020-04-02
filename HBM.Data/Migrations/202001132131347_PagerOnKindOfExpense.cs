namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PagerOnKindOfExpense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KindOfExpenses", "KindOfExpenseID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KindOfExpenses", "KindOfExpenseID");
        }
    }
}
