namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documentfile_added_TimestampCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanApplicationDocumentFiles", "TimestampCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoanApplicationDocumentFiles", "TimestampCreated");
        }
    }
}
