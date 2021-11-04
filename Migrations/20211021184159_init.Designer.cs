﻿// <auto-generated />
using CommanderData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CommanderDBMigrationMgr.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211021184159_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommanderDA.Entities.Command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommandLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HowTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ToolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ToolId");

                    b.ToTable("Commands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CommandLine = "dotnet user-secrets init",
                            HowTo = "Enable secret storage for project",
                            ToolId = 1
                        },
                        new
                        {
                            Id = 2,
                            CommandLine = "dotnet user-secrets set \"<key>\" \"<value>\"",
                            HowTo = "Set secret for project",
                            ToolId = 1
                        },
                        new
                        {
                            Id = 3,
                            CommandLine = "dotnet new gitignore",
                            HowTo = "Generate default dotnet gitignore file for project",
                            ToolId = 1
                        },
                        new
                        {
                            Id = 4,
                            CommandLine = "git init",
                            HowTo = "Create an empty Git repository or reinitialize an existing one",
                            ToolId = 3
                        });
                });

            modelBuilder.Entity("CommanderDA.Entities.Tool", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tools");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "",
                            Name = "dotnet"
                        },
                        new
                        {
                            Id = 2,
                            Description = "",
                            Name = "docker"
                        },
                        new
                        {
                            Id = 3,
                            Description = "",
                            Name = "git"
                        });
                });

            modelBuilder.Entity("CommanderDA.Entities.Command", b =>
                {
                    b.HasOne("CommanderDA.Entities.Tool", "Tool")
                        .WithMany("Commands")
                        .HasForeignKey("ToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tool");
                });

            modelBuilder.Entity("CommanderDA.Entities.Tool", b =>
                {
                    b.Navigation("Commands");
                });
#pragma warning restore 612, 618
        }
    }
}
