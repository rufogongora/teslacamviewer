﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using teslacamviewer.web.Data;

namespace teslacamviewer.web.Migrations
{
    [DbContext(typeof(TeslaContext))]
    partial class TeslaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("teslacamviewer.Data.DataModels.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("teslacamviewer.Data.DataModels.TeslaConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Salt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TeslaConfigs");
                });

            modelBuilder.Entity("teslacamviewer.web.Data.DataModels.TeslaClip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActualPath")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Side")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TeslaFolderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TeslaFolderId");

                    b.ToTable("TeslaClips");
                });

            modelBuilder.Entity("teslacamviewer.web.Data.DataModels.TeslaEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<double>("Est_Lat")
                        .HasColumnType("REAL");

                    b.Property<double>("Est_Lon")
                        .HasColumnType("REAL");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TeslaEvents");
                });

            modelBuilder.Entity("teslacamviewer.web.Data.DataModels.TeslaFolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActualPath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HardDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TeslaEventId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Thumbnail")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TeslaEventId");

                    b.ToTable("TeslaFolders");
                });

            modelBuilder.Entity("teslacamviewer.web.Data.DataModels.TeslaClip", b =>
                {
                    b.HasOne("teslacamviewer.web.Data.DataModels.TeslaFolder", null)
                        .WithMany("TeslaClips")
                        .HasForeignKey("TeslaFolderId");
                });

            modelBuilder.Entity("teslacamviewer.web.Data.DataModels.TeslaFolder", b =>
                {
                    b.HasOne("teslacamviewer.web.Data.DataModels.TeslaEvent", "TeslaEvent")
                        .WithMany()
                        .HasForeignKey("TeslaEventId");

                    b.Navigation("TeslaEvent");
                });

            modelBuilder.Entity("teslacamviewer.web.Data.DataModels.TeslaFolder", b =>
                {
                    b.Navigation("TeslaClips");
                });
#pragma warning restore 612, 618
        }
    }
}
