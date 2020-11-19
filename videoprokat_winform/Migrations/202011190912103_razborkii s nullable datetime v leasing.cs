namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class razborkiisnullabledatetimevleasing : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leasings", "ReturnDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leasings", "ReturnDate", c => c.DateTime());
        }
    }
}
