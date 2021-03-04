namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actBd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Resultadoes", "Resu_Justificacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resultadoes", "Resu_Justificacion", c => c.String());
        }
    }
}
