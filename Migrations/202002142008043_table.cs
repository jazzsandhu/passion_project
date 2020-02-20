namespace passion_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.posts",
                c => new
                    {
                        post_id = c.Int(nullable: false, identity: true),
                        post_name = c.String(),
                        post_description = c.String(),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.post_id)
                .ForeignKey("dbo.users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id);
            
            AddColumn("dbo.tags", "post_post_id", c => c.Int());
            CreateIndex("dbo.tags", "post_post_id");
            AddForeignKey("dbo.tags", "post_post_id", "dbo.posts", "post_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.posts", "user_id", "dbo.users");
            DropForeignKey("dbo.tags", "post_post_id", "dbo.posts");
            DropIndex("dbo.tags", new[] { "post_post_id" });
            DropIndex("dbo.posts", new[] { "user_id" });
            DropColumn("dbo.tags", "post_post_id");
            DropTable("dbo.posts");
        }
    }
}
