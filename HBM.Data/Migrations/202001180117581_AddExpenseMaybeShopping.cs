namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpenseMaybeShopping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImportantShoppings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Place = c.String(),
                        DateStart = c.DateTime(nullable: false),
                        DateStop = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImportantShoppings");
        }
    }
}
