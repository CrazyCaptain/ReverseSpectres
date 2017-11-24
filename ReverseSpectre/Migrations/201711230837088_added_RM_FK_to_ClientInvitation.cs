namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_RM_FK_to_ClientInvitation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientInvitations", "RelationshipManagerId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClientInvitations", "RelationshipManagerId");
            AddForeignKey("dbo.ClientInvitations", "RelationshipManagerId", "dbo.RelationshipManagers", "RelationshipManagerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientInvitations", "RelationshipManagerId", "dbo.RelationshipManagers");
            DropIndex("dbo.ClientInvitations", new[] { "RelationshipManagerId" });
            DropColumn("dbo.ClientInvitations", "RelationshipManagerId");
        }
    }
}
