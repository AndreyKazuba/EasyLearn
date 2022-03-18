using EasyLearn.Data.Sql;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    public partial class Add_IrregularVerbs : Migration
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
