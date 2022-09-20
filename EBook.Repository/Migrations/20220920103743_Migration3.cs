using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBook.Repository.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "28a67c6b-13e2-4580-be59-5aa63647d290" });

            migrationBuilder.DeleteData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: new Guid("b5e54394-296b-40d0-b31a-676f0fd0ea16"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "28a67c6b-13e2-4580-be59-5aa63647d290");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "c1bec236-57f5-48a5-ba0a-dbe698bb5fc9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6e776223-29cc-486b-a46c-e1ebb2ed7d0b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a7c0c624-1fab-4c78-b511-3ebc048a1aa9", 0, null, "b25c43a3-b5c4-4d6b-92b9-643dff21fe59", "admin@test.com", true, null, null, false, null, "ADMIN@TEST.COM", "ADMIN@TEST.COM", "AQAAAAEAACcQAAAAEBNbZ0T2qnmTAZYjv4RuVKEsA7910dQZirbMrYBLmKqVX2boBhm07PehmAMwXoMeLA==", null, true, "48cbd443-c89d-4a3c-ae21-9edc13d85cb8", false, "admin@test.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "a7c0c624-1fab-4c78-b511-3ebc048a1aa9" });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "OwnerId" },
                values: new object[] { new Guid("af8a085d-e7bb-4d9f-afb6-de53fab9ca39"), "a7c0c624-1fab-4c78-b511-3ebc048a1aa9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "a7c0c624-1fab-4c78-b511-3ebc048a1aa9" });

            migrationBuilder.DeleteData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: new Guid("af8a085d-e7bb-4d9f-afb6-de53fab9ca39"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a7c0c624-1fab-4c78-b511-3ebc048a1aa9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "b1d2f86c-866b-44bf-90f0-fa9efa1df1b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c87fe960-7f5f-49e4-b1c4-151977138417");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "28a67c6b-13e2-4580-be59-5aa63647d290", 0, null, "b5686801-3e4f-480f-bba4-5c86c32b9c03", "admin@test.com", true, null, null, false, null, "ADMIN@TEST.COM", "ADMIN@TEST.COM", "AQAAAAEAACcQAAAAEGOpyxvg3F5rFa2DUiQdCre13usxpRvpW3Ov0TzPRmI8HEKKyO8dgYhpPAs3Y0Z/mg==", null, true, "62b9615d-4182-41ba-9b19-ce5c2dbbf0d6", false, "admin@test.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "28a67c6b-13e2-4580-be59-5aa63647d290" });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "OwnerId" },
                values: new object[] { new Guid("b5e54394-296b-40d0-b31a-676f0fd0ea16"), "28a67c6b-13e2-4580-be59-5aa63647d290" });
        }
    }
}
