using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    public partial class SomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "VerbPrepositions");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateUtc",
                table: "CommonRelations",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDateUtc",
                table: "CommonRelations");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "VerbPrepositions",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);
        }
    }
}
