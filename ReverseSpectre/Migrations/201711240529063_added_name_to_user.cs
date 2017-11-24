namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_name_to_user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountingOfficers",
                c => new
                    {
                        AccountingOfficerId = c.Int(nullable: false, identity: true),
                        IsDisabled = c.Boolean(nullable: false),
                        TimestampCreated = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AccountingOfficerId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.ClientInvitations", "Amount", c => c.Double(nullable: false));
            AddColumn("dbo.ClientInvitations", "Term", c => c.Int(nullable: false));
            AddColumn("dbo.ClientInvitations", "AccountingOfficerId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClientInvitations", "AccountingOfficerId");
            AddForeignKey("dbo.ClientInvitations", "AccountingOfficerId", "dbo.AccountingOfficers", "AccountingOfficerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientInvitations", "AccountingOfficerId", "dbo.AccountingOfficers");
            DropForeignKey("dbo.AccountingOfficers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ClientInvitations", new[] { "AccountingOfficerId" });
            DropIndex("dbo.AccountingOfficers", new[] { "UserId" });
            DropColumn("dbo.ClientInvitations", "AccountingOfficerId");
            DropColumn("dbo.ClientInvitations", "Term");
            DropColumn("dbo.ClientInvitations", "Amount");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "MiddleName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropTable("dbo.AccountingOfficers");
        }
    }
}
