namespace imdb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imdbDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.fav_actor",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        id_User = c.Int(nullable: false),
                        id_actor = c.Int(nullable: false),
                        Actor_id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.actors", t => t.Actor_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Actor_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.fav_dir",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        id_User = c.Int(nullable: false),
                        id_director = c.Int(nullable: false),
                        Director_id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.directors", t => t.Director_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Director_id)
                .Index(t => t.User_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.fav_dir", "User_id", "dbo.Users");
            DropForeignKey("dbo.fav_dir", "Director_id", "dbo.directors");
            DropForeignKey("dbo.fav_actor", "User_id", "dbo.Users");
            DropForeignKey("dbo.fav_actor", "Actor_id", "dbo.actors");
            DropIndex("dbo.fav_dir", new[] { "User_id" });
            DropIndex("dbo.fav_dir", new[] { "Director_id" });
            DropIndex("dbo.fav_actor", new[] { "User_id" });
            DropIndex("dbo.fav_actor", new[] { "Actor_id" });
            DropTable("dbo.fav_dir");
            DropTable("dbo.fav_actor");
        }
    }
}
