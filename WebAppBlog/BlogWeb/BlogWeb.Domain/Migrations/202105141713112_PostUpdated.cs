﻿namespace BlogWeb.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostUpdated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            RenameColumn(table: "dbo.Comments", name: "Post_Id", newName: "PostId");
            AlterColumn("dbo.Comments", "PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "PostId");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostId" });
            AlterColumn("dbo.Comments", "PostId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "PostId", newName: "Post_Id");
            CreateIndex("dbo.Comments", "Post_Id");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.Posts", "Id");
        }
    }
}
