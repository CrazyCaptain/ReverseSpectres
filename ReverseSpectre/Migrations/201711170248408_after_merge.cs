namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class after_merge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmploymentInformations",
                c => new
                    {
                        EmploymentInformationId = c.Int(nullable: false, identity: true),
                        SourceOfFunds = c.Int(nullable: false),
                        SourceOfFundsInfo = c.String(),
                        Employer = c.String(),
                        Position = c.String(),
                        FormOfBusiness = c.Byte(nullable: false),
                        EmployerBusinessAddress = c.String(),
                        ContactNumber = c.String(),
                        NatureOfJob = c.String(),
                        YearsInJob = c.Byte(nullable: false),
                        IsOFW = c.Boolean(nullable: false),
                        ClientId = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmploymentInformationId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.TwoFactorAuthSMS",
                c => new
                    {
                        TwoFactorAuthSMSId = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                        DateTimeExpiry = c.DateTime(nullable: false),
                        LoanApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TwoFactorAuthSMSId)
                .ForeignKey("dbo.LoanApplications", t => t.LoanApplicationId, cascadeDelete: true)
                .Index(t => t.LoanApplicationId);
            
            AddColumn("dbo.Clients", "AccessToken", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TwoFactorAuthSMS", "LoanApplicationId", "dbo.LoanApplications");
            DropForeignKey("dbo.EmploymentInformations", "ClientId", "dbo.Clients");
            DropIndex("dbo.TwoFactorAuthSMS", new[] { "LoanApplicationId" });
            DropIndex("dbo.EmploymentInformations", new[] { "ClientId" });
            DropColumn("dbo.Clients", "AccessToken");
            DropTable("dbo.TwoFactorAuthSMS");
            DropTable("dbo.EmploymentInformations");
        }
    }
}
