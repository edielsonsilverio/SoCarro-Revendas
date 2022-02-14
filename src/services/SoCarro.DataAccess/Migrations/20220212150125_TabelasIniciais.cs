using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoCarro.DataAccess.Migrations
{
    public partial class TabelasIniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proprietario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Documento = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TipoProprietario = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Cep = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ProprietarioId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Proprietario_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Renavam = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Quilometragem = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MarcaId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    ProprietarioId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Veiculo_Proprietario_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Modelo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    AnoModelo = table.Column<int>(type: "int", nullable: false),
                    VeiculoId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelo_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "Id");
                });


            migrationBuilder.InsertData(
               table: "Marca",
               columns: new[] { "Id", "DataCadastro", "Nome", "Status" },
               values: new object[,]
               {
                    { new Guid("1962c218-a0dc-4746-b406-0bd79aa6e5a8"), new DateTime(2022, 2, 12, 11, 1, 25, 597, DateTimeKind.Local).AddTicks(2529), "Ford", 0 },
                    { new Guid("879d7c81-9841-4469-b71d-ecd09ae07fdf"), new DateTime(2022, 2, 12, 11, 1, 25, 597, DateTimeKind.Local).AddTicks(2531), "Honda", 0 },
                    { new Guid("b358b8dd-be36-4afe-8c6b-4ce7438a434a"), new DateTime(2022, 2, 12, 11, 1, 25, 597, DateTimeKind.Local).AddTicks(2509), "Volkswagen", 0 },
                    { new Guid("c30375f4-d336-4a5f-af87-f8bd87eabc6e"), new DateTime(2022, 2, 12, 11, 1, 25, 597, DateTimeKind.Local).AddTicks(2532), "Hyundai", 0 },
                    { new Guid("ef2ae5a0-9b10-4372-a214-79ded3e0f5a3"), new DateTime(2022, 2, 12, 11, 1, 25, 597, DateTimeKind.Local).AddTicks(2528), "Toyota", 0 }
               });


            migrationBuilder.InsertData(
                table: "Proprietario",
                columns: new[] { "Id", "DataCadastro", "Documento", "Email", "Nome", "Status", "TipoProprietario" },
                values: new object[,]
                {
                    { new Guid("47c477a8-3063-4df1-be95-1d8919850f40"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "80292613067", "jose@socarro.com.br", "Zé do Carro", 0, 1 },
                    { new Guid("5f535587-ea67-4395-aca1-47261934ac84"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "66689736040", "tonho@socarro.com.br", "Tonho do Carro", 0, 1 },
                    { new Guid("8ccca536-c35e-4eb2-9bdb-7b23f764ad92"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "65695214033", "maria.jose@socarro.com.br", "Maria José", 0, 1 },
                    { new Guid("f499f8c0-15d8-4a79-b66a-35728712dea8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "66689736040", "jose.maria@socarro.com.br", "José Maria", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Endereco",
                columns: new[] { "Id", "Bairro", "Cep", "Cidade", "Complemento", "Estado", "Logradouro", "Numero", "ProprietarioId" },
                values: new object[,]
                {
                    { new Guid("5c824376-c134-43cf-ac42-5ba1e3a1d9e1"), "Centro", "78098000", "Cuiabá", "Quadra 15", "MT", "Rua 10", "01", new Guid("47c477a8-3063-4df1-be95-1d8919850f40") },
                    { new Guid("6d335490-c849-4792-919e-adb38dd964f2"), "Jardim Imperial", "78098000", "Cuiabá", "Quadra 70", "MT", "Rua 42", "395", new Guid("5f535587-ea67-4395-aca1-47261934ac84") },
                    { new Guid("9c3fee74-05ae-42ec-b841-88a31c243401"), "Porto", "78098000", "Cuiabá", "Quadra 36", "MT", "Rua Brazil", "10", new Guid("8ccca536-c35e-4eb2-9bdb-7b23f764ad92") },
                    { new Guid("b397ea69-31cf-48fb-9425-8222a95e1cc4"), "Jardim Universitário", "78098000", "Cuiabá", "Quadra 98", "MT", "Rua Orquideas", "01", new Guid("f499f8c0-15d8-4a79-b66a-35728712dea8") }
                });

           

            migrationBuilder.InsertData(
                table: "Veiculo",
                columns: new[] { "Id", "DataCadastro", "MarcaId", "Nome", "ProprietarioId", "Quilometragem", "Renavam", "Status", "Valor" },
                values: new object[,]
                {
                    { new Guid("1c667238-fe0e-4c7e-9519-94438d04ce8d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b358b8dd-be36-4afe-8c6b-4ce7438a434a"), "Voyage", new Guid("47c477a8-3063-4df1-be95-1d8919850f40"), 50000, "74493153016", 0, 32000m },
                    { new Guid("228f3149-61d1-4e7e-96bf-29d27da650df"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("879d7c81-9841-4469-b71d-ecd09ae07fdf"), "Sandero", new Guid("879d7c81-9841-4469-b71d-ecd09ae07fdf"), 65000, "75106570228", 0, 55000m },
                    { new Guid("48ecbd3c-660b-42fb-90f4-38cc33e596ea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b358b8dd-be36-4afe-8c6b-4ce7438a434a"), "Gol", new Guid("b358b8dd-be36-4afe-8c6b-4ce7438a434a"), 15000, "76473056607", 0, 45000m },
                    { new Guid("50e2c51f-9684-42b2-8bb1-972ee8d22038"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ef2ae5a0-9b10-4372-a214-79ded3e0f5a3"), "Corolla", new Guid("c30375f4-d336-4a5f-af87-f8bd87eabc6e"), 0, "91838212968", 0, 105000m },
                    { new Guid("8599fc54-30b3-4b7d-846b-00da02d5e5e1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1962c218-a0dc-4746-b406-0bd79aa6e5a8"), "Palio", new Guid("ef2ae5a0-9b10-4372-a214-79ded3e0f5a3"), 118000, "83663783642", 0, 25000m }
                });

            migrationBuilder.InsertData(
                table: "Modelo",
                columns: new[] { "Id", "AnoFabricacao", "AnoModelo", "Descricao", "VeiculoId" },
                values: new object[,]
                {
                    { new Guid("489b69d4-5025-4526-a42c-68de204a1b08"), 2020, 2021, "Trend", new Guid("1c667238-fe0e-4c7e-9519-94438d04ce8d") },
                    { new Guid("5f3c18db-d77f-424c-ba90-58e2de3ab2d9"), 2022, 2022, "Esportivo", new Guid("228f3149-61d1-4e7e-96bf-29d27da650df") },
                    { new Guid("8d3bc0ee-f683-4f99-af82-c51f7c5d7aef"), 2021, 2022, "Verão", new Guid("48ecbd3c-660b-42fb-90f4-38cc33e596ea") },
                    { new Guid("91c37cdb-fb51-4766-a23e-d204af8a9516"), 2021, 2021, "Confort line", new Guid("50e2c51f-9684-42b2-8bb1-972ee8d22038") },
                    { new Guid("e77d018a-ab5e-404a-930a-1e9ee4f3792f"), 2021, 2022, "Inferno", new Guid("8599fc54-30b3-4b7d-846b-00da02d5e5e1") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_ProprietarioId",
                table: "Endereco",
                column: "ProprietarioId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_VeiculoId",
                table: "Modelo",
                column: "VeiculoId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_MarcaId",
                table: "Veiculo",
                column: "MarcaId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_ProprietarioId",
                table: "Veiculo",
                column: "ProprietarioId",
                unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Modelo");

            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Proprietario");
        }
    }
}
