namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanApplicationDocumentFiles",
                c => new
                    {
                        LoanApplicationDocumentFileId = c.Int(nullable: false, identity: true),
                        FileType = c.String(maxLength: 5),
                        Url = c.String(),
                        LoanApplicationDocumentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoanApplicationDocumentFileId)
                .ForeignKey("dbo.LoanApplicationDocuments", t => t.LoanApplicationDocumentId, cascadeDelete: true)
                .Index(t => t.LoanApplicationDocumentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanApplicationDocumentFiles", "LoanApplicationDocumentId", "dbo.LoanApplicationDocuments");
            DropIndex("dbo.LoanApplicationDocumentFiles", new[] { "LoanApplicationDocumentId" });
            DropTable("dbo.LoanApplicationDocumentFiles");
        }
    }
}
