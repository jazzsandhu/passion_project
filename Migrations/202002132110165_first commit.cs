namespace passion_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstcommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tags",
                c => new
                    {
                        tag_id = c.Int(nullable: false, identity: true),
                        tag_name = c.String(),
                        tag_description = c.String(),
                    })
                .PrimaryKey(t => t.tag_id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        user_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        e_mail = c.String(),
                        phone_number = c.String(),
                        home_address = c.String(),
                        user_name = c.String(),
                    })
                .PrimaryKey(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.users");
            DropTable("dbo.tags");
        }
    }
}
