using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sirep.Data.Migrations
{
    public partial class DesdeCero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Cuencas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(maxLength: 50, nullable: true),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuencas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Especies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Familia = table.Column<string>(maxLength: 50, nullable: false),
                    Orden = table.Column<string>(maxLength: 50, nullable: false),
                    Codigo = table.Column<string>(maxLength: 50, nullable: true),
                    NombreComun = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepresentantesLegales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    NID = table.Column<string>(maxLength: 30, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentantesLegales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 30, nullable: false),
                    Apellido = table.Column<string>(maxLength: 30, nullable: false),
                    NID = table.Column<string>(maxLength: 30, nullable: true),
                    IdUser = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Centros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    NID = table.Column<string>(maxLength: 30, nullable: true),
                    Telefono = table.Column<string>(maxLength: 30, nullable: true),
                    Direccion = table.Column<string>(maxLength: 50, nullable: false),
                    Lugar = table.Column<string>(maxLength: 50, nullable: true),
                    DepartamentoId = table.Column<int>(nullable: false),
                    RepresentanteLegalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Centros_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Centros_RepresentantesLegales_RepresentanteLegalId",
                        column: x => x.RepresentanteLegalId,
                        principalTable: "RepresentantesLegales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentroUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentroId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentroUsuarios_Centros_CentroId",
                        column: x => x.CentroId,
                        principalTable: "Centros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentroUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 30, nullable: false),
                    Detalles = table.Column<string>(maxLength: 200, nullable: true),
                    CentroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotes_Centros_CentroId",
                        column: x => x.CentroId,
                        principalTable: "Centros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermisoCentros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentroId = table.Column<int>(nullable: false),
                    InstitucionId = table.Column<int>(nullable: false),
                    PermisoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisoCentros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermisoCentros_Centros_CentroId",
                        column: x => x.CentroId,
                        principalTable: "Centros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermisoCentros_Instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermisoCentros_Permisos_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reproductores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChipId = table.Column<string>(maxLength: 20, nullable: false),
                    FechaIngreso = table.Column<DateTime>(nullable: false),
                    LugarProcedencia = table.Column<string>(maxLength: 100, nullable: false),
                    EspecieId = table.Column<int>(nullable: false),
                    LoteId = table.Column<int>(nullable: false),
                    CuencaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reproductores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reproductores_Cuencas_CuencaId",
                        column: x => x.CuencaId,
                        principalTable: "Cuencas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reproductores_Especies_EspecieId",
                        column: x => x.EspecieId,
                        principalTable: "Especies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reproductores_Lotes_LoteId",
                        column: x => x.LoteId,
                        principalTable: "Lotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatosReproductores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReproductorId = table.Column<int>(nullable: false),
                    Peso = table.Column<double>(nullable: false),
                    Talla = table.Column<double>(nullable: false),
                    Sexo = table.Column<bool>(nullable: false),
                    HTO = table.Column<double>(nullable: false),
                    HTOLab = table.Column<double>(nullable: false),
                    HB = table.Column<double>(nullable: false),
                    PPT = table.Column<double>(nullable: false),
                    CSL = table.Column<double>(nullable: false),
                    PS = table.Column<double>(nullable: false),
                    UREA = table.Column<double>(nullable: false),
                    GLS = table.Column<double>(nullable: false),
                    RBC = table.Column<double>(nullable: true),
                    WBC = table.Column<double>(nullable: true),
                    Observaciones = table.Column<string>(maxLength: 100, nullable: true),
                    Ojos = table.Column<string>(maxLength: 100, nullable: true),
                    Piel = table.Column<string>(maxLength: 100, nullable: true),
                    Aleta = table.Column<string>(maxLength: 100, nullable: true),
                    Branquias = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosReproductores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatosReproductores_Reproductores_ReproductorId",
                        column: x => x.ReproductorId,
                        principalTable: "Reproductores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagenReproductores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReproductorId = table.Column<int>(nullable: false),
                    Imagen = table.Column<byte[]>(nullable: false),
                    Numero = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenReproductores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagenReproductores_Reproductores_ReproductorId",
                        column: x => x.ReproductorId,
                        principalTable: "Reproductores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Centros_DepartamentoId",
                table: "Centros",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Centros_RepresentanteLegalId",
                table: "Centros",
                column: "RepresentanteLegalId");

            migrationBuilder.CreateIndex(
                name: "IX_CentroUsuarios_CentroId",
                table: "CentroUsuarios",
                column: "CentroId");

            migrationBuilder.CreateIndex(
                name: "IX_CentroUsuarios_UsuarioId",
                table: "CentroUsuarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DatosReproductores_ReproductorId",
                table: "DatosReproductores",
                column: "ReproductorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagenReproductores_ReproductorId",
                table: "ImagenReproductores",
                column: "ReproductorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_CentroId",
                table: "Lotes",
                column: "CentroId");

            migrationBuilder.CreateIndex(
                name: "IX_PermisoCentros_CentroId",
                table: "PermisoCentros",
                column: "CentroId");

            migrationBuilder.CreateIndex(
                name: "IX_PermisoCentros_InstitucionId",
                table: "PermisoCentros",
                column: "InstitucionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermisoCentros_PermisoId",
                table: "PermisoCentros",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reproductores_CuencaId",
                table: "Reproductores",
                column: "CuencaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reproductores_EspecieId",
                table: "Reproductores",
                column: "EspecieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reproductores_LoteId",
                table: "Reproductores",
                column: "LoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentroUsuarios");

            migrationBuilder.DropTable(
                name: "DatosReproductores");

            migrationBuilder.DropTable(
                name: "ImagenReproductores");

            migrationBuilder.DropTable(
                name: "PermisoCentros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Reproductores");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Cuencas");

            migrationBuilder.DropTable(
                name: "Especies");

            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "Centros");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "RepresentantesLegales");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
