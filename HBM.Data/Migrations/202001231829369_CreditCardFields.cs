namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreditCardFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DebtCards", "CountTransactions");
            DropColumn("dbo.DebtCards", "DateStart");
            DropColumn("dbo.DebtCards", "DateEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DebtCards", "DateEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.DebtCards", "DateStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.DebtCards", "CountTransactions", c => c.Int(nullable: false));
        }
    }
}
