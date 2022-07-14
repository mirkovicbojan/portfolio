﻿// <auto-generated />
using System;
using Food_Delivery_App.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Food_Delivery_App.Migrations
{
    [DbContext(typeof(FoodAppContext))]
    partial class FoodAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("Food_Delivery_App.Models.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("price")
                        .HasColumnType("REAL");

                    b.Property<Guid>("restaurantId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("restaurantId");

                    b.ToTable("foods");
                });

            modelBuilder.Entity("Food_Delivery_App.Models.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("isDeliveryFree")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("restaurants");
                });

            modelBuilder.Entity("Food_Delivery_App.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Food_Delivery_App.Models.Food", b =>
                {
                    b.HasOne("Food_Delivery_App.Models.Restaurant", "restaurant")
                        .WithMany()
                        .HasForeignKey("restaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("restaurant");
                });
#pragma warning restore 612, 618
        }
    }
}
