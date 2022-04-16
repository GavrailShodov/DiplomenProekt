using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAplicationForServices.Server.Migrations
{
    public partial class hpurs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Orders");
        }
    }
}
