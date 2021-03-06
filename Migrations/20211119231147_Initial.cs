using Microsoft.EntityFrameworkCore.Migrations;

namespace TP_Plataformas_de_Desarrollo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.idCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dni = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    apellido = table.Column<string>(type: "varchar(50)", nullable: false),
                    mail = table.Column<string>(type: "varchar(50)", nullable: false),
                    password = table.Column<string>(type: "varchar(50)", nullable: false),
                    cuit_cuil = table.Column<long>(type: "bigint", nullable: false),
                    rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.idProducto);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_idCategoria",
                        column: x => x.idCategoria,
                        principalTable: "Categoria",
                        principalColumn: "idCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carro",
                columns: table => new
                {
                    idCarro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carro", x => x.idCarro);
                    table.ForeignKey(
                        name: "FK_Carro_Usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    idCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.idCompra);
                    table.ForeignKey(
                        name: "FK_Compra_Usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarroProducto",
                columns: table => new
                {
                    idCarroProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCarro = table.Column<int>(type: "int", nullable: false),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarroProducto", x => x.idCarroProducto);
                    table.ForeignKey(
                        name: "FK_CarroProducto_Carro_idCarro",
                        column: x => x.idCarro,
                        principalTable: "Carro",
                        principalColumn: "idCarro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarroProducto_Producto_idProducto",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraProducto",
                columns: table => new
                {
                    idCompraProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCompra = table.Column<int>(type: "int", nullable: false),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraProducto", x => x.idCompraProducto);
                    table.ForeignKey(
                        name: "FK_CompraProducto_Compra_idCompra",
                        column: x => x.idCompra,
                        principalTable: "Compra",
                        principalColumn: "idCompra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraProducto_Producto_idProducto",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "idCategoria", "nombre" },
                values: new object[,]
                {
                    { 1, "Comida" },
                    { 2, "Bebida" },
                    { 3, "Ropa" },
                    { 4, "Articulos de limpieza" },
                    { 5, "Electrodomesticos" },
                    { 6, "Informatica" },
                    { 7, "Herramientas" },
                    { 8, "Electronica" },
                    { 9, "Mascotas" },
                    { 10, "Libreria" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "idUsuario", "apellido", "cuit_cuil", "dni", "mail", "nombre", "password", "rol" },
                values: new object[,]
                {
                    { 1, "Admin", 34865218L, 123456, "admin@gmail.com", "Admin", "123456", 1 },
                    { 2, "Lopez", 25689475L, 654321, "pepitolopez@gmail.com", "Pepito", "654321", 2 },
                    { 3, "Perez", 20321548L, 32154869, "joseperez@hotmail.com", "José", "123456", 3 }
                });

            migrationBuilder.InsertData(
                table: "Carro",
                columns: new[] { "idCarro", "idUsuario" },
                values: new object[,]
                {
                    { 3, 3 },
                    { 2, 2 },
                    { 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Compra",
                columns: new[] { "idCompra", "idUsuario", "total" },
                values: new object[] { 1, 2, 0m });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "idProducto", "cantidad", "idCategoria", "nombre", "precio" },
                values: new object[,]
                {
                    { 49, 200, 10, "Resma", 700m },
                    { 15, 20, 6, "Monitor", 28000m },
                    { 16, 20, 6, "Notebook", 95000m },
                    { 32, 150, 7, "Taladro", 15000m },
                    { 33, 150, 7, "Amoladora", 7000m },
                    { 34, 150, 7, "Soldadora", 20000m },
                    { 35, 150, 7, "Sierra", 8000m },
                    { 7, 150, 8, "TV", 80000m },
                    { 36, 150, 8, "Hidrolavadora", 7000m },
                    { 37, 150, 8, "Parlantes", 10000m },
                    { 50, 200, 10, "Tablero dibujo", 3000m },
                    { 38, 150, 8, "Auriculares", 4500m },
                    { 14, 20, 6, "Teclado", 3500m },
                    { 41, 200, 9, "Alimento para Perros", 282m },
                    { 42, 200, 9, "Alimento para Gatos", 144m },
                    { 43, 200, 9, "Cuchas", 3000m },
                    { 44, 200, 9, "Correas", 1200m },
                    { 45, 200, 9, "Juguetes", 600m },
                    { 46, 200, 10, "Cuaderno", 250m },
                    { 47, 200, 10, "Marcadores", 1200m },
                    { 48, 200, 10, "Calculadora", 1500m },
                    { 39, 150, 8, "Celular", 50000m },
                    { 40, 150, 8, "Proyector", 45000m },
                    { 13, 20, 6, "Mouse", 1500m },
                    { 31, 20, 5, "Licuadora", 6000m },
                    { 5, 100, 1, "Palitos", 126m },
                    { 6, 100, 1, "Chizitos", 138m },
                    { 19, 100, 1, "Mani", 121m },
                    { 20, 100, 1, "Nachos", 241m },
                    { 1, 100, 2, "Gaseosa", 125m },
                    { 2, 50, 2, "Cerveza", 120m },
                    { 3, 100, 2, "Agua", 78m },
                    { 17, 100, 2, "Leche", 95m },
                    { 18, 100, 2, "Energizante", 108m },
                    { 9, 50, 3, "Pantalon Deportivo", 6500m },
                    { 10, 50, 3, "Camiseta Deportiva", 6500m },
                    { 21, 50, 3, "Campera", 6000m },
                    { 22, 50, 3, "Sweater", 3000m }
                });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "idProducto", "cantidad", "idCategoria", "nombre", "precio" },
                values: new object[,]
                {
                    { 23, 50, 3, "Jean", 2000m },
                    { 11, 50, 4, "Lavandina", 49m },
                    { 12, 50, 4, "Escoba", 340m },
                    { 24, 50, 4, "Detergente", 87m },
                    { 25, 50, 4, "Pala", 300m },
                    { 26, 50, 4, "Secador", 800m },
                    { 27, 20, 5, "Heladera", 83000m },
                    { 28, 20, 5, "Lavarropa", 78000m },
                    { 29, 20, 5, "Cocina", 50000m },
                    { 30, 20, 5, "Microondas", 25000m },
                    { 8, 30, 6, "PC", 60000m },
                    { 4, 100, 1, "Papas", 250m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carro_idUsuario",
                table: "Carro",
                column: "idUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarroProducto_idCarro",
                table: "CarroProducto",
                column: "idCarro");

            migrationBuilder.CreateIndex(
                name: "IX_CarroProducto_idProducto",
                table: "CarroProducto",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_idUsuario",
                table: "Compra",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_CompraProducto_idCompra",
                table: "CompraProducto",
                column: "idCompra");

            migrationBuilder.CreateIndex(
                name: "IX_CompraProducto_idProducto",
                table: "CompraProducto",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_idCategoria",
                table: "Producto",
                column: "idCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarroProducto");

            migrationBuilder.DropTable(
                name: "CompraProducto");

            migrationBuilder.DropTable(
                name: "Carro");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
