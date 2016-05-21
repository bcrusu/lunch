using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using lunch.Repositories.Impl;

namespace lunch.Repositories.Migrations.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("lunch.Domain.Security.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 1024);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 254);

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("PictureUrl")
                        .HasAnnotation("MaxLength", 2048);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("lunch.Domain.Security.UserSession", b =>
                {
                    b.Property<Guid>("Token");

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("State");

                    b.Property<int>("UserId");

                    b.HasKey("Token");

                    b.HasIndex("UserId");

                    b.ToTable("UserSessions");
                });

            modelBuilder.Entity("lunch.Domain.Security.UserSession", b =>
                {
                    b.HasOne("lunch.Domain.Security.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
