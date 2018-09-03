﻿// <auto-generated />
using System;
using CRUD_Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRUD_Server.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180902001436_Changes-to-UserAccount-password")]
    partial class ChangestoUserAccountpassword
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CRUD_Server.Models.BankAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountClientNumber")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<bool>("AccountStatus");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<float>("Balance");

                    b.Property<long>("ClientId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("CRUD_Server.Models.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1");

                    b.Property<string>("City");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<DateTime>("DateOfRegistration");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("SocialNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("State");

                    b.Property<string>("Zip")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CRUD_Server.Models.FavAccount", b =>
                {
                    b.Property<long>("ClientId");

                    b.Property<string>("FavBankAccount");

                    b.HasKey("ClientId", "FavBankAccount");

                    b.ToTable("FavAccounts");
                });

            modelBuilder.Entity("CRUD_Server.Models.Payment", b =>
                {
                    b.Property<long>("ClientId");

                    b.Property<long>("ProviderId");

                    b.Property<float>("Amount");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<DateTime>("DueDate");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsPaid");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("ClientId", "ProviderId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("CRUD_Server.Models.Provider", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LegalNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("CRUD_Server.Models.Transfer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount");

                    b.Property<long>("ClientId");

                    b.Property<string>("Description")
                        .HasMaxLength(100);

                    b.Property<long>("DestBankAccount");

                    b.Property<DateTime>("TransactionDate");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("CRUD_Server.Models.UserAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ClientId");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("SocialNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("CRUD_Server.Models.BankAccount", b =>
                {
                    b.HasOne("CRUD_Server.Models.Client", "Client")
                        .WithMany("BankAccounts")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRUD_Server.Models.FavAccount", b =>
                {
                    b.HasOne("CRUD_Server.Models.Client", "Client")
                        .WithMany("FavAccounts")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRUD_Server.Models.Payment", b =>
                {
                    b.HasOne("CRUD_Server.Models.Client", "Client")
                        .WithMany("Payments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRUD_Server.Models.Provider", "Provider")
                        .WithMany("Payments")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRUD_Server.Models.Transfer", b =>
                {
                    b.HasOne("CRUD_Server.Models.Client", "Client")
                        .WithMany("Transfers")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CRUD_Server.Models.UserAccount", b =>
                {
                    b.HasOne("CRUD_Server.Models.Client", "Client")
                        .WithOne("UserAccount")
                        .HasForeignKey("CRUD_Server.Models.UserAccount", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
