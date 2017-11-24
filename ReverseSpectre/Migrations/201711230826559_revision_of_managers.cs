namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revision_of_managers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessManagers", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.RelationshipManagers", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.BusinessManagers", "UserId");
            CreateIndex("dbo.RelationshipManagers", "UserId");
            AddForeignKey("dbo.RelationshipManagers", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.BusinessManagers", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BusinessManagers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RelationshipManagers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.RelationshipManagers", new[] { "UserId" });
            DropIndex("dbo.BusinessManagers", new[] { "UserId" });
            DropColumn("dbo.RelationshipManagers", "UserId");
            DropColumn("dbo.BusinessManagers", "UserId");
        }
    }
}
