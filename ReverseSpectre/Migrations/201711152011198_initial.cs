namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Int(nullable: false, identity: true),
                        BranchName = c.String(),
                    })
                .PrimaryKey(t => t.BankId);
            
            CreateTable(
                "dbo.LoanApplicationDocuments",
                c => new
                    {
                        LoanApplicationDocumentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        TimestampCreated = c.DateTime(nullable: false),
                        Comment = c.String(),
                        LoanApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoanApplicationDocumentId)
                .ForeignKey("dbo.LoanApplications", t => t.LoanApplicationId, cascadeDelete: true)
                .Index(t => t.LoanApplicationId);
            
            CreateTable(
                "dbo.LoanApplications",
                c => new
                    {
                        LoanApplicationId = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Term = c.Int(nullable: false),
                        TimestampCreated = c.DateTime(nullable: false),
                        LoanStatus = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        LoanTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoanApplicationId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.LoanTypes", t => t.LoanTypeId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.LoanTypeId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Nationality = c.String(nullable: false),
                        CivilStatus = c.Int(nullable: false),
                        TIN = c.String(nullable: false),
                        SSS = c.String(),
                        CurrentAddress = c.String(nullable: false),
                        PermanentAddress = c.String(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                .PrimaryKey(t => t.LoanApplicationReferenceId)
                .ForeignKey("dbo.LoanApplications", t => t.LoanApplicationId, cascadeDelete: true)
                .Index(t => t.LoanApplicationId);
            
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
                .PrimaryKey(t => t.LoanApplicationPartnerId)
                .ForeignKey("dbo.LoanApplications", t => t.LoanApplicationId, cascadeDelete: true)
                .Index(t => t.LoanApplicationId);
            
            CreateTable(
                "dbo.RelationshipManagers",
                c => new
                    {
                        RelationshipManagerId = c.Int(nullable: false, identity: true),
                        IsDisabled = c.Boolean(nullable: false),
                        BankId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelationshipManagerId)
                .ForeignKey("dbo.Banks", t => t.BankId, cascadeDelete: true)
                .Index(t => t.BankId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RelationshipManagers", "BankId", "dbo.Banks");
            DropForeignKey("dbo.LoanApplicationPartners", "LoanApplicationId", "dbo.LoanApplications");
            DropForeignKey("dbo.LoanApplicationDocuments", "LoanApplicationId", "dbo.LoanApplications");
            DropForeignKey("dbo.LoanApplications", "LoanTypeId", "dbo.LoanTypes");
            DropForeignKey("dbo.LoanApplicationReferences", "LoanApplicationId", "dbo.LoanApplications");
            DropForeignKey("dbo.LoanApplications", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RelationshipManagers", new[] { "BankId" });
            DropIndex("dbo.LoanApplicationPartners", new[] { "LoanApplicationId" });
            DropIndex("dbo.LoanApplicationReferences", new[] { "LoanApplicationId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropIndex("dbo.LoanApplications", new[] { "LoanTypeId" });
            DropIndex("dbo.LoanApplications", new[] { "ClientId" });
            DropIndex("dbo.LoanApplicationDocuments", new[] { "LoanApplicationId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RelationshipManagers");
            DropTable("dbo.LoanApplicationPartners");
            DropTable("dbo.LoanTypes");
            DropTable("dbo.LoanApplicationReferences");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Clients");
            DropTable("dbo.LoanApplications");
            DropTable("dbo.LoanApplicationDocuments");
            DropTable("dbo.Banks");
        }
    }
}
