using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Medications.Models
{
    public partial class MedicationsContext : DbContext
    {
        public MedicationsContext()
        {
        }

        public MedicationsContext(DbContextOptions<MedicationsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Medication> Medications { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Prescription> Prescriptions { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-0S485TL\\MSSQLSERVER01;Database=Medications;Trusted_Connection = yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__Notificat__Patie__440B1D61");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.PrescriptionDoctors)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__Prescript__Docto__3B75D760");

                entity.HasOne(d => d.Medication)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(d => d.MedicationId)
                    .HasConstraintName("FK__Prescript__Medic__3D5E1FD2");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PrescriptionPatients)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__Prescript__Patie__3C69FB99");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__Requests__Patien__403A8C7D");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK__Requests__Prescr__412EB0B6");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
