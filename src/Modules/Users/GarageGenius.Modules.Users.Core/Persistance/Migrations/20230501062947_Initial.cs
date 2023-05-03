﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Users.Core.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "users");

        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "users",
            columns: table => new
            {
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Permissions = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.RoleId);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            schema: "users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                State = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "users",
                    principalTable: "Roles",
                    principalColumn: "RoleId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            schema: "users",
            table: "Users",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_RoleId",
            schema: "users",
            table: "Users",
            column: "RoleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users",
            schema: "users");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "users");
    }
}