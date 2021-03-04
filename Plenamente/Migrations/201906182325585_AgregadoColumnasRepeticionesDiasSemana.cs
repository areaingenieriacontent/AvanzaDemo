namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregadoColumnasRepeticionesDiasSemana : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActiCumplimientoes", "DiasSemana", c => c.String());
            AddColumn("dbo.ActiCumplimientoes", "Repeticiones", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActiCumplimientoes", "Repeticiones");
            DropColumn("dbo.ActiCumplimientoes", "DiasSemana");
        }
    }
}
