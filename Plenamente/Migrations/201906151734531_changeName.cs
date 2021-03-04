namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Usersplandetrabajoes", newName: "UsuariosPlandetrabajoes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UsuariosPlandetrabajoes", newName: "Usersplandetrabajoes");
        }
    }
}
