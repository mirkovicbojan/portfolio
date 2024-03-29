﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeSheet.Contexts;

#nullable disable

namespace TimeSheet.Migrations
{
    [DbContext(typeof(TimeSheetContext))]
    [Migration("20220710181209_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("TimeSheet.Models.Category", b =>
                {
                    b.Property<int>("categoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("categoryDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("categoryName")
                        .HasColumnType("TEXT");

                    b.HasKey("categoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TimeSheet.Models.Client", b =>
                {
                    b.Property<int>("clientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("address")
                        .HasColumnType("TEXT");

                    b.Property<string>("city")
                        .HasColumnType("TEXT");

                    b.Property<string>("clientName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("country")
                        .HasColumnType("TEXT");

                    b.Property<int>("zip")
                        .HasColumnType("INTEGER");

                    b.HasKey("clientID");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("TimeSheet.Models.Member", b =>
                {
                    b.Property<int>("memberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<float>("hoursPerWeek")
                        .HasColumnType("REAL");

                    b.Property<string>("memberName")
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("role")
                        .HasColumnType("INTEGER");

                    b.Property<int>("status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("username")
                        .HasColumnType("TEXT");

                    b.HasKey("memberID");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("TimeSheet.Models.Project", b =>
                {
                    b.Property<int>("projectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("currentclientID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("memberID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("projectDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("projectName")
                        .HasColumnType("TEXT");

                    b.HasKey("projectID");

                    b.HasIndex("currentclientID");

                    b.HasIndex("memberID");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TimeSheet.Models.TimeSheetClass", b =>
                {
                    b.Property<int>("sheetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("categoryID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("clientID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("memberID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("overtime")
                        .HasColumnType("REAL");

                    b.Property<int?>("projectID")
                        .HasColumnType("INTEGER");

                    b.Property<double>("time")
                        .HasColumnType("REAL");

                    b.HasKey("sheetID");

                    b.HasIndex("categoryID");

                    b.HasIndex("clientID");

                    b.HasIndex("memberID");

                    b.HasIndex("projectID");

                    b.ToTable("TimeSheets");
                });

            modelBuilder.Entity("TimeSheet.Models.Project", b =>
                {
                    b.HasOne("TimeSheet.Models.Client", "currentClient")
                        .WithMany()
                        .HasForeignKey("currentclientID");

                    b.HasOne("TimeSheet.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("memberID");

                    b.Navigation("Member");

                    b.Navigation("currentClient");
                });

            modelBuilder.Entity("TimeSheet.Models.TimeSheetClass", b =>
                {
                    b.HasOne("TimeSheet.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("categoryID");

                    b.HasOne("TimeSheet.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("clientID");

                    b.HasOne("TimeSheet.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("memberID");

                    b.HasOne("TimeSheet.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("projectID");

                    b.Navigation("Category");

                    b.Navigation("Client");

                    b.Navigation("Member");

                    b.Navigation("Project");
                });
#pragma warning restore 612, 618
        }
    }
}
