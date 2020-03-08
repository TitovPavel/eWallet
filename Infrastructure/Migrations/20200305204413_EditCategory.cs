using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class EditCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_ExpenseCategory_CategoryId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_IncomeCategory_CategoryId",
                table: "Income");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Income",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Expense",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_ExpenseCategory_CategoryId",
                table: "Expense",
                column: "CategoryId",
                principalTable: "ExpenseCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_IncomeCategory_CategoryId",
                table: "Income",
                column: "CategoryId",
                principalTable: "IncomeCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_ExpenseCategory_CategoryId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_IncomeCategory_CategoryId",
                table: "Income");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Income",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Expense",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_ExpenseCategory_CategoryId",
                table: "Expense",
                column: "CategoryId",
                principalTable: "ExpenseCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_IncomeCategory_CategoryId",
                table: "Income",
                column: "CategoryId",
                principalTable: "IncomeCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
