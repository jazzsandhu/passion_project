namespace passion_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.posts", "tag_tag_id", "dbo.tags");
            DropForeignKey("dbo.posts", "tag_id", "dbo.tags");
            DropForeignKey("dbo.tags", "post_post_id", "dbo.posts");
            DropIndex("dbo.posts", new[] { "tag_id" });
            DropIndex("dbo.posts", new[] { "tag_tag_id" });
            DropIndex("dbo.tags", new[] { "post_post_id" });
            CreateTable(
                "dbo.tagposts",
                c => new
                    {
                        tag_tag_id = c.Int(nullable: false),
                        post_post_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.tag_tag_id, t.post_post_id })
                .ForeignKey("dbo.tags", t => t.tag_tag_id, cascadeDelete: true)
                .ForeignKey("dbo.posts", t => t.post_post_id, cascadeDelete: true)
                .Index(t => t.tag_tag_id)
                .Index(t => t.post_post_id);
            
            DropColumn("dbo.posts", "tag_id");
            DropColumn("dbo.posts", "tag_tag_id");
            DropColumn("dbo.tags", "post_post_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tags", "post_post_id", c => c.Int());
            AddColumn("dbo.posts", "tag_tag_id", c => c.Int());
            AddColumn("dbo.posts", "tag_id", c => c.Int(nullable: false));
            DropForeignKey("dbo.tagposts", "post_post_id", "dbo.posts");
            DropForeignKey("dbo.tagposts", "tag_tag_id", "dbo.tags");
            DropIndex("dbo.tagposts", new[] { "post_post_id" });
            DropIndex("dbo.tagposts", new[] { "tag_tag_id" });
            DropTable("dbo.tagposts");
            CreateIndex("dbo.tags", "post_post_id");
            CreateIndex("dbo.posts", "tag_tag_id");
            CreateIndex("dbo.posts", "tag_id");
            AddForeignKey("dbo.tags", "post_post_id", "dbo.posts", "post_id");
            AddForeignKey("dbo.posts", "tag_id", "dbo.tags", "tag_id", cascadeDelete: true);
            AddForeignKey("dbo.posts", "tag_tag_id", "dbo.tags", "tag_id");
        }
    }
}
