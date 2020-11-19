namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leasingclassalldatestodatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leasings", "LeasingStartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Leasings", "LeasingExpectedEndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leasings", "LeasingExpectedEndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Leasings", "LeasingStartDate", c => c.DateTime(nullable: false));
        }
    }
}
