namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelspropertiesrevamped : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MovieCopies", "CurrentOwner_Id", "dbo.Clients");
            DropForeignKey("dbo.MovieCopies", "MovieId", "dbo.MovieOriginals");
            DropIndex("dbo.MovieCopies", new[] { "MovieId" });
            DropIndex("dbo.MovieCopies", new[] { "CurrentOwner_Id" });
            CreateTable(
                "dbo.Leasings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LeasingStartDate = c.DateTime(nullable: false),
                        LeasingEndDate = c.DateTime(nullable: false),
                        MovieCopy_Id = c.Int(),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieCopies", t => t.MovieCopy_Id)
                .ForeignKey("dbo.Clients", t => t.Owner_Id)
                .Index(t => t.MovieCopy_Id)
                .Index(t => t.Owner_Id);
            
            AddColumn("dbo.MovieCopies", "PricePerDay", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Clients", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.MovieCopies", "MovieId", c => c.Int(nullable: false));
            CreateIndex("dbo.MovieCopies", "MovieId");
            AddForeignKey("dbo.MovieCopies", "MovieId", "dbo.MovieOriginals", "Id", cascadeDelete: true);
            DropColumn("dbo.MovieCopies", "LeaseStartDate");
            DropColumn("dbo.MovieCopies", "LeaseEndDate");
            DropColumn("dbo.MovieCopies", "CurrentOwner_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MovieCopies", "CurrentOwner_Id", c => c.Int());
            AddColumn("dbo.MovieCopies", "LeaseEndDate", c => c.DateTime());
            AddColumn("dbo.MovieCopies", "LeaseStartDate", c => c.DateTime());
            DropForeignKey("dbo.MovieCopies", "MovieId", "dbo.MovieOriginals");
            DropForeignKey("dbo.Leasings", "Owner_Id", "dbo.Clients");
            DropForeignKey("dbo.Leasings", "MovieCopy_Id", "dbo.MovieCopies");
            DropIndex("dbo.MovieCopies", new[] { "MovieId" });
            DropIndex("dbo.Leasings", new[] { "Owner_Id" });
            DropIndex("dbo.Leasings", new[] { "MovieCopy_Id" });
            AlterColumn("dbo.MovieCopies", "MovieId", c => c.Int());
            AlterColumn("dbo.Clients", "Rating", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.MovieCopies", "PricePerDay");
            DropTable("dbo.Leasings");
            CreateIndex("dbo.MovieCopies", "CurrentOwner_Id");
            CreateIndex("dbo.MovieCopies", "MovieId");
            AddForeignKey("dbo.MovieCopies", "MovieId", "dbo.MovieOriginals", "Id");
            AddForeignKey("dbo.MovieCopies", "CurrentOwner_Id", "dbo.Clients", "Id");
        }
    }
}
