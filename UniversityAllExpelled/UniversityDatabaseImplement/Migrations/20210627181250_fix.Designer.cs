﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityDatabaseImplement;

namespace UniversityDatabaseImplement.Migrations
{
    [DbContext(typeof(UniversityDatabase))]
    [Migration("20210627181250_fix")]
    partial class fix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UniversityDatabaseImplement.Models.Certification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DenearyLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentGradebookNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Certifications");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.CheckList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfExam")
                        .HasColumnType("datetime2");

                    b.Property<int>("LectorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CheckLists");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.Deneary", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Login");

                    b.ToTable("Denearies");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.Department", b =>
                {
                    b.Property<string>("DepartmentLogin")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentLogin");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.EducationPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.Property<string>("StreamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EducationPlans");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.EducationPlanLector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EducationPlanId")
                        .HasColumnType("int");

                    b.Property<int>("LectorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EducationPlanId");

                    b.HasIndex("LectorId");

                    b.ToTable("EducationPlanLectors");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.EducationPlanStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EducationPlanId")
                        .HasColumnType("int");

                    b.Property<string>("StudentGradebookNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EducationPlanId");

                    b.HasIndex("StudentGradebookNumber");

                    b.ToTable("EducationPlanStudents");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.Lector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Lectors");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.Student", b =>
                {
                    b.Property<string>("GradebookNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GradebookNumber");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.StudentSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StudentGradebookNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentGradebookNumber");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.EducationPlanLector", b =>
                {
                    b.HasOne("UniversityDatabaseImplement.Models.EducationPlan", "EducationPlan")
                        .WithMany("EducationPlanLectors")
                        .HasForeignKey("EducationPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityDatabaseImplement.Models.Lector", "Lector")
                        .WithMany("EducationPlanLectors")
                        .HasForeignKey("LectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.EducationPlanStudent", b =>
                {
                    b.HasOne("UniversityDatabaseImplement.Models.EducationPlan", "EducationPlan")
                        .WithMany("EducationPlanStudents")
                        .HasForeignKey("EducationPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityDatabaseImplement.Models.Student", "Student")
                        .WithMany("EducationPlanStudents")
                        .HasForeignKey("StudentGradebookNumber");
                });

            modelBuilder.Entity("UniversityDatabaseImplement.Models.StudentSubject", b =>
                {
                    b.HasOne("UniversityDatabaseImplement.Models.Student", "Student")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudentGradebookNumber");

                    b.HasOne("UniversityDatabaseImplement.Models.Subject", "Subject")
                        .WithMany("StudentSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
