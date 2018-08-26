using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_Server.Migrations
{
    public partial class AddedBankAccountPaymentTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<long>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    AccountClientNumber = table.Column<string>(nullable: true),
                    Balance = table.Column<float>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    AccountType = table.Column<string>(nullable: true),
                    AccountStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankAccountId = table.Column<long>(nullable: false),
                    PaymentType = table.Column<string>(nullable: true),
                    Amount = table.Column<float>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    MyProperty = table.Column<int>(nullable: false),
                    PaymentStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BankAccountId = table.Column<long>(nullable: false),
                    DestBankAccountId = table.Column<long>(nullable: true),
                    Amount = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_BankAccounts_DestBankAccountId",
                        column: x => x.DestBankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_ClientId",
                table: "BankAccounts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BankAccountId",
                table: "Payments",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_BankAccountId",
                table: "Transfers",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_DestBankAccountId",
                table: "Transfers",
                column: "DestBankAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "BankAccounts");
        }
    }
}
