using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    public partial class RatingAddedToRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersStreak",
                table: "VerbPrepositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "VerbPrepositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Studied",
                table: "VerbPrepositions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "IrregularVerbs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswersStreak",
                table: "CommonRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "CommonRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Studied",
                table: "CommonRelations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswersStreak",
                table: "VerbPrepositions");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "VerbPrepositions");

            migrationBuilder.DropColumn(
                name: "Studied",
                table: "VerbPrepositions");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "IrregularVerbs");

            migrationBuilder.DropColumn(
                name: "CorrectAnswersStreak",
                table: "CommonRelations");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "CommonRelations");

            migrationBuilder.DropColumn(
                name: "Studied",
                table: "CommonRelations");
        }
    }
}
