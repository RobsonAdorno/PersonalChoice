namespace ClearChoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablerr : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pessoas", "estaPermitidoJuridica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoas", "estaPermitidoJuridica", c => c.Boolean());
        }
    }
}
