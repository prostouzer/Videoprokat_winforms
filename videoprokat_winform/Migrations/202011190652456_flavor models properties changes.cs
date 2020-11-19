namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flavormodelspropertieschanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieOriginals", "YearReleased", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieOriginals", "YearReleased");
        }
    }
}
