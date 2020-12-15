namespace videoprokat_winform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientToCustomer : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clients", newName: "Customers");
            DropForeignKey("dbo.Leasings", "ClientId", "dbo.Clients");
            DropIndex("dbo.Leasings", new[] { "ClientId" });
            AddColumn("dbo.Leasings", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Leasings", "Customer_Id");
            AddForeignKey("dbo.Leasings", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leasings", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Leasings", new[] { "Customer_Id" });
            DropColumn("dbo.Leasings", "Customer_Id");
            CreateIndex("dbo.Leasings", "ClientId");
            AddForeignKey("dbo.Leasings", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Customers", newName: "Clients");
        }
    }
}
