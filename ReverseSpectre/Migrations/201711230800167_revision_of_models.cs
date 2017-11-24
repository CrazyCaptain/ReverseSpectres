namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revision_of_models : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmploymentInformations", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.LoanApplicationReferences", "LoanApplicationId", "dbo.LoanApplications");
            DropForeignKey("dbo.LoanApplications", "LoanTypeId", "dbo.LoanTypes");
            DropForeignKey("dbo.LoanApplicationPartners", "LoanApplicationId", "dbo.LoanApplications");
            DropForeignKey("dbo.RelationshipManagers", "BankId", "dbo.Banks");
            DropIndex("dbo.EmploymentInformations", new[] { "ClientId" });
            DropIndex("dbo.LoanApplications", new[] { "LoanTypeId" });
            DropIndex("dbo.LoanApplicationReferences", new[] { "LoanApplicationId" });
            DropIndex("dbo.LoanApplicationPartners", new[] { "LoanApplicationId" });
            DropIndex("dbo.RelationshipManagers", new[] { "BankId" });
            CreateTable(
                "dbo.BusinessManagers",
                c => new
                    {
                        BusinessManagerId = c.Int(nullable: false, identity: true),
                        TimestampCreated = c.DateTime(nullable: false),
                        BankId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BusinessManagerId)
                .ForeignKey("dbo.Banks", t => t.BankId, cascadeDelete: true)
                .Index(t => t.BankId);
            
            CreateTable(
                "dbo.ClientInvitations",
                c => new
                    {
                        ClientInvitationId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        BusinessName = c.String(),
                        FormOfBusiness = c.Int(nullable: false),
                        Token = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClientInvitationId);
            
            CreateTable(
                "dbo.ContactInformations",
                c => new
                    {
                        ContactInformationId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        IsFemale = c.Boolean(nullable: false),
                        Position = c.String(),
                        Email = c.String(),
                        MobileNumber = c.String(),
                        TimestampCreated = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactInformationId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            AddColumn("dbo.Clients", "BusinessName", c => c.String());
            AddColumn("dbo.Clients", "BusinessAddress", c => c.String());
            AddColumn("dbo.Clients", "FormOfBusiness", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "TelephoneNumber", c => c.String());
            AddColumn("dbo.Clients", "RelationshipManagerId", c => c.Int(nullable: false));
            AddColumn("dbo.LoanApplications", "Interest", c => c.Double(nullable: false));
            AddColumn("dbo.RelationshipManagers", "TimestampCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.RelationshipManagers", "BusinessManagerId", c => c.Int(nullable: false));
            CreateIndex("dbo.RelationshipManagers", "BusinessManagerId");
            CreateIndex("dbo.Clients", "RelationshipManagerId");
            AddForeignKey("dbo.RelationshipManagers", "BusinessManagerId", "dbo.BusinessManagers", "BusinessManagerId", cascadeDelete: true);
            AddForeignKey("dbo.Clients", "RelationshipManagerId", "dbo.RelationshipManagers", "RelationshipManagerId", cascadeDelete: true);
            DropColumn("dbo.Clients", "FirstName");
            DropColumn("dbo.Clients", "MiddleName");
            DropColumn("dbo.Clients", "LastName");
            DropColumn("dbo.Clients", "Birthdate");
            DropColumn("dbo.Clients", "Nationality");
            DropColumn("dbo.Clients", "CivilStatus");
            DropColumn("dbo.Clients", "TIN");
            DropColumn("dbo.Clients", "SSS");
            DropColumn("dbo.Clients", "CurrentAddress");
            DropColumn("dbo.Clients", "PermanentAddress");
            DropColumn("dbo.Clients", "MobileNumber");
            DropColumn("dbo.LoanApplications", "LoanTypeId");
            DropColumn("dbo.RelationshipManagers", "BankId");
            DropTable("dbo.EmploymentInformations");
            DropTable("dbo.LoanApplicationReferences");
            DropTable("dbo.LoanTypes");
            DropTable("dbo.LoanApplicationPartners");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoanApplicationPartners",
                c => new
                    {
                        LoanApplicationPartnerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        IsFemale = c.Boolean(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Nationality = c.String(nullable: false),
                        Relationship = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Employer = c.String(),
                        Position = c.String(nullable: false),
                        EmployerAddress = c.String(nullable: false),
                        YearsInJob = c.Int(nullable: false),
                        LoanApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoanApplicationPartnerId);
            
            CreateTable(
                "dbo.LoanTypes",
                c => new
                    {
                        LoanTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        InterestRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LoanTypeId);
            
            CreateTable(
                "dbo.LoanApplicationReferences",
                c => new
                    {
                        LoanApplicationReferenceId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        LoanApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoanApplicationReferenceId);
            
            CreateTable(
                "dbo.EmploymentInformations",
                c => new
                    {
                        EmploymentInformationId = c.Int(nullable: false, identity: true),
                        SourceOfFunds = c.Int(nullable: false),
                        SourceOfFundsInfo = c.String(),
                        Employer = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        FormOfBusiness = c.Int(nullable: false),
                        EmployerBusinessAddress = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        NatureOfJob = c.String(nullable: false),
                        YearsInJob = c.Byte(nullable: false),
                        IsOFW = c.Boolean(nullable: false),
                        TimestampCreated = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmploymentInformationId);
            
            AddColumn("dbo.RelationshipManagers", "BankId", c => c.Int(nullable: false));
            AddColumn("dbo.LoanApplications", "LoanTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "MobileNumber", c => c.String());
            AddColumn("dbo.Clients", "PermanentAddress", c => c.String());
            AddColumn("dbo.Clients", "CurrentAddress", c => c.String());
            AddColumn("dbo.Clients", "SSS", c => c.String());
            AddColumn("dbo.Clients", "TIN", c => c.String());
            AddColumn("dbo.Clients", "CivilStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "Nationality", c => c.String());
            AddColumn("dbo.Clients", "Birthdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clients", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "MiddleName", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "FirstName", c => c.String(nullable: false));
            DropForeignKey("dbo.Clients", "RelationshipManagerId", "dbo.RelationshipManagers");
            DropForeignKey("dbo.ContactInformations", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.RelationshipManagers", "BusinessManagerId", "dbo.BusinessManagers");
            DropForeignKey("dbo.BusinessManagers", "BankId", "dbo.Banks");
            DropIndex("dbo.ContactInformations", new[] { "ClientId" });
            DropIndex("dbo.Clients", new[] { "RelationshipManagerId" });
            DropIndex("dbo.RelationshipManagers", new[] { "BusinessManagerId" });
            DropIndex("dbo.BusinessManagers", new[] { "BankId" });
            DropColumn("dbo.RelationshipManagers", "BusinessManagerId");
            DropColumn("dbo.RelationshipManagers", "TimestampCreated");
            DropColumn("dbo.LoanApplications", "Interest");
            DropColumn("dbo.Clients", "RelationshipManagerId");
            DropColumn("dbo.Clients", "TelephoneNumber");
            DropColumn("dbo.Clients", "FormOfBusiness");
            DropColumn("dbo.Clients", "BusinessAddress");
            DropColumn("dbo.Clients", "BusinessName");
            DropTable("dbo.ContactInformations");
            DropTable("dbo.ClientInvitations");
            DropTable("dbo.BusinessManagers");
            CreateIndex("dbo.RelationshipManagers", "BankId");
            CreateIndex("dbo.LoanApplicationPartners", "LoanApplicationId");
            CreateIndex("dbo.LoanApplicationReferences", "LoanApplicationId");
            CreateIndex("dbo.LoanApplications", "LoanTypeId");
            CreateIndex("dbo.EmploymentInformations", "ClientId");
            AddForeignKey("dbo.RelationshipManagers", "BankId", "dbo.Banks", "BankId", cascadeDelete: true);
            AddForeignKey("dbo.LoanApplicationPartners", "LoanApplicationId", "dbo.LoanApplications", "LoanApplicationId", cascadeDelete: true);
            AddForeignKey("dbo.LoanApplications", "LoanTypeId", "dbo.LoanTypes", "LoanTypeId", cascadeDelete: true);
            AddForeignKey("dbo.LoanApplicationReferences", "LoanApplicationId", "dbo.LoanApplications", "LoanApplicationId", cascadeDelete: true);
            AddForeignKey("dbo.EmploymentInformations", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
    }
}
