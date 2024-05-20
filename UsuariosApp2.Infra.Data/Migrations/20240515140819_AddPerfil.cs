﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosApp2.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PerfilId",
                table: "USUARIO",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PERFIL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERFIL", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_PerfilId",
                table: "USUARIO",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_PERFIL_NOME",
                table: "PERFIL",
                column: "NOME",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_PERFIL_PerfilId",
                table: "USUARIO",
                column: "PerfilId",
                principalTable: "PERFIL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_PERFIL_PerfilId",
                table: "USUARIO");

            migrationBuilder.DropTable(
                name: "PERFIL");

            migrationBuilder.DropIndex(
                name: "IX_USUARIO_PerfilId",
                table: "USUARIO");

            migrationBuilder.DropColumn(
                name: "PerfilId",
                table: "USUARIO");
        }
    }
}
