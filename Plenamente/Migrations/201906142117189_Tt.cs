namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resultadoes", "Resu_Justificacion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resultadoes", "Resu_Justificacion");
        }
    }
}
