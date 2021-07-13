using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class AddNewUserAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("514b2dc0-af2b-4af9-8d96-16c498726f26"), "715ee017-18cf-44f2-9bb1-c522ca75718f", "Administrator Role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("514b2dc0-af2b-4af9-8d96-16c498726f26"), new Guid("09f5bc24-85d1-4fef-8898-2004bcbd1844") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DoB", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("09f5bc24-85d1-4fef-8898-2004bcbd1844"), 0, "288a09a4-a143-4c45-811b-0758f336e8f3", new DateTime(1993, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "phat@admin.com", true, "Patrick", "Ha", false, null, "phat@admin.com", "admin", "AQAAAAEAACcQAAAAEF0mRx5Z9liGc0Efqkz+y3Pnh5pZAbPXuy/pZKQcDCtCRCha9JE9MzqBZpBvplgPUQ==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 13, 11, 30, 20, 23, DateTimeKind.Local).AddTicks(1647));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("514b2dc0-af2b-4af9-8d96-16c498726f26"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("514b2dc0-af2b-4af9-8d96-16c498726f26"), new Guid("09f5bc24-85d1-4fef-8898-2004bcbd1844") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("09f5bc24-85d1-4fef-8898-2004bcbd1844"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 13, 10, 46, 31, 468, DateTimeKind.Local).AddTicks(1921));
        }
    }
}
