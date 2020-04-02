namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SavingsCurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Savings", "Currency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Savings", "Currency");
        }
    }
}
