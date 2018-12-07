namespace ClearChoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pessoas", "CEP1");
            DropColumn("dbo.Pessoas", "UF1");
            DropColumn("dbo.Pessoas", "Localidade1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoas", "Localidade1", c => c.String());
            AddColumn("dbo.Pessoas", "UF1", c => c.String());
            AddColumn("dbo.Pessoas", "CEP1", c => c.String());
        }
    }
}
