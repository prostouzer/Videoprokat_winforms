namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Leasingmodelpropertiesrename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leasings", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Leasings", "ExpectedEndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Leasings", "LeasingStartDate");
            DropColumn("dbo.Leasings", "LeasingExpectedEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leasings", "LeasingExpectedEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Leasings", "LeasingStartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Leasings", "ExpectedEndDate");
            DropColumn("dbo.Leasings", "StartDate");
        }
    }
}
