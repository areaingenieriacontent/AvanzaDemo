namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregadoColumnaHoraAct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActiCumplimientoes", "HoraAct", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActiCumplimientoes", "HoraAct");
        }
    }
}
