namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_nullable_in_Client_fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "SmsAccessToken", c => c.String());
            AlterColumn("dbo.Clients", "Nationality", c => c.String());
            AlterColumn("dbo.Clients", "TIN", c => c.String());
            AlterColumn("dbo.Clients", "CurrentAddress", c => c.String());
            AlterColumn("dbo.Clients", "PermanentAddress", c => c.String());
            AlterColumn("dbo.Clients", "MobileNumber", c => c.String());
            DropColumn("dbo.Clients", "AccessToken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "AccessToken", c => c.String());
            AlterColumn("dbo.Clients", "MobileNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "PermanentAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "CurrentAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "TIN", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Nationality", c => c.String(nullable: false));
            DropColumn("dbo.Clients", "SmsAccessToken");
        }
    }
}
