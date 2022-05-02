using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnglishUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnglishUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RussianUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RussianUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IrregularVerbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RussianUnitId = table.Column<int>(type: "int", nullable: false),
                    FirstFormId = table.Column<int>(type: "int", nullable: false),
                    SecondFormId = table.Column<int>(type: "int", nullable: false),
                    ThirdFormId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrregularVerbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IrregularVerbs_EnglishUnits_FirstFormId",
                        column: x => x.FirstFormId,
                        principalTable: "EnglishUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_IrregularVerbs_EnglishUnits_SecondFormId",
                        column: x => x.SecondFormId,
                        principalTable: "EnglishUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_IrregularVerbs_EnglishUnits_ThirdFormId",
                        column: x => x.ThirdFormId,
                        principalTable: "EnglishUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_IrregularVerbs_RussianUnits_RussianUnitId",
                        column: x => x.RussianUnitId,
                        principalTable: "RussianUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CommonDictionaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonDictionaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonDictionaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerbPrepositionDictionaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerbPrepositionDictionaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerbPrepositionDictionaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommonRelations",
                columns: table => new
                {
                    CommonDictionaryId = table.Column<int>(type: "int", nullable: false),
                    RussianUnitId = table.Column<int>(type: "int", nullable: false),
                    EnglishUnitId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonRelations", x => new { x.EnglishUnitId, x.RussianUnitId, x.CommonDictionaryId });
                    table.ForeignKey(
                        name: "FK_CommonRelations_CommonDictionaries_CommonDictionaryId",
                        column: x => x.CommonDictionaryId,
                        principalTable: "CommonDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommonRelations_EnglishUnits_EnglishUnitId",
                        column: x => x.EnglishUnitId,
                        principalTable: "EnglishUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CommonRelations_RussianUnits_RussianUnitId",
                        column: x => x.RussianUnitId,
                        principalTable: "RussianUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "VerbPrepositions",
                columns: table => new
                {
                    VerbPrepositionDictionaryId = table.Column<int>(type: "int", nullable: false),
                    PrepositionId = table.Column<int>(type: "int", nullable: false),
                    VerbId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Translation = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerbPrepositions", x => new { x.VerbId, x.PrepositionId, x.VerbPrepositionDictionaryId });
                    table.ForeignKey(
                        name: "FK_VerbPrepositions_EnglishUnits_PrepositionId",
                        column: x => x.PrepositionId,
                        principalTable: "EnglishUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_VerbPrepositions_EnglishUnits_VerbId",
                        column: x => x.VerbId,
                        principalTable: "EnglishUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_VerbPrepositions_VerbPrepositionDictionaries_VerbPrepositionDictionaryId",
                        column: x => x.VerbPrepositionDictionaryId,
                        principalTable: "VerbPrepositionDictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RussianValue = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    EnglishValue = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommonRelationCommonDictionaryId = table.Column<int>(type: "int", nullable: true),
                    CommonRelationEnglishUnitId = table.Column<int>(type: "int", nullable: true),
                    CommonRelationRussianUnitId = table.Column<int>(type: "int", nullable: true),
                    IrregularVerbId = table.Column<int>(type: "int", nullable: true),
                    VerbPrepositionDictionaryId = table.Column<int>(type: "int", nullable: true),
                    VerbPrepositionPrepositionId = table.Column<int>(type: "int", nullable: true),
                    VerbPrepositionVerbId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examples_CommonRelations_CommonRelationEnglishUnitId_CommonRelationRussianUnitId_CommonRelationCommonDictionaryId",
                        columns: x => new { x.CommonRelationEnglishUnitId, x.CommonRelationRussianUnitId, x.CommonRelationCommonDictionaryId },
                        principalTable: "CommonRelations",
                        principalColumns: new[] { "EnglishUnitId", "RussianUnitId", "CommonDictionaryId" });
                    table.ForeignKey(
                        name: "FK_Examples_IrregularVerbs_IrregularVerbId",
                        column: x => x.IrregularVerbId,
                        principalTable: "IrregularVerbs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Examples_VerbPrepositions_VerbPrepositionVerbId_VerbPrepositionPrepositionId_VerbPrepositionDictionaryId",
                        columns: x => new { x.VerbPrepositionVerbId, x.VerbPrepositionPrepositionId, x.VerbPrepositionDictionaryId },
                        principalTable: "VerbPrepositions",
                        principalColumns: new[] { "VerbId", "PrepositionId", "VerbPrepositionDictionaryId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommonDictionaries_UserId",
                table: "CommonDictionaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonRelations_CommonDictionaryId",
                table: "CommonRelations",
                column: "CommonDictionaryId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonRelations_RussianUnitId",
                table: "CommonRelations",
                column: "RussianUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_CommonRelationEnglishUnitId_CommonRelationRussianUnitId_CommonRelationCommonDictionaryId",
                table: "Examples",
                columns: new[] { "CommonRelationEnglishUnitId", "CommonRelationRussianUnitId", "CommonRelationCommonDictionaryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Examples_IrregularVerbId",
                table: "Examples",
                column: "IrregularVerbId");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_VerbPrepositionVerbId_VerbPrepositionPrepositionId_VerbPrepositionDictionaryId",
                table: "Examples",
                columns: new[] { "VerbPrepositionVerbId", "VerbPrepositionPrepositionId", "VerbPrepositionDictionaryId" });

            migrationBuilder.CreateIndex(
                name: "IX_IrregularVerbs_FirstFormId",
                table: "IrregularVerbs",
                column: "FirstFormId");

            migrationBuilder.CreateIndex(
                name: "IX_IrregularVerbs_RussianUnitId",
                table: "IrregularVerbs",
                column: "RussianUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_IrregularVerbs_SecondFormId",
                table: "IrregularVerbs",
                column: "SecondFormId");

            migrationBuilder.CreateIndex(
                name: "IX_IrregularVerbs_ThirdFormId",
                table: "IrregularVerbs",
                column: "ThirdFormId");

            migrationBuilder.CreateIndex(
                name: "IX_VerbPrepositionDictionaries_UserId",
                table: "VerbPrepositionDictionaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VerbPrepositions_PrepositionId",
                table: "VerbPrepositions",
                column: "PrepositionId");

            migrationBuilder.CreateIndex(
                name: "IX_VerbPrepositions_VerbPrepositionDictionaryId",
                table: "VerbPrepositions",
                column: "VerbPrepositionDictionaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "CommonRelations");

            migrationBuilder.DropTable(
                name: "IrregularVerbs");

            migrationBuilder.DropTable(
                name: "VerbPrepositions");

            migrationBuilder.DropTable(
                name: "CommonDictionaries");

            migrationBuilder.DropTable(
                name: "RussianUnits");

            migrationBuilder.DropTable(
                name: "EnglishUnits");

            migrationBuilder.DropTable(
                name: "VerbPrepositionDictionaries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
