﻿// <auto-generated />
using System;
using IMMRequest.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IMMRequest.DataAccess.Migrations
{
    [DbContext(typeof(IMMRequestContext))]
    [Migration("20200403224226_CreateDb")]
    partial class CreateDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IMMRequest.Domain.Citizen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Citizens");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Citizen");
                });

            modelBuilder.Entity("IMMRequest.Domain.Fields.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RequestTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestTypeId");

                    b.ToTable("Field");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Field");
                });

            modelBuilder.Entity("IMMRequest.Domain.Fields.Item<System.DateTime>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DateFieldId")
                        .HasColumnType("int");

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Value")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DateFieldId");

                    b.ToTable("Item<DateTime>");
                });

            modelBuilder.Entity("IMMRequest.Domain.Fields.Item<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<int?>("IntegerFieldId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IntegerFieldId");

                    b.ToTable("Item<int>");
                });

            modelBuilder.Entity("IMMRequest.Domain.RequestArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RequestAreas");
                });

            modelBuilder.Entity("IMMRequest.Domain.RequestTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RequestAreaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestAreaId");

                    b.ToTable("RequestTopics");
                });

            modelBuilder.Entity("IMMRequest.Domain.RequestType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RequestTopicId")
                        .HasColumnType("int");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RequestTopicId");

                    b.ToTable("RequestTypes");
                });

            modelBuilder.Entity("IMMRequest.Domain.Admin", b =>
                {
                    b.HasBaseType("IMMRequest.Domain.Citizen");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("IMMRequest.Domain.Fields.DateField", b =>
                {
                    b.HasBaseType("IMMRequest.Domain.Fields.Field");

                    b.Property<DateTime>("Type")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("DateField");
                });

            modelBuilder.Entity("IMMRequest.Domain.Fields.IntegerField", b =>
                {
                    b.HasBaseType("IMMRequest.Domain.Fields.Field");

                    b.Property<int>("Type")
                        .HasColumnName("IntegerField_Type")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("IntegerField");
                });

            modelBuilder.Entity("IMMRequest.Domain.Fields.Field", b =>
                {
                    b.HasOne("IMMRequest.Domain.RequestType", null)
                        .WithMany("AditionalFields")
                        .HasForeignKey("RequestTypeId");
                });

            modelBuilder.Entity("IMMRequest.Domain.Fields.Item<System.DateTime>", b =>
                {
                    b.HasOne("IMMRequest.Domain.Fields.DateField", null)
                        .WithMany("Range")
                        .HasForeignKey("DateFieldId");
                });

            modelBuilder.Entity("IMMRequest.Domain.Fields.Item<int>", b =>
                {
                    b.HasOne("IMMRequest.Domain.Fields.IntegerField", null)
                        .WithMany("Range")
                        .HasForeignKey("IntegerFieldId");
                });

            modelBuilder.Entity("IMMRequest.Domain.RequestTopic", b =>
                {
                    b.HasOne("IMMRequest.Domain.RequestArea", null)
                        .WithMany("Topics")
                        .HasForeignKey("RequestAreaId");
                });

            modelBuilder.Entity("IMMRequest.Domain.RequestType", b =>
                {
                    b.HasOne("IMMRequest.Domain.RequestTopic", null)
                        .WithMany("Types")
                        .HasForeignKey("RequestTopicId");
                });
#pragma warning restore 612, 618
        }
    }
}
