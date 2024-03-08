using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicazioneAlbergo_core_Entity.Migrations
{
    /// <inheritdoc />
    public partial class Tabelle_e_relazioni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Camere",
                columns: table => new
                {
                    IdCamera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCamera = table.Column<int>(type: "int", nullable: false),
                    TipoCamera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camere", x => x.IdCamera);
                });

            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cellulare = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Dipendenti",
                columns: table => new
                {
                    IdDipendente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUtente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dipendenti", x => x.IdDipendente);
                });

            migrationBuilder.CreateTable(
                name: "Pensioni",
                columns: table => new
                {
                    IdPensione = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPensione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pensioni", x => x.IdPensione);
                });

            migrationBuilder.CreateTable(
                name: "Servizi",
                columns: table => new
                {
                    IdServizio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescrizioneServizio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoServizio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servizi", x => x.IdServizio);
                });

            migrationBuilder.CreateTable(
                name: "Prenotazioni",
                columns: table => new
                {
                    IdPrenotazione = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInizioPrenotazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFinePrenotazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acconto = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdCamera = table.Column<int>(type: "int", nullable: false),
                    IdPensione = table.Column<int>(type: "int", nullable: false),
                    IdServizio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prenotazioni", x => x.IdPrenotazione);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Camere_IdCamera",
                        column: x => x.IdCamera,
                        principalTable: "Camere",
                        principalColumn: "IdCamera",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Clienti_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clienti",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Pensioni_IdPensione",
                        column: x => x.IdPensione,
                        principalTable: "Pensioni",
                        principalColumn: "IdPensione",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Servizi_IdServizio",
                        column: x => x.IdServizio,
                        principalTable: "Servizi",
                        principalColumn: "IdServizio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdCamera",
                table: "Prenotazioni",
                column: "IdCamera");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdCliente",
                table: "Prenotazioni",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdPensione",
                table: "Prenotazioni",
                column: "IdPensione");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_IdServizio",
                table: "Prenotazioni",
                column: "IdServizio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dipendenti");

            migrationBuilder.DropTable(
                name: "Prenotazioni");

            migrationBuilder.DropTable(
                name: "Camere");

            migrationBuilder.DropTable(
                name: "Clienti");

            migrationBuilder.DropTable(
                name: "Pensioni");

            migrationBuilder.DropTable(
                name: "Servizi");
        }
    }
}
