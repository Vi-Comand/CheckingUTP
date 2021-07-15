﻿// <auto-generated />
using CheckingUTP.Models.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CheckingUTP.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("CheckingUTP.Models.DataBase.Forma_obuchen", b =>
                {
                    b.Property<int>("Forma_obucen_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Forma_obucen_id");

                    b.ToTable("Forma_obuchen");
                });

            modelBuilder.Entity("CheckingUTP.Models.DataBase.Type_trainig_load", b =>
                {
                    b.Property<int>("Type_trainig_load_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<float>("Number_control_forms")
                        .HasColumnType("float");

                    b.Property<float>("Number_groups")
                        .HasColumnType("float");

                    b.Property<float>("Number_listeners")
                        .HasColumnType("float");

                    b.Property<float>("Number_subgroups")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Utp_id")
                        .HasColumnType("int");

                    b.Property<float>("Voulme_hours")
                        .HasColumnType("float");

                    b.Property<float>("Voulme_hours_per_listener")
                        .HasColumnType("float");

                    b.HasKey("Type_trainig_load_id");

                    b.ToTable("Type_trainig_load");
                });

            modelBuilder.Entity("CheckingUTP.Models.DataBase.UTP", b =>
                {
                    b.Property<int>("Utp_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Forma_obuchen_id")
                        .HasColumnType("int");

                    b.Property<float>("Hour")
                        .HasColumnType("float");

                    b.Property<int>("Kol_groups")
                        .HasColumnType("int");

                    b.Property<int>("Kol_slushatel_v_group")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Rejim_zanyati")
                        .HasColumnType("int");

                    b.HasKey("Utp_id");

                    b.ToTable("UTPs");
                });
#pragma warning restore 612, 618
        }
    }
}
