// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SiT.Scheduler.Data.PostgreSql;

#nullable disable

namespace SiT.Scheduler.Data.PostgreSql.Migrations
{
    [DbContext(typeof(PostgreSchedulerDbContext))]
    [Migration("20220412170739_RefactoringTheIdentityModel")]
    partial class RefactoringTheIdentityModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategorySong", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uuid");

                    b.HasKey("CategoriesId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("CategorySong");
                });

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.Property<Guid>("GenresId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uuid");

                    b.HasKey("GenresId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("GenreSong");
                });

            modelBuilder.Entity("IdentityTenant", b =>
                {
                    b.Property<Guid>("IdentitiesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantsId")
                        .HasColumnType("uuid");

                    b.HasKey("IdentitiesId", "TenantsId");

                    b.HasIndex("TenantsId");

                    b.ToTable("IdentityTenant");
                });

            modelBuilder.Entity("PerformerSong", b =>
                {
                    b.Property<Guid>("PerformersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongsId")
                        .HasColumnType("uuid");

                    b.HasKey("PerformersId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("PerformerSong");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Identity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("Identities");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Performer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Performers");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Song", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("CategorySong", b =>
                {
                    b.HasOne("SiT.Scheduler.Data.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SiT.Scheduler.Data.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.HasOne("SiT.Scheduler.Data.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SiT.Scheduler.Data.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdentityTenant", b =>
                {
                    b.HasOne("SiT.Scheduler.Data.Models.Identity", null)
                        .WithMany()
                        .HasForeignKey("IdentitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SiT.Scheduler.Data.Models.Tenant", null)
                        .WithMany()
                        .HasForeignKey("TenantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PerformerSong", b =>
                {
                    b.HasOne("SiT.Scheduler.Data.Models.Performer", null)
                        .WithMany()
                        .HasForeignKey("PerformersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SiT.Scheduler.Data.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Category", b =>
                {
                    b.HasOne("SiT.Scheduler.Data.Models.Tenant", "Tenant")
                        .WithMany("Categories")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Genre", b =>
                {
                    b.HasOne("SiT.Scheduler.Data.Models.Tenant", "Tenant")
                        .WithMany("Genres")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Performer", b =>
                {
                    b.HasOne("SiT.Scheduler.Data.Models.Tenant", "Tenant")
                        .WithMany("Performers")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Song", b =>
                {
                    b.HasOne("SiT.Scheduler.Data.Models.Tenant", "Tenant")
                        .WithMany("Songs")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("SiT.Scheduler.Data.Models.Tenant", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Genres");

                    b.Navigation("Performers");

                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
