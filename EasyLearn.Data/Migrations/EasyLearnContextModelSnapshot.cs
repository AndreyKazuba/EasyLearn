﻿// <auto-generated />
using System;
using EasyLearn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    [DbContext(typeof(EasyLearnContext))]
    partial class EasyLearnContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EasyLearn.Data.Models.CommonDictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ChangeDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(85)
                        .HasColumnType("nvarchar(85)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CommonDictionaries");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.CommonRelation", b =>
                {
                    b.Property<int>("EnglishUnitId")
                        .HasColumnType("int");

                    b.Property<int>("RussianUnitId")
                        .HasColumnType("int");

                    b.Property<int>("CommonDictionaryId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstExampleEnglishValue")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("FirstExampleRussianValue")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("SecondExampleEnglishValue")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("SecondExampleRussianValue")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<DateTime?>("UpdateDateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("EnglishUnitId", "RussianUnitId", "CommonDictionaryId");

                    b.HasIndex("CommonDictionaryId");

                    b.HasIndex("RussianUnitId");

                    b.ToTable("CommonRelations");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.EasyLearnUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.EnglishUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("EnglishUnits");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.IrregularVerb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("FirstFormId")
                        .HasColumnType("int");

                    b.Property<int>("RussianUnitId")
                        .HasColumnType("int");

                    b.Property<int>("SecondFormId")
                        .HasColumnType("int");

                    b.Property<int>("ThirdFormId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FirstFormId");

                    b.HasIndex("RussianUnitId");

                    b.HasIndex("SecondFormId");

                    b.HasIndex("ThirdFormId");

                    b.ToTable("IrregularVerbs");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.RussianUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("RussianUnits");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPreposition", b =>
                {
                    b.Property<int>("VerbId")
                        .HasColumnType("int");

                    b.Property<int>("PrepositionId")
                        .HasColumnType("int");

                    b.Property<int>("VerbPrepositionDictionaryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstExampleEnglishValue")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("FirstExampleRussianValue")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("SecondExampleEnglishValue")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("SecondExampleRussianValue")
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("UpdateDateUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("VerbId", "PrepositionId", "VerbPrepositionDictionaryId");

                    b.HasIndex("PrepositionId");

                    b.HasIndex("VerbPrepositionDictionaryId");

                    b.ToTable("VerbPrepositions");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPrepositionDictionnary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("ChangeDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(85)
                        .HasColumnType("nvarchar(85)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("VerbPrepositionDictionaries");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.CommonDictionary", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.EasyLearnUser", "User")
                        .WithMany("CommonDictionaries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.CommonRelation", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.CommonDictionary", "CommonDictionary")
                        .WithMany("Relations")
                        .HasForeignKey("CommonDictionaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.EnglishUnit", "EnglishUnit")
                        .WithMany()
                        .HasForeignKey("EnglishUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.RussianUnit", "RussianUnit")
                        .WithMany()
                        .HasForeignKey("RussianUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommonDictionary");

                    b.Navigation("EnglishUnit");

                    b.Navigation("RussianUnit");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.IrregularVerb", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.EnglishUnit", "FirstForm")
                        .WithMany()
                        .HasForeignKey("FirstFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.RussianUnit", "RussianUnit")
                        .WithMany()
                        .HasForeignKey("RussianUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.EnglishUnit", "SecondForm")
                        .WithMany()
                        .HasForeignKey("SecondFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.EnglishUnit", "ThirdForm")
                        .WithMany()
                        .HasForeignKey("ThirdFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FirstForm");

                    b.Navigation("RussianUnit");

                    b.Navigation("SecondForm");

                    b.Navigation("ThirdForm");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPreposition", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.EnglishUnit", "Preposition")
                        .WithMany()
                        .HasForeignKey("PrepositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.EnglishUnit", "Verb")
                        .WithMany()
                        .HasForeignKey("VerbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.VerbPrepositionDictionnary", "VerbPrepositionDictionary")
                        .WithMany("VerbPrepositions")
                        .HasForeignKey("VerbPrepositionDictionaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Preposition");

                    b.Navigation("Verb");

                    b.Navigation("VerbPrepositionDictionary");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPrepositionDictionnary", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.EasyLearnUser", "User")
                        .WithMany("VerbPrepositionDictionaries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.CommonDictionary", b =>
                {
                    b.Navigation("Relations");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.EasyLearnUser", b =>
                {
                    b.Navigation("CommonDictionaries");

                    b.Navigation("VerbPrepositionDictionaries");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPrepositionDictionnary", b =>
                {
                    b.Navigation("VerbPrepositions");
                });
#pragma warning restore 612, 618
        }
    }
}
