﻿// <auto-generated />
using System;
using EasyLearn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyLearn.Data.Migrations
{
    [DbContext(typeof(EasyLearnDbContext))]
    [Migration("20220313152604_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EasyLearn.Data.Models.CommonRelation", b =>
                {
                    b.Property<int>("EnglishWordId")
                        .HasColumnType("int");

                    b.Property<int>("RussianWordId")
                        .HasColumnType("int");

                    b.Property<int>("WordListId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("EnglishWordId", "RussianWordId", "WordListId");

                    b.HasIndex("RussianWordId");

                    b.HasIndex("WordListId");

                    b.ToTable("CommonRelations");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.CommonWordList", b =>
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CommonWordLists");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.EasyLearnUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("bit");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EnglishUnits");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.Example", b =>
                {
                    b.Property<int>("RussianTranslationId")
                        .HasColumnType("int");

                    b.Property<int>("EnglishTranslationId")
                        .HasColumnType("int");

                    b.Property<int?>("CommonRelationEnglishWordId")
                        .HasColumnType("int");

                    b.Property<int?>("CommonRelationRussianWordId")
                        .HasColumnType("int");

                    b.Property<int?>("CommonRelationWordListId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("IrregularVerbId")
                        .HasColumnType("int");

                    b.Property<int?>("VerbPrepositionListId")
                        .HasColumnType("int");

                    b.Property<int?>("VerbPrepositionPrepositionId")
                        .HasColumnType("int");

                    b.Property<int?>("VerbPrepositionVerbId")
                        .HasColumnType("int");

                    b.HasKey("RussianTranslationId", "EnglishTranslationId");

                    b.HasIndex("EnglishTranslationId");

                    b.HasIndex("IrregularVerbId");

                    b.HasIndex("CommonRelationEnglishWordId", "CommonRelationRussianWordId", "CommonRelationWordListId");

                    b.HasIndex("VerbPrepositionVerbId", "VerbPrepositionPrepositionId", "VerbPrepositionListId");

                    b.ToTable("Examples");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.IrregularVerb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("FirstFormId")
                        .HasColumnType("int");

                    b.Property<int>("RussianWordId")
                        .HasColumnType("int");

                    b.Property<int>("SecondFormId")
                        .HasColumnType("int");

                    b.Property<int>("ThirdFormId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FirstFormId");

                    b.HasIndex("RussianWordId");

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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RussianUnits");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPreposition", b =>
                {
                    b.Property<int>("VerbId")
                        .HasColumnType("int");

                    b.Property<int>("PrepositionId")
                        .HasColumnType("int");

                    b.Property<int>("VerbPrepositionListId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("VerbId", "PrepositionId", "VerbPrepositionListId");

                    b.HasIndex("PrepositionId");

                    b.HasIndex("VerbPrepositionListId");

                    b.ToTable("VerbPrepositions");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPrepositionList", b =>
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("VerbPrepositionLists");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.CommonRelation", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.EnglishUnit", "EnglishWord")
                        .WithMany()
                        .HasForeignKey("EnglishWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.RussianUnit", "RussianWord")
                        .WithMany()
                        .HasForeignKey("RussianWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.CommonWordList", "WordList")
                        .WithMany("Relations")
                        .HasForeignKey("WordListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnglishWord");

                    b.Navigation("RussianWord");

                    b.Navigation("WordList");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.CommonWordList", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.EasyLearnUser", "User")
                        .WithMany("CommonWordLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.Example", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.EnglishUnit", "EnglishTranslation")
                        .WithMany()
                        .HasForeignKey("EnglishTranslationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.IrregularVerb", null)
                        .WithMany("Examples")
                        .HasForeignKey("IrregularVerbId");

                    b.HasOne("EasyLearn.Data.Models.RussianUnit", "RussianTranslation")
                        .WithMany()
                        .HasForeignKey("RussianTranslationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.CommonRelation", null)
                        .WithMany("Examples")
                        .HasForeignKey("CommonRelationEnglishWordId", "CommonRelationRussianWordId", "CommonRelationWordListId");

                    b.HasOne("EasyLearn.Data.Models.VerbPreposition", null)
                        .WithMany("Examples")
                        .HasForeignKey("VerbPrepositionVerbId", "VerbPrepositionPrepositionId", "VerbPrepositionListId");

                    b.Navigation("EnglishTranslation");

                    b.Navigation("RussianTranslation");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.IrregularVerb", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.EnglishUnit", "FirstForm")
                        .WithMany()
                        .HasForeignKey("FirstFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLearn.Data.Models.RussianUnit", "RussianWord")
                        .WithMany()
                        .HasForeignKey("RussianWordId")
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

                    b.Navigation("RussianWord");

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

                    b.HasOne("EasyLearn.Data.Models.VerbPrepositionList", "VerbPrepositionList")
                        .WithMany("VerbPrepositions")
                        .HasForeignKey("VerbPrepositionListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Preposition");

                    b.Navigation("Verb");

                    b.Navigation("VerbPrepositionList");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPrepositionList", b =>
                {
                    b.HasOne("EasyLearn.Data.Models.EasyLearnUser", "User")
                        .WithMany("VerbPrepositionLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.CommonRelation", b =>
                {
                    b.Navigation("Examples");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.CommonWordList", b =>
                {
                    b.Navigation("Relations");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.EasyLearnUser", b =>
                {
                    b.Navigation("CommonWordLists");

                    b.Navigation("VerbPrepositionLists");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.IrregularVerb", b =>
                {
                    b.Navigation("Examples");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPreposition", b =>
                {
                    b.Navigation("Examples");
                });

            modelBuilder.Entity("EasyLearn.Data.Models.VerbPrepositionList", b =>
                {
                    b.Navigation("VerbPrepositions");
                });
#pragma warning restore 612, 618
        }
    }
}
