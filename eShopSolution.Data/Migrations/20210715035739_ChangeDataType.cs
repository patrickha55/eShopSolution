using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class ChangeDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("514b2dc0-af2b-4af9-8d96-16c498726f26"),
                column: "ConcurrencyStamp",
                value: "942addcd-3137-46f4-ba27-b0f2c1ec621d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("09f5bc24-85d1-4fef-8898-2004bcbd1844"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fe92b0d2-bc04-4f42-bbb6-179debfdec91", "AQAAAAEAACcQAAAAEDPZjPl43mLbJVZOJzvydGI4/i7MAtYVaja+bCzmb4dL2RYvuF5d6UMgmsQwzOD4pA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 15, 10, 57, 39, 299, DateTimeKind.Local).AddTicks(5683));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("514b2dc0-af2b-4af9-8d96-16c498726f26"),
                column: "ConcurrencyStamp",
                value: "1625a42b-fe46-4351-8e14-6b0dd8c3af35");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("09f5bc24-85d1-4fef-8898-2004bcbd1844"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5e109cee-d53b-44f2-a6d8-13c3795365e8", "AQAAAAEAACcQAAAAEN6U1KHn2uo4bor1vG+cNPop7l0UqpIHSJMNBVpR6EWzayc/5el3kHg5upp/iQ2gvg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 14, 17, 44, 58, 599, DateTimeKind.Local).AddTicks(8296));
        }
    }
}
