namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeasingclassaddedClientNameproperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leasings", "ClientName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leasings", "ClientName");
        }
    }
}
