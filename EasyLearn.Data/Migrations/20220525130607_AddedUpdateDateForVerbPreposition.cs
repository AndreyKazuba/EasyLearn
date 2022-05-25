using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    public partial class AddedUpdateDateForVerbPreposition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateUtc",
                table: "VerbPrepositions",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDateUtc",
                table: "VerbPrepositions");
        }
    }
}
