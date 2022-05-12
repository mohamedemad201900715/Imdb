namespace imdb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imdb_v1Db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.act_in_mov",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        id_mov = c.Int(nullable: false),
                        id_act = c.Int(nullable: false),
                        actors_id = c.Int(),
                        movies_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.actors", t => t.actors_id)
                .ForeignKey("dbo.movies", t => t.movies_id)
                .Index(t => t.actors_id)
                .Index(t => t.movies_id);
            
            CreateTable(
                "dbo.actors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        photo_act = c.String(),
                        act_In_Mov_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.movies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        photo_movie = c.String(),
                        description = c.String(nullable: false, maxLength: 200),
                        id_director = c.Int(nullable: false),
                        act_In_Mov_id = c.Int(nullable: false),
                        director_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.directors", t => t.director_id)
                .Index(t => t.director_id);
            
            CreateTable(
                "dbo.directors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        photo_dir = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.fav_movies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        id_User = c.Int(nullable: false),
                        id_movie = c.Int(nullable: false),
                        Movie_id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.movies", t => t.Movie_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Movie_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FristName = c.String(nullable: false, maxLength: 50),
                        lastName = c.String(nullable: false, maxLength: 50),
                        Photo_user = c.String(),
                        username = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.fav_movies", "User_id", "dbo.Users");
            DropForeignKey("dbo.fav_movies", "Movie_id", "dbo.movies");
            DropForeignKey("dbo.movies", "director_id", "dbo.directors");
            DropForeignKey("dbo.act_in_mov", "movies_id", "dbo.movies");
            DropForeignKey("dbo.act_in_mov", "actors_id", "dbo.actors");
            DropIndex("dbo.fav_movies", new[] { "User_id" });
            DropIndex("dbo.fav_movies", new[] { "Movie_id" });
            DropIndex("dbo.movies", new[] { "director_id" });
            DropIndex("dbo.act_in_mov", new[] { "movies_id" });
            DropIndex("dbo.act_in_mov", new[] { "actors_id" });
            DropTable("dbo.Users");
            DropTable("dbo.fav_movies");
            DropTable("dbo.directors");
            DropTable("dbo.movies");
            DropTable("dbo.actors");
            DropTable("dbo.act_in_mov");
        }
    }
}
