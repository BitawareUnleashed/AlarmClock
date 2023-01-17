﻿// <auto-generated />
using BlazorAlarmClock.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorAlarmClock.Server.Migrations
{
    [DbContext(typeof(AlarmDbContext))]
    partial class AlarmDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("BlazorAlarmClock.Shared.Models.Alarm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Hour")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Minute")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RingtoneName")
                        .HasColumnType("TEXT");

                    b.Property<int>("SnoozeTime")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Alarms");
                });

            modelBuilder.Entity("BlazorAlarmClock.Shared.Models.AlarmDay", b =>
                {
                    b.Property<int>("AlarmDayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlarmId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DayAsInt")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlarmDayId");

                    b.HasIndex("AlarmId");

                    b.ToTable("AlarmDay");
                });

            modelBuilder.Entity("BlazorAlarmClock.Shared.Models.AlarmDay", b =>
                {
                    b.HasOne("BlazorAlarmClock.Shared.Models.Alarm", "Alarm")
                        .WithMany("AlarmDays")
                        .HasForeignKey("AlarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alarm");
                });

            modelBuilder.Entity("BlazorAlarmClock.Shared.Models.Alarm", b =>
                {
                    b.Navigation("AlarmDays");
                });
#pragma warning restore 612, 618
        }
    }
}