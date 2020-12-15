namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renaming : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leasings", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Leasings", new[] { "Customer_Id" });
            RenameColumn(table: "dbo.Leasings", name: "Customer_Id", newName: "CustomerId");
            AlterColumn("dbo.Leasings", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Leasings", "CustomerId");
            AddForeignKey("dbo.Leasings", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.Leasings", "ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leasings", "ClientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Leasings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Leasings", new[] { "CustomerId" });
            AlterColumn("dbo.Leasings", "CustomerId", c => c.Int());
            RenameColumn(table: "dbo.Leasings", name: "CustomerId", newName: "Customer_Id");
            CreateIndex("dbo.Leasings", "Customer_Id");
            AddForeignKey("dbo.Leasings", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
