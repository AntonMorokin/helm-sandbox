﻿// <auto-generated />
using System;
using Crs.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Crs.Backend.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("crs")
                .UseCollation("ru_RU.utf8")
                .HasAnnotation("Npgsql:DatabaseTemplate", "template0")
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Crs.Backend.Data.Model.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("brand");

                    b.Property<double>("Mileage")
                        .HasColumnType("double precision")
                        .HasColumnName("mileage");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("model");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("number");

                    b.HasKey("Id")
                        .HasName("pk_cars");

                    b.HasIndex("Number")
                        .IsUnique()
                        .HasDatabaseName("ix_cars_number");

                    b.ToTable("cars", "crs");
                });

            modelBuilder.Entity("Crs.Backend.Data.Model.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_clients");

                    b.ToTable("clients", "crs");
                });

            modelBuilder.Entity("Crs.Backend.Data.Model.Ride", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("integer")
                        .HasColumnName("car_id");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer")
                        .HasColumnName("client_id");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_time");

                    b.Property<double?>("Mileage")
                        .HasColumnType("double precision")
                        .HasColumnName("mileage");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_time");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_rides");

                    b.HasIndex("CarId")
                        .HasDatabaseName("ix_rides_car_id");

                    b.HasIndex("ClientId")
                        .HasDatabaseName("ix_rides_client_id");

                    b.ToTable("rides", "crs");
                });

            modelBuilder.Entity("Crs.Backend.Data.Model.Ride", b =>
                {
                    b.HasOne("Crs.Backend.Data.Model.Car", "Car")
                        .WithMany("Rides")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rides_cars_car_id");

                    b.HasOne("Crs.Backend.Data.Model.Client", "Client")
                        .WithMany("Rides")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rides_clients_client_id");

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Crs.Backend.Data.Model.Car", b =>
                {
                    b.Navigation("Rides");
                });

            modelBuilder.Entity("Crs.Backend.Data.Model.Client", b =>
                {
                    b.Navigation("Rides");
                });
#pragma warning restore 612, 618
        }
    }
}
