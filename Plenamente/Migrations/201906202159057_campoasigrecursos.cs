namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campoasigrecursos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActiCumplimientoes", "asigrecursos", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActiCumplimientoes", "asigrecursos");
        }
    }
}
