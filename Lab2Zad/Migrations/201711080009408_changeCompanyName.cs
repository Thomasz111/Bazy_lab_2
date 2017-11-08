namespace Lab2Zad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeCompanyName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "Customer_CompanyName", newName: "CompanyName");
            RenameIndex(table: "dbo.Orders", name: "IX_Customer_CompanyName", newName: "IX_CompanyName");
            DropColumn("dbo.Orders", "CustomerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CustomerName", c => c.String());
            RenameIndex(table: "dbo.Orders", name: "IX_CompanyName", newName: "IX_Customer_CompanyName");
            RenameColumn(table: "dbo.Orders", name: "CompanyName", newName: "Customer_CompanyName");
        }
    }
}
