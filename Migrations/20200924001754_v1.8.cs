using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProductCatalog.Migrations
{
    public partial class v18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "varchar(120)",
                table: "Brand",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brand",
                type: "varchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 120);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Brand",
                newName: "varchar(120)");

            migrationBuilder.AlterColumn<string>(
                name: "varchar(120)",
                table: "Brand",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(120)",
                oldMaxLength: 120);
        }
    }
}
