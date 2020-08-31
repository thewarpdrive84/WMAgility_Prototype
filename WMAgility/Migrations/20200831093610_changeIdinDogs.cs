using Microsoft.EntityFrameworkCore.Migrations;

namespace WMAgility.Migrations
{
    public partial class changeIdinDogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Dogs_CanineId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Dogs_CanineId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Dogs_CanineId",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dogs",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "DogId",
                table: "Dogs");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Dogs",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dogs",
                table: "Dogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Dogs_CanineId",
                table: "Competitions",
                column: "CanineId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Dogs_CanineId",
                table: "Practices",
                column: "CanineId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Dogs_CanineId",
                table: "Skills",
                column: "CanineId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Dogs_CanineId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Dogs_CanineId",
                table: "Practices");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Dogs_CanineId",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dogs",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Dogs");

            migrationBuilder.AddColumn<int>(
                name: "DogId",
                table: "Dogs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dogs",
                table: "Dogs",
                column: "DogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Dogs_CanineId",
                table: "Competitions",
                column: "CanineId",
                principalTable: "Dogs",
                principalColumn: "DogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Dogs_CanineId",
                table: "Practices",
                column: "CanineId",
                principalTable: "Dogs",
                principalColumn: "DogId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Dogs_CanineId",
                table: "Skills",
                column: "CanineId",
                principalTable: "Dogs",
                principalColumn: "DogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
