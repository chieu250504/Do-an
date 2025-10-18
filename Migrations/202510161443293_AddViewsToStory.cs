namespace Nettruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewsToStory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stories", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stories", "Views");
        }
    }
}
