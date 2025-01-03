﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Unfield.Repositories.Infrastructure.Contexts;

#nullable disable

namespace Unfield.Repositories.Migrations.Archive
{
    [DbContext(typeof(ArchiveDbContext))]
    partial class ArchiveDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Unfield.Domain.Entities.Dashboard.StadiumDashboard", b =>
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

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_modified");

                    b.Property<int>("StadiumId")
                        .HasColumnType("integer")
                        .HasColumnName("stadium_id");

                    b.HasKey("Id");

                    b.ToTable("stadium_dashboard", "dashboards");
                });

            modelBuilder.Entity("Unfield.Domain.Entities.Dashboard.StadiumDashboard", b =>
                {
                    b.OwnsOne("Unfield.Domain.Entities.Dashboard.StadiumDashboardData", "Data", b1 =>
                        {
                            b1.Property<int>("StadiumDashboardId")
                                .HasColumnType("integer");

                            b1.HasKey("StadiumDashboardId");

                            b1.ToTable("stadium_dashboard", "dashboards");

                            b1.ToJson("data");

                            b1.WithOwner()
                                .HasForeignKey("StadiumDashboardId");

                            b1.OwnsOne("Unfield.Domain.Entities.Dashboard.DashboardAverageBill", "AverageBill", b2 =>
                                {
                                    b2.Property<int>("StadiumDashboardDataStadiumDashboardId")
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .HasColumnType("text");

                                    b2.HasKey("StadiumDashboardDataStadiumDashboardId");

                                    b2.ToTable("stadium_dashboard", "dashboards");

                                    b2.WithOwner()
                                        .HasForeignKey("StadiumDashboardDataStadiumDashboardId");
                                });

                            b1.OwnsOne("Unfield.Domain.Entities.Dashboard.DashboardFieldDistribution", "FieldDistribution", b2 =>
                                {
                                    b2.Property<int>("StadiumDashboardDataStadiumDashboardId")
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .HasColumnType("text");

                                    b2.HasKey("StadiumDashboardDataStadiumDashboardId");

                                    b2.ToTable("stadium_dashboard", "dashboards");

                                    b2.WithOwner()
                                        .HasForeignKey("StadiumDashboardDataStadiumDashboardId");
                                });

                            b1.OwnsOne("Unfield.Domain.Entities.Dashboard.DashboardPopularInventory", "PopularInventory", b2 =>
                                {
                                    b2.Property<int>("StadiumDashboardDataStadiumDashboardId")
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .HasColumnType("text");

                                    b2.HasKey("StadiumDashboardDataStadiumDashboardId");

                                    b2.ToTable("stadium_dashboard", "dashboards");

                                    b2.WithOwner()
                                        .HasForeignKey("StadiumDashboardDataStadiumDashboardId");
                                });

                            b1.OwnsOne("Unfield.Domain.Entities.Dashboard.DashboardTimeChart", "TimeChart", b2 =>
                                {
                                    b2.Property<int>("StadiumDashboardDataStadiumDashboardId")
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .HasColumnType("text");

                                    b2.HasKey("StadiumDashboardDataStadiumDashboardId");

                                    b2.ToTable("stadium_dashboard", "dashboards");

                                    b2.WithOwner()
                                        .HasForeignKey("StadiumDashboardDataStadiumDashboardId");
                                });

                            b1.OwnsOne("Unfield.Domain.Entities.Dashboard.DashboardYearChart", "YearChart", b2 =>
                                {
                                    b2.Property<int>("StadiumDashboardDataStadiumDashboardId")
                                        .HasColumnType("integer");

                                    b2.Property<string>("Name")
                                        .HasColumnType("text");

                                    b2.HasKey("StadiumDashboardDataStadiumDashboardId");

                                    b2.ToTable("stadium_dashboard", "dashboards");

                                    b2.WithOwner()
                                        .HasForeignKey("StadiumDashboardDataStadiumDashboardId");
                                });

                            b1.Navigation("AverageBill");

                            b1.Navigation("FieldDistribution");

                            b1.Navigation("PopularInventory");

                            b1.Navigation("TimeChart");

                            b1.Navigation("YearChart");
                        });

                    b.Navigation("Data");
                });
#pragma warning restore 612, 618
        }
    }
}
