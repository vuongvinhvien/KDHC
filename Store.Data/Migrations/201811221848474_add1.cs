namespace Store.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegisterForm", "ExpectedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegisterForm", "ExpectedDate", c => c.DateTime());
        }
    }
}
