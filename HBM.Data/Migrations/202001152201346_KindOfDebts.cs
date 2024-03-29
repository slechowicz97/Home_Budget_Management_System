namespace HBM.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KindOfDebts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KindOfDebts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KindOfDebts");
        }
    }
}
