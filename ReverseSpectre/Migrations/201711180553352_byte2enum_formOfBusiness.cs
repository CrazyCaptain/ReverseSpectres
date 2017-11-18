namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class byte2enum_formOfBusiness : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmploymentInformations", "SourceOfFundsInfo", c => c.String());
            AlterColumn("dbo.EmploymentInformations", "FormOfBusiness", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmploymentInformations", "FormOfBusiness", c => c.Byte(nullable: false));
            AlterColumn("dbo.EmploymentInformations", "SourceOfFundsInfo", c => c.String(nullable: false));
        }
    }
}
