using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBook.Repository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "c7f68944-b2f0-40ca-8607-3172585cc2cb", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", "234e787b-5857-48e1-b74b-06dd471c4d8f", "Standard_User", "STANDARD_USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a6d6c280-e20d-4786-8d34-7cf2980a7cde", 0, null, "58644399-a186-4d2f-871d-555cd3edb773", "admin@test.com", true, null, null, false, null, "ADMIN@TEST.COM", "ADMIN@TEST.COM", "AQAAAAEAACcQAAAAENE1/U4TKLu+N67oXTnYre+UEvXWdWapnLoXwCN7yOCaiAmLg0PHIGKXAxiNd1js+A==", null, true, "8552e450-29ad-4af6-909d-9d1e7bea6a5a", false, "admin@test.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "a6d6c280-e20d-4786-8d34-7cf2980a7cde" });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "OwnerId" },
                values: new object[] { new Guid("3b0bcd8f-b03b-4b50-b76e-73df4de91e7d"), "a6d6c280-e20d-4786-8d34-7cf2980a7cde" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "a6d6c280-e20d-4786-8d34-7cf2980a7cde" });

            migrationBuilder.DeleteData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: new Guid("3b0bcd8f-b03b-4b50-b76e-73df4de91e7d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6d6c280-e20d-4786-8d34-7cf2980a7cde");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Books");
        }
    }
}
