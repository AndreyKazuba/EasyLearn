using EasyLearn.Data.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using EasyLearn.Data.Sql;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    public partial class IrregularVerbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = SqlReader.GetSql("IrregularVerbs");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
