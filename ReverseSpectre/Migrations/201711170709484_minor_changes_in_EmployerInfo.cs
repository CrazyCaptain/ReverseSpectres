namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minor_changes_in_EmployerInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmploymentInformations", "TimestampCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmploymentInformations", "SourceOfFundsInfo", c => c.String(nullable: false));
            AlterColumn("dbo.EmploymentInformations", "Employer", c => c.String(nullable: false));
            AlterColumn("dbo.EmploymentInformations", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.EmploymentInformations", "EmployerBusinessAddress", c => c.String(nullable: false));
            AlterColumn("dbo.EmploymentInformations", "ContactNumber", c => c.String(nullable: false));
            AlterColumn("dbo.EmploymentInformations", "NatureOfJob", c => c.String(nullable: false));
            DropColumn("dbo.EmploymentInformations", "DateTimeCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmploymentInformations", "DateTimeCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.EmploymentInformations", "NatureOfJob", c => c.String());
            AlterColumn("dbo.EmploymentInformations", "ContactNumber", c => c.String());
            AlterColumn("dbo.EmploymentInformations", "EmployerBusinessAddress", c => c.String());
            AlterColumn("dbo.EmploymentInformations", "Position", c => c.String());
            AlterColumn("dbo.EmploymentInformations", "Employer", c => c.String());
            AlterColumn("dbo.EmploymentInformations", "SourceOfFundsInfo", c => c.String());
            DropColumn("dbo.EmploymentInformations", "TimestampCreated");
        }
    }
}
