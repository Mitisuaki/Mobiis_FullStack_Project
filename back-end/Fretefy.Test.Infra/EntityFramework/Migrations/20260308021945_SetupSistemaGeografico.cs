using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fretefy.Test.Infra.EntityFramework.Migrations
{
    public partial class SetupSistemaGeografico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESTADO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SIGLA = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "REGIAO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ATIVO = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGIAO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CIDADE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    NOME = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ESTADO_ID = table.Column<Guid>(type: "TEXT", maxLength: 2, nullable: false),
                    EstadoId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIDADE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CIDADE_ESTADO_ESTADO_ID",
                        column: x => x.ESTADO_ID,
                        principalTable: "ESTADO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CIDADE_ESTADO_EstadoId1",
                        column: x => x.EstadoId1,
                        principalTable: "ESTADO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REL_REGIAO_CIDADE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    REGIAO_ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CIDADE_ID = table.Column<Guid>(type: "TEXT", nullable: true),
                    ESTADO_ID = table.Column<Guid>(type: "TEXT", nullable: true),
                    RegiaoId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REL_REGIAO_CIDADE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REL_REGIAO_CIDADE_CIDADE_CIDADE_ID",
                        column: x => x.CIDADE_ID,
                        principalTable: "CIDADE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REL_REGIAO_CIDADE_ESTADO_ESTADO_ID",
                        column: x => x.ESTADO_ID,
                        principalTable: "ESTADO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REL_REGIAO_CIDADE_REGIAO_REGIAO_ID",
                        column: x => x.REGIAO_ID,
                        principalTable: "REGIAO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REL_REGIAO_CIDADE_REGIAO_RegiaoId1",
                        column: x => x.RegiaoId1,
                        principalTable: "REGIAO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CIDADE_ESTADO_ID",
                table: "CIDADE",
                column: "ESTADO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CIDADE_EstadoId1",
                table: "CIDADE",
                column: "EstadoId1");

            migrationBuilder.CreateIndex(
                name: "IX_CIDADE_NOME_ESTADO_ID",
                table: "CIDADE",
                columns: new[] { "NOME", "ESTADO_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ESTADO_NOME",
                table: "ESTADO",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ESTADO_SIGLA",
                table: "ESTADO",
                column: "SIGLA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REGIAO_NOME",
                table: "REGIAO",
                column: "NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_REL_REGIAO_CIDADE_CIDADE_ID",
                table: "REL_REGIAO_CIDADE",
                column: "CIDADE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REL_REGIAO_CIDADE_ESTADO_ID",
                table: "REL_REGIAO_CIDADE",
                column: "ESTADO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REL_REGIAO_CIDADE_REGIAO_ID_CIDADE_ID",
                table: "REL_REGIAO_CIDADE",
                columns: new[] { "REGIAO_ID", "CIDADE_ID" },
                unique: true,
                filter: "\"CIDADE_ID\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_REL_REGIAO_CIDADE_REGIAO_ID_ESTADO_ID",
                table: "REL_REGIAO_CIDADE",
                columns: new[] { "REGIAO_ID", "ESTADO_ID" },
                unique: true,
                filter: "\"ESTADO_ID\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_REL_REGIAO_CIDADE_RegiaoId1",
                table: "REL_REGIAO_CIDADE",
                column: "RegiaoId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REL_REGIAO_CIDADE");

            migrationBuilder.DropTable(
                name: "CIDADE");

            migrationBuilder.DropTable(
                name: "REGIAO");

            migrationBuilder.DropTable(
                name: "ESTADO");
        }
    }
}
