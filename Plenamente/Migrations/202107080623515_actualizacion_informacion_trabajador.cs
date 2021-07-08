namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actualizacion_informacion_trabajador : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Pers_Temeg", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Pers_Temeg", c => c.Int(nullable: false));
        }
    }
}
