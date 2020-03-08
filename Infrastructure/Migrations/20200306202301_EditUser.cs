using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class EditUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "IncomeCategory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ExpenseCategory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeCategory_UserId",
                table: "IncomeCategory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategory_UserId",
                table: "ExpenseCategory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseCategory_AspNetUsers_UserId",
                table: "ExpenseCategory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeCategory_AspNetUsers_UserId",
                table: "IncomeCategory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseCategory_AspNetUsers_UserId",
                table: "ExpenseCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeCategory_AspNetUsers_UserId",
                table: "IncomeCategory");

            migrationBuilder.DropIndex(
                name: "IX_IncomeCategory_UserId",
                table: "IncomeCategory");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseCategory_UserId",
                table: "ExpenseCategory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "IncomeCategory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExpenseCategory");
        }
    }
}
