﻿// <auto-generated />
using System;
using GarageGenius.Modules.Vehicles.Infrastructure.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GarageGenius.Modules.Vehicles.Infrastructure.Persistance.Migrations
{
    [DbContext(typeof(VehiclesDbContext))]
    partial class VehiclesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("vehicles")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GarageGenius.Modules.Vehicles.Core.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Vin")
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.Property<int?>("Year")
                        .HasMaxLength(8)
                        .HasColumnType("int");

                    b.HasKey("VehicleId");

                    b.HasIndex("VehicleId");

                    b.HasIndex("Vin")
                        .IsUnique()
                        .HasFilter("[Vin] IS NOT NULL");

                    b.ToTable("Vehicles", "vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
