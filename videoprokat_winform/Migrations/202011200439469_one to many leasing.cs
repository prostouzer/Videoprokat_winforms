namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class onetomanyleasing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leasings", "Owner_Id", "dbo.Clients");
            DropForeignKey("dbo.Leasings", "MovieCopy_Id", "dbo.MovieCopies");
            DropIndex("dbo.Leasings", new[] { "MovieCopy_Id" });
            DropIndex("dbo.Leasings", new[] { "Owner_Id" });
            RenameColumn(table: "dbo.Leasings", name: "Owner_Id", newName: "ClientId");
            RenameColumn(table: "dbo.Leasings", name: "MovieCopy_Id", newName: "MovieCopyId");
            AlterColumn("dbo.Leasings", "MovieCopyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Leasings", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Leasings", "MovieCopyId");
            CreateIndex("dbo.Leasings", "ClientId");
            AddForeignKey("dbo.Leasings", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Leasings", "MovieCopyId", "dbo.MovieCopies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leasings", "MovieCopyId", "dbo.MovieCopies");
            DropForeignKey("dbo.Leasings", "ClientId", "dbo.Clients");
            DropIndex("dbo.Leasings", new[] { "ClientId" });
            DropIndex("dbo.Leasings", new[] { "MovieCopyId" });
            AlterColumn("dbo.Leasings", "ClientId", c => c.Int());
            AlterColumn("dbo.Leasings", "MovieCopyId", c => c.Int());
            RenameColumn(table: "dbo.Leasings", name: "MovieCopyId", newName: "MovieCopy_Id");
            RenameColumn(table: "dbo.Leasings", name: "ClientId", newName: "Owner_Id");
            CreateIndex("dbo.Leasings", "Owner_Id");
            CreateIndex("dbo.Leasings", "MovieCopy_Id");
            AddForeignKey("dbo.Leasings", "MovieCopy_Id", "dbo.MovieCopies", "Id");
            AddForeignKey("dbo.Leasings", "Owner_Id", "dbo.Clients", "Id");
        }
    }
}
