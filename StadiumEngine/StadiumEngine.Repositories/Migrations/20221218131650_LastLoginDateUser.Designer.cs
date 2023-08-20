﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StadiumEngine.Repositories.Infrastructure.Contexts;

#nullable disable

namespace StadiumEngine.Repositories.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20221218131650_LastLoginDateUser")]
    partial class LastLoginDateUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.Legal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("integer")
                        .HasColumnName("city_id");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("HeadName")
                        .HasColumnType("text")
                        .HasColumnName("head_name");

                    b.Property<string>("Inn")
                        .HasColumnType("text")
                        .HasColumnName("inn");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("legal", "accounts");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("PermissionGroupId")
                        .HasColumnType("integer")
                        .HasColumnName("permission_group_id");

                    b.HasKey("Id");

                    b.HasIndex("PermissionGroupId");

                    b.ToTable("permission", "accounts");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.PermissionGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("permission_group", "accounts");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("LegalId")
                        .HasColumnType("integer")
                        .HasColumnName("legal_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int?>("UserCreatedId")
                        .HasColumnType("integer")
                        .HasColumnName("user_created_id");

                    b.Property<int?>("UserModifiedId")
                        .HasColumnType("integer")
                        .HasColumnName("user_modified_id");

                    b.HasKey("Id");

                    b.HasIndex("LegalId");

                    b.HasIndex("UserCreatedId");

                    b.HasIndex("UserModifiedId");

                    b.ToTable("role", "accounts");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer")
                        .HasColumnName("permission_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<int?>("UserCreatedId")
                        .HasColumnType("integer")
                        .HasColumnName("user_created_id");

                    b.Property<int?>("UserModifiedId")
                        .HasColumnType("integer")
                        .HasColumnName("user_modified_id");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserCreatedId");

                    b.HasIndex("UserModifiedId");

                    b.ToTable("role_permission", "accounts");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.RoleStadium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<int>("StadiumId")
                        .HasColumnType("integer")
                        .HasColumnName("stadium_id");

                    b.Property<int?>("UserCreatedId")
                        .HasColumnType("integer")
                        .HasColumnName("user_created_id");

                    b.Property<int?>("UserModifiedId")
                        .HasColumnType("integer")
                        .HasColumnName("user_modified_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StadiumId");

                    b.HasIndex("UserCreatedId");

                    b.HasIndex("UserModifiedId");

                    b.ToTable("role_stadium", "accounts");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsSuperuser")
                        .HasColumnType("boolean")
                        .HasColumnName("is_superuser");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_login_date");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<int>("LegalId")
                        .HasColumnType("integer")
                        .HasColumnName("legal_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<int?>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<int?>("UserCreatedId")
                        .HasColumnType("integer")
                        .HasColumnName("user_created_id");

                    b.Property<int?>("UserModifiedId")
                        .HasColumnType("integer")
                        .HasColumnName("user_modified_id");

                    b.HasKey("Id");

                    b.HasIndex("LegalId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserCreatedId");

                    b.HasIndex("UserModifiedId");

                    b.ToTable("user", "accounts");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Geo.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("RegionId")
                        .HasColumnType("integer")
                        .HasColumnName("region_id");

                    b.Property<string>("ShortName")
                        .HasColumnType("text")
                        .HasColumnName("short_name");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("city", "geo");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Geo.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ShortName")
                        .HasColumnType("text")
                        .HasColumnName("short_name");

                    b.HasKey("Id");

                    b.ToTable("country", "geo");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Geo.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("integer")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ShortName")
                        .HasColumnType("text")
                        .HasColumnName("short_name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("region", "geo");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Offers.Stadium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("integer")
                        .HasColumnName("city_id");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("LegalId")
                        .HasColumnType("integer")
                        .HasColumnName("legal_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("LegalId");

                    b.ToTable("stadium", "offers");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.Legal", b =>
                {
                    b.HasOne("StadiumEngine.Domain.Domain.Geo.City", "City")
                        .WithMany("Legals")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.Permission", b =>
                {
                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.PermissionGroup", "PermissionGroup")
                        .WithMany("Permissions")
                        .HasForeignKey("PermissionGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PermissionGroup");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.Role", b =>
                {
                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.Legal", "Legal")
                        .WithMany("Roles")
                        .HasForeignKey("LegalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.User", "UserCreated")
                        .WithMany("CreatedRoles")
                        .HasForeignKey("UserCreatedId");

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.User", "UserModified")
                        .WithMany("LastModifiedRoles")
                        .HasForeignKey("UserModifiedId");

                    b.Navigation("Legal");

                    b.Navigation("UserCreated");

                    b.Navigation("UserModified");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.RolePermission", b =>
                {
                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.User", "UserCreated")
                        .WithMany("CreatedRolePermissions")
                        .HasForeignKey("UserCreatedId");

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.User", "UserModified")
                        .WithMany("LastModifiedRolePermissions")
                        .HasForeignKey("UserModifiedId");

                    b.Navigation("Permission");

                    b.Navigation("Role");

                    b.Navigation("UserCreated");

                    b.Navigation("UserModified");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.RoleStadium", b =>
                {
                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.Role", "Role")
                        .WithMany("RoleStadiums")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StadiumEngine.Domain.Domain.Offers.Stadium", "Stadium")
                        .WithMany("RoleStadiums")
                        .HasForeignKey("StadiumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.User", "UserCreated")
                        .WithMany("CreatedRoleStadiums")
                        .HasForeignKey("UserCreatedId");

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.User", "UserModified")
                        .WithMany("LastModifiedRoleStadiums")
                        .HasForeignKey("UserModifiedId");

                    b.Navigation("Role");

                    b.Navigation("Stadium");

                    b.Navigation("UserCreated");

                    b.Navigation("UserModified");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.User", b =>
                {
                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.Legal", "Legal")
                        .WithMany("Users")
                        .HasForeignKey("LegalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.User", "UserCreated")
                        .WithMany("CreatedUsers")
                        .HasForeignKey("UserCreatedId");

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.User", "UserModified")
                        .WithMany("LastModifiedUsers")
                        .HasForeignKey("UserModifiedId");

                    b.Navigation("Legal");

                    b.Navigation("Role");

                    b.Navigation("UserCreated");

                    b.Navigation("UserModified");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Geo.City", b =>
                {
                    b.HasOne("StadiumEngine.Domain.Domain.Geo.Region", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Geo.Region", b =>
                {
                    b.HasOne("StadiumEngine.Domain.Domain.Geo.Country", "Country")
                        .WithMany("Regions")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Offers.Stadium", b =>
                {
                    b.HasOne("StadiumEngine.Domain.Domain.Geo.City", "City")
                        .WithMany("Stadiums")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StadiumEngine.Domain.Domain.Accounts.Legal", "Legal")
                        .WithMany("Stadiums")
                        .HasForeignKey("LegalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Legal");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.Legal", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("Stadiums");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.PermissionGroup", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("RoleStadiums");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Accounts.User", b =>
                {
                    b.Navigation("CreatedRolePermissions");

                    b.Navigation("CreatedRoleStadiums");

                    b.Navigation("CreatedRoles");

                    b.Navigation("CreatedUsers");

                    b.Navigation("LastModifiedRolePermissions");

                    b.Navigation("LastModifiedRoleStadiums");

                    b.Navigation("LastModifiedRoles");

                    b.Navigation("LastModifiedUsers");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Geo.City", b =>
                {
                    b.Navigation("Legals");

                    b.Navigation("Stadiums");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Geo.Country", b =>
                {
                    b.Navigation("Regions");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Geo.Region", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("StadiumEngine.Domain.Domain.Offers.Stadium", b =>
                {
                    b.Navigation("RoleStadiums");
                });
#pragma warning restore 612, 618
        }
    }
}
