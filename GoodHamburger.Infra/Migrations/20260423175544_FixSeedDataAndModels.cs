using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodHamburger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedDataAndModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4506afff-af71-499f-a69b-fcaee39f903b"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("94d7d6eb-11d2-41d0-95fe-f18a251a6823"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ecd27c78-5b16-40f5-8be0-cb46007ea321"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f4f9560e-abc0-4efe-ac0f-555c7f2778cb"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f9942189-3a73-45f4-9b1a-2c5fd76205f0"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4506afff-af71-499f-a69b-fcaee39f903b"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc).AddTicks(2792));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("94d7d6eb-11d2-41d0-95fe-f18a251a6823"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc).AddTicks(3138));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ecd27c78-5b16-40f5-8be0-cb46007ea321"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc).AddTicks(3140));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f4f9560e-abc0-4efe-ac0f-555c7f2778cb"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc).AddTicks(3141));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f9942189-3a73-45f4-9b1a-2c5fd76205f0"),
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 18, 54, 29, 760, DateTimeKind.Utc).AddTicks(3142));
        }
    }
}
