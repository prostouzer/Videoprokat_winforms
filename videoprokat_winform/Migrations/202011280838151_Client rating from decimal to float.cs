namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clientratingfromdecimaltofloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Rating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
