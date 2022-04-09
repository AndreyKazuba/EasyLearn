using EasyLearn.Data.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    public partial class Add_Units_For_IrregularVerbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            int id = 1;

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "быть", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "подниматься", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "пробуждать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "рожать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "бить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "становиться", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "начинать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "наклонять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "заключать пари", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "связывать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "кусать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "кровоточить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "дуть", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "ломать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "разводить животных", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "приносить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "строить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "покупать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "ловить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "мама", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "выбирать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "цепляться", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "приходить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "стоить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "резать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "вести дела", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "копать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "делать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "рисовать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "пить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "водить автомобиль", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "кушать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "падать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "кормить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "папа", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "сестра", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "чувствовать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "бороться", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "находить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "сбегать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "летать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "запрещать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "забывать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "прощать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "замораживать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "получать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "давать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "идти", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "расти", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "висеть", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "иметь", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "слышать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "прятать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "ударять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "попадать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "держать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "причинять боль", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "сохранять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "знать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "класть", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "лидировать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "учиться", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "покидать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "давать взаймы", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "позволять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "лежать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "освещать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "терять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "мастерить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "значить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "встречать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "платить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "класть", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "читать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "ездить верхом", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "звонить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "возрастать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "бежать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "сказать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "видеть", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "искать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "продавать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "посылать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "устанавливать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "трясти", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "сиять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "стрелять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "показывать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "сжиматься", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "закрывать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "петь", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "сидеть", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "спать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "скользить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "говорить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "произносить или писать по буквам", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "тратить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "разлить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "крутить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "разделять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "портить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "распространять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "стоять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "воровать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "жалить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "вонять", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "бастовать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "клясться", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "подметать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "опухать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "плавать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "брать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "обучать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "рвать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "рассказывать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "думать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "бросать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "понимать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "будить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "носить", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "побеждать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "обматывать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "писать", (int)UnitType.Verb, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "RussianUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "пахнуть", (int)UnitType.Verb, DateTime.UtcNow });

            id = 1;


            #region V1

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "arise", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "awake", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "be", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bear", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "beat", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "become", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "begin", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bend", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bet", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bind", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bite", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bleed", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "blow", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "break", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "breed", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bring", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "build", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "buy", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "catch", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "choose", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "cling", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "come", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "cost", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "cut", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "deal", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "dig", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "do", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "draw", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "drink", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "drive", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "eat", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fall", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "feed", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "feel", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fight", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "find", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "flee", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fly", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "forbid", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "forget", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "forgive", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "freeze", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "get", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "give", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "go", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "grow", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hang", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "have", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hear", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hide", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hit", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hold", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hurt", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "keep", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "know", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lay", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lead", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "learn", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "leave", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lend", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "let", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lie", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "light", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lose", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "make", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "mean", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "meet", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "pay", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "put", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "read", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "ride", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "ring", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "rise", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "run", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "say", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "see", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "seek", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sell", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "send", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "set", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shake", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shine", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shoot", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "show", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shrink", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shut", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sing", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sit", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sleep", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "slide", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "smell", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "speak", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spell", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spend", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spill", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spin", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "split", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spoil", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spread", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stand", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "steal", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sting", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stink", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "strike", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swear", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sweep", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swell", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swim", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "take", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "teach", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "tear", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "tell", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "think", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "throw", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "understand", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "wake", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "wear", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "win", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "wind", (int)UnitType.IrregularV1, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "write", (int)UnitType.IrregularV1, DateTime.UtcNow });

            #endregion

            #region V2

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "arose", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "awoke", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "was/were", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bore", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "beat", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "became", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "began", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bent", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bet", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bound", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bit", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bled", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "blew", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "broke", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bred", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "brought", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "built", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bought", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "caught", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "chose", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "clung", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "came", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "cost", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "cut", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "dealt", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "dug", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "did", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "drew", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "drank", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "drove", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "ate", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fell", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fed", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "felt", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fought", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "found", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fled", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "flew", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "forbade", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "forgot", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "forgave", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "froze", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "got", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "gave", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "went", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "grew", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hung", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "had", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "heard", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hid", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hit", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "held", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hurt", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "kept", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "knew", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "laid", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "led", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "learnt", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "left", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lent", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "let", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lay", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lit", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lost", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "made", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "meant", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "met", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "paid", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "put", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "read", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "rode", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "rang", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "rose", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "ran", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "said", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "saw", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sought", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sold", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sent", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "set", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shook", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shone", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shot", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "showed", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shrank", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shut", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sang", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sat", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "slept", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "slid", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "smelt", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spoke", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spelt", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spent", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spilt", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spun", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "split", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spoilt", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spread", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stood", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stole", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stung", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stank", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "struck", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swore", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swept", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swelled", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swam", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "took", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "taught", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "tore", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "told", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "thought", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "threw", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "understood", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "woke", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "wore", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "won", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "wound", (int)UnitType.IrregularV2, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "wrote", (int)UnitType.IrregularV2, DateTime.UtcNow });

            #endregion

            #region V3

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "arisen", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "awoken", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "been", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "born", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "broken", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "beaten", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "become", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "begun", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bent", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bet", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bound", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bitten", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bled", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "blown", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "broken	", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bred", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "brought", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "built", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "bought", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "caught", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "chosen", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "clung", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "come", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "cost", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "cut", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "dealt", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "dug", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "done", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "drawn", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "drunk", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "driven", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "eaten", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fallen", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fed", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "felt", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fought", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "found", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "fled", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "flown", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "forbidden", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "forgotten", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "forgiven", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "frozen", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "got/gotten", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "given", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "gone", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "grown", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hung", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "had", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "heard", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hidden", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hit", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "held", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "hurt", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "kept", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "known", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "laid", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "led", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "learnt", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "left", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lent", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "let", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lain", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lit", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "lost", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "made", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "meant", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "met", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "paid", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "put", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "read", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "ridden", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "rung", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "risen", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "run", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "said", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "seen", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sought", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sold", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sent", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "set", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shaken", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shone", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shot", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shown", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shrunk", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "shut", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sung", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sat", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "slept", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "slid", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "smelt", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spoken", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spelt", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spent", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spilt", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spun", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "split", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spoilt", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "spread", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stood", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stolen", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stung", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "stunk", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "struck", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "sworn", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swept", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swollen", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "swum", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "taken", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "taught", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "torn", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "told", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "thought", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "thrown", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "understood", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "woken", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "worn", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "won", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "wound", (int)UnitType.IrregularV3, DateTime.UtcNow });

            migrationBuilder.InsertData(
            table: "EnglishUnits",
            columns: new[] { "Id", "Value", "Type", "CreationDateUtc" },
            values: new object[] { id++, "written", (int)UnitType.IrregularV3, DateTime.UtcNow });

            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
