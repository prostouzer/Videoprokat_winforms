namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pricepropertiesinmodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leasings", "LeasingExpectedEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Leasings", "ReturnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Leasings", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Leasings", "LeasingEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leasings", "LeasingEndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Leasings", "TotalPrice");
            DropColumn("dbo.Leasings", "ReturnDate");
            DropColumn("dbo.Leasings", "LeasingExpectedEndDate");
        }
    }
}
