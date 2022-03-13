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
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    RussianWordId = table.Column<int>(type: "int", nullable: false),
                    FirstFormId = table.Column<int>(type: "int", nullable: false),
                    SecondFormId = table.Column<int>(type: "int", nullable: false),
                    ThirdFormId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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
                        name: "FK_IrregularVerbs_RussianUnits_RussianWordId",
                        column: x => x.RussianWordId,
                        principalTable: "RussianUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CommonWordLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonWordLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonWordLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerbPrepositionLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerbPrepositionLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerbPrepositionLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommonRelations",
                columns: table => new
                {
                    WordListId = table.Column<int>(type: "int", nullable: false),
                    RussianWordId = table.Column<int>(type: "int", nullable: false),
                    EnglishWordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonRelations", x => new { x.EnglishWordId, x.RussianWordId, x.WordListId });
                    table.ForeignKey(
                        name: "FK_CommonRelations_CommonWordLists_WordListId",
                        column: x => x.WordListId,
                        principalTable: "CommonWordLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommonRelations_EnglishUnits_EnglishWordId",
                        column: x => x.EnglishWordId,
                        principalTable: "EnglishUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CommonRelations_RussianUnits_RussianWordId",
                        column: x => x.RussianWordId,
                        principalTable: "RussianUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "VerbPrepositions",
                columns: table => new
                {
                    VerbPrepositionListId = table.Column<int>(type: "int", nullable: false),
                    PrepositionId = table.Column<int>(type: "int", nullable: false),
                    VerbId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerbPrepositions", x => new { x.VerbId, x.PrepositionId, x.VerbPrepositionListId });
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
                        name: "FK_VerbPrepositions_VerbPrepositionLists_VerbPrepositionListId",
                        column: x => x.VerbPrepositionListId,
                        principalTable: "VerbPrepositionLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    RussianTranslationId = table.Column<int>(type: "int", nullable: false),
                    EnglishTranslationId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommonRelationEnglishWordId = table.Column<int>(type: "int", nullable: true),
                    CommonRelationRussianWordId = table.Column<int>(type: "int", nullable: true),
                    CommonRelationWordListId = table.Column<int>(type: "int", nullable: true),
                    IrregularVerbId = table.Column<int>(type: "int", nullable: true),
                    VerbPrepositionListId = table.Column<int>(type: "int", nullable: true),
                    VerbPrepositionPrepositionId = table.Column<int>(type: "int", nullable: true),
                    VerbPrepositionVerbId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => new { x.RussianTranslationId, x.EnglishTranslationId });
                    table.ForeignKey(
                        name: "FK_Examples_CommonRelations_CommonRelationEnglishWordId_CommonRelationRussianWordId_CommonRelationWordListId",
                        columns: x => new { x.CommonRelationEnglishWordId, x.CommonRelationRussianWordId, x.CommonRelationWordListId },
                        principalTable: "CommonRelations",
                        principalColumns: new[] { "EnglishWordId", "RussianWordId", "WordListId" });
                    table.ForeignKey(
                        name: "FK_Examples_EnglishUnits_EnglishTranslationId",
                        column: x => x.EnglishTranslationId,
                        principalTable: "EnglishUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Examples_IrregularVerbs_IrregularVerbId",
                        column: x => x.IrregularVerbId,
                        principalTable: "IrregularVerbs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Examples_RussianUnits_RussianTranslationId",
                        column: x => x.RussianTranslationId,
                        principalTable: "RussianUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Examples_VerbPrepositions_VerbPrepositionVerbId_VerbPrepositionPrepositionId_VerbPrepositionListId",
                        columns: x => new { x.VerbPrepositionVerbId, x.VerbPrepositionPrepositionId, x.VerbPrepositionListId },
                        principalTable: "VerbPrepositions",
                        principalColumns: new[] { "VerbId", "PrepositionId", "VerbPrepositionListId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommonRelations_RussianWordId",
                table: "CommonRelations",
                column: "RussianWordId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonRelations_WordListId",
                table: "CommonRelations",
                column: "WordListId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonWordLists_UserId",
                table: "CommonWordLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_CommonRelationEnglishWordId_CommonRelationRussianWordId_CommonRelationWordListId",
                table: "Examples",
                columns: new[] { "CommonRelationEnglishWordId", "CommonRelationRussianWordId", "CommonRelationWordListId" });

            migrationBuilder.CreateIndex(
                name: "IX_Examples_EnglishTranslationId",
                table: "Examples",
                column: "EnglishTranslationId");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_IrregularVerbId",
                table: "Examples",
                column: "IrregularVerbId");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_VerbPrepositionVerbId_VerbPrepositionPrepositionId_VerbPrepositionListId",
                table: "Examples",
                columns: new[] { "VerbPrepositionVerbId", "VerbPrepositionPrepositionId", "VerbPrepositionListId" });

            migrationBuilder.CreateIndex(
                name: "IX_IrregularVerbs_FirstFormId",
                table: "IrregularVerbs",
                column: "FirstFormId");

            migrationBuilder.CreateIndex(
                name: "IX_IrregularVerbs_RussianWordId",
                table: "IrregularVerbs",
                column: "RussianWordId");

            migrationBuilder.CreateIndex(
                name: "IX_IrregularVerbs_SecondFormId",
                table: "IrregularVerbs",
                column: "SecondFormId");

            migrationBuilder.CreateIndex(
                name: "IX_IrregularVerbs_ThirdFormId",
                table: "IrregularVerbs",
                column: "ThirdFormId");

            migrationBuilder.CreateIndex(
                name: "IX_VerbPrepositionLists_UserId",
                table: "VerbPrepositionLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VerbPrepositions_PrepositionId",
                table: "VerbPrepositions",
                column: "PrepositionId");

            migrationBuilder.CreateIndex(
                name: "IX_VerbPrepositions_VerbPrepositionListId",
                table: "VerbPrepositions",
                column: "VerbPrepositionListId");
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
                name: "CommonWordLists");

            migrationBuilder.DropTable(
                name: "RussianUnits");

            migrationBuilder.DropTable(
                name: "EnglishUnits");

            migrationBuilder.DropTable(
                name: "VerbPrepositionLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
