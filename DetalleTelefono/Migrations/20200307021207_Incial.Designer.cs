﻿// <auto-generated />
using System;
using DetalleTelefono.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DetalleTelefono.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20200307021207_Incial")]
    partial class Incial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("DetalleTelefono.Entidades.Personas", b =>
                {
                    b.Property<int>("PersonaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cedula")
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonaId");

                    b.ToTable("personas");
                });

            modelBuilder.Entity("DetalleTelefono.Entidades.TelefonosDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoTelefono")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.ToTable("TelefonosDetalle");
                });

            modelBuilder.Entity("DetalleTelefono.Entidades.TelefonosDetalle", b =>
                {
                    b.HasOne("DetalleTelefono.Entidades.Personas", null)
                        .WithMany("Telefonos")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}