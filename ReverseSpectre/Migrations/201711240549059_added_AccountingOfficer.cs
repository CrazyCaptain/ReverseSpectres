namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_AccountingOfficer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "AccountingOfficerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "AccountingOfficerId");
            AddForeignKey("dbo.Clients", "AccountingOfficerId", "dbo.AccountingOfficers", "AccountingOfficerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "AccountingOfficerId", "dbo.AccountingOfficers");
            DropIndex("dbo.Clients", new[] { "AccountingOfficerId" });
            DropColumn("dbo.Clients", "AccountingOfficerId");
        }
    }
}
