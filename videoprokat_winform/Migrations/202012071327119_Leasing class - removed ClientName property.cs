namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeasingclassremovedClientNameproperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Leasings", "ClientName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leasings", "ClientName", c => c.String());
        }
    }
}
