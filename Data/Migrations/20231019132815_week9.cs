using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenAITech.Data.Migrations
{
    public partial class week9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislike",
                table: "GenAI");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislike",
                table: "GenAI",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
