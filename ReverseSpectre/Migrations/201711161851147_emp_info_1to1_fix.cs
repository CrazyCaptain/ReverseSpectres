namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emp_info_1to1_fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoanApplications", "ClientId", "dbo.Clients");
            DropPrimaryKey("dbo.Clients");
            CreateTable(
                "dbo.EmploymentInformations",
                c => new
                    {
                        EmploymentInformationId = c.Int(nullable: false, identity: true),
                        SourceOfFunds = c.Byte(nullable: false),
                        SourceOfFundsInfo = c.String(),
                        Employer = c.String(),
                        Position = c.String(),
                        FormOfBusiness = c.Byte(nullable: false),
                        EmployerBusinessAddress = c.String(),
                        ContactNo = c.String(),
                        NatureOfJob = c.String(),
                        YrsInJob = c.Byte(nullable: false),
                        IsOverseas = c.Boolean(nullable: false),
                        ClientId = c.Int(nullable: false),
                        DateTimeCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmploymentInformationId);
            
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
            AlterColumn("dbo.Clients", "ClientId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Clients", "ClientId");
            CreateIndex("dbo.Clients", "ClientId");
            AddForeignKey("dbo.Clients", "ClientId", "dbo.EmploymentInformations", "EmploymentInformationId");
            AddForeignKey("dbo.LoanApplications", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanApplications", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.TwoFactorAuthSMS", "LoanApplicationId", "dbo.LoanApplications");
            DropForeignKey("dbo.Clients", "ClientId", "dbo.EmploymentInformations");
            DropIndex("dbo.TwoFactorAuthSMS", new[] { "LoanApplicationId" });
            DropIndex("dbo.Clients", new[] { "ClientId" });
            DropPrimaryKey("dbo.Clients");
            AlterColumn("dbo.Clients", "ClientId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Clients", "AccessToken");
            DropTable("dbo.TwoFactorAuthSMS");
            DropTable("dbo.EmploymentInformations");
            AddPrimaryKey("dbo.Clients", "ClientId");
            AddForeignKey("dbo.LoanApplications", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
    }
}
