using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftLogger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableSimplification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Users_UserId",
                table: "Shifts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "IsClockedIn",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "IsClockedOut",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "NeedsReviewed",
                table: "Shifts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Shifts",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Shifts_UserId",
                table: "Shifts",
                newName: "IX_Shifts_EmployeeId");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Employee_EmployeeId",
                table: "Shifts",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Employee_EmployeeId",
                table: "Shifts");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Shifts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Shifts_EmployeeId",
                table: "Shifts",
                newName: "IX_Shifts_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsClockedIn",
                table: "Shifts",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClockedOut",
                table: "Shifts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NeedsReviewed",
                table: "Shifts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Users_UserId",
                table: "Shifts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
