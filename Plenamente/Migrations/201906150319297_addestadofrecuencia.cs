namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addestadofrecuencia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Frecuencias", "Frec_Descripcion", c => c.String());
            AddColumn("dbo.Frecuencias", "Estado", c => c.Boolean(nullable: false));
            DropColumn("dbo.Frecuencias", "Frec_Nom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Frecuencias", "Frec_Nom", c => c.String());
            DropColumn("dbo.Frecuencias", "Estado");
            DropColumn("dbo.Frecuencias", "Frec_Descripcion");
        }
    }
}
