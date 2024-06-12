using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingBook.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class fixusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "142fc063-5e35-4bd0-829c-dbbfcc225474",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a063e2a-d8d9-43a3-bef4-a1163cb8a749", "AQAAAAIAAYagAAAAEBE4nl0pC9Vgih0u/WCYjAqE6nnKgGBSLxQnGnHwwn/BJjraUfoTOdWgdY7Eh0X3QA==", "2f5a6504-4fc2-4028-ba4a-e5210f93300a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "142fc063-5e35-4bd0-829c-dbbfcc225474",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7b9eb61-3020-4ed0-a8c1-7dcd88394f6e", "AQAAAAIAAYagAAAAECRzIC/PL3botxjWanUbYxCx6ZTiHE8yKSB06h29Ja2VcsqHXHlfnTIrpoIzLSvaxQ==", "52d4914b-703d-4fb2-85b0-767761585028" });
        }
    }
}
