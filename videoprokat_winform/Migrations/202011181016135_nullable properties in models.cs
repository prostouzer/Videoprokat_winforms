namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullablepropertiesinmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rating = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieCopies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Commentary = c.String(),
                        LeaseStartDate = c.DateTime(),
                        LeaseEndDate = c.DateTime(),
                        Available = c.Boolean(nullable: false),
                        MovieId = c.Int(),
                        CurrentOwner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.CurrentOwner_Id)
                .ForeignKey("dbo.MovieOriginals", t => t.MovieId)
                .Index(t => t.MovieId)
                .Index(t => t.CurrentOwner_Id);
            
            CreateTable(
                "dbo.MovieOriginals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieCopies", "MovieId", "dbo.MovieOriginals");
            DropForeignKey("dbo.MovieCopies", "CurrentOwner_Id", "dbo.Clients");
            DropIndex("dbo.MovieCopies", new[] { "CurrentOwner_Id" });
            DropIndex("dbo.MovieCopies", new[] { "MovieId" });
            DropTable("dbo.MovieOriginals");
            DropTable("dbo.MovieCopies");
            DropTable("dbo.Clients");
        }
    }
}
