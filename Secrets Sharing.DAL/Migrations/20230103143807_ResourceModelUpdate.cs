using Microsoft.EntityFrameworkCore.Migrations;

namespace Secrets_Sharing.DAL.Migrations
{
    public partial class ResourceModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AutoRemoveable",
                table: "Resources",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoRemoveable",
                table: "Resources");
        }
    }
}
