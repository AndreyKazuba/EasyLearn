using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    public partial class RemovedDictionaryDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "VerbPrepositionDictionaries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CommonDictionaries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VerbPrepositionDictionaries",
                type: "nvarchar(85)",
                maxLength: 85,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CommonDictionaries",
                type: "nvarchar(85)",
                maxLength: 85,
                nullable: true);
        }
    }
}
