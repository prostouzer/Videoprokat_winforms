namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dealingwithLeasingclassnullabledatetimeproperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leasings", "LeasingStartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Leasings", "LeasingExpectedEndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Leasings", "ReturnDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leasings", "ReturnDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Leasings", "LeasingExpectedEndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Leasings", "LeasingStartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
