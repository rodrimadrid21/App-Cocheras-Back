using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstacionamientoAustralApi.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioEgreso",
                table: "Estacionamientos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estacionamientos_IdCochera",
                table: "Estacionamientos",
                column: "IdCochera");

            migrationBuilder.AddForeignKey(
                name: "FK_Estacionamientos_Cocheras_IdCochera",
                table: "Estacionamientos",
                column: "IdCochera",
                principalTable: "Cocheras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estacionamientos_Cocheras_IdCochera",
                table: "Estacionamientos");

            migrationBuilder.DropIndex(
                name: "IX_Estacionamientos_IdCochera",
                table: "Estacionamientos");

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioEgreso",
                table: "Estacionamientos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
