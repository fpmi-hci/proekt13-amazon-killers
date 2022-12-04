﻿// <auto-generated />
using System;
using AmazonKillers.News.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AmazonKillers.News.Api.Migrations
{
    [DbContext(typeof(NewsContext))]
    partial class NewsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AmazonKillers.News.Api.Models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("CoverImage")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Headline")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PublishingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("News", (string)null);
                });

            modelBuilder.Entity("AmazonKillers.News.Api.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Publishers", (string)null);
                });

            modelBuilder.Entity("AmazonKillers.News.Api.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<string>("SubscriberId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions", (string)null);
                });

            modelBuilder.Entity("AmazonKillers.News.Api.Models.News", b =>
                {
                    b.HasOne("AmazonKillers.News.Api.Models.Publisher", "Publisher")
                        .WithMany("News")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("AmazonKillers.News.Api.Models.Publisher", b =>
                {
                    b.Navigation("News");
                });
#pragma warning restore 612, 618
        }
    }
}
