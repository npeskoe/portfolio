namespace DvdLibraryWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DVDs",
                c => new
                    {
                        DvdID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ReleaseYear = c.Int(nullable: false),
                        Director = c.String(),
                        Rating = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.DvdID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DVDs");
        }
    }
}
