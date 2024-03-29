﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using teslacamviewer.data.Context;

namespace teslacamviewer.data.Migrations
{
    [DbContext(typeof(TeslaContext))]
    [Migration("20221025044125_AddedPlaceHolderColumn")]
    partial class AddedPlaceHolderColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("teslacamviewer.data.Models.Favorite", b =>
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

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaClip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActualPath")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Favorite")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Side")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeslaClipGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TeslaClipsGroupId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TeslaClipsGroupId");

                    b.ToTable("TeslaClips");
                });

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaClipsGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TeslaFolderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TeslaFolderId");

                    b.ToTable("TeslaClipsGroups");
                });

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaConfig", b =>
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

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastRun")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlaceHolderColumn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TeslaDatas");
                });

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaEvent", b =>
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

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaFolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActualPath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Favorite")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FolderType")
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

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaClip", b =>
                {
                    b.HasOne("teslacamviewer.data.Models.TeslaClipsGroup", null)
                        .WithMany("TeslaClips")
                        .HasForeignKey("TeslaClipsGroupId");
                });

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaClipsGroup", b =>
                {
                    b.HasOne("teslacamviewer.data.Models.TeslaFolder", null)
                        .WithMany("TeslaClipGroups")
                        .HasForeignKey("TeslaFolderId");
                });

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaFolder", b =>
                {
                    b.HasOne("teslacamviewer.data.Models.TeslaEvent", "TeslaEvent")
                        .WithMany()
                        .HasForeignKey("TeslaEventId");

                    b.Navigation("TeslaEvent");
                });

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaClipsGroup", b =>
                {
                    b.Navigation("TeslaClips");
                });

            modelBuilder.Entity("teslacamviewer.data.Models.TeslaFolder", b =>
                {
                    b.Navigation("TeslaClipGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
