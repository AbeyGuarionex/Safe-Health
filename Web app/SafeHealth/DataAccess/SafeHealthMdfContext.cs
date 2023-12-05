using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SafeHealth.Domain;

namespace SafeHealth.DataAccess
{
    public partial class SafeHealthMdfContext : DbContext
    {
        public SafeHealthMdfContext()
        {
        }

        public SafeHealthMdfContext(DbContextOptions<SafeHealthMdfContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<MedicalInsurance> MedicalInsurances { get; set; } = null!;
        public virtual DbSet<Office> Offices { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\abeyv\\Documents\\Universidad\\5 ano 1 sesmestre\\SICI Omar\\Git\\Safe-Health-main\\Safe-Health-main\\Web app\\SafeHealth\\SafeHealthDB.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => new { e.UserCodeFk1, e.UploadedDocDate })
                    .HasName("documentsPK");

                entity.ToTable("DOCUMENTS");

                entity.Property(e => e.UserCodeFk1)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("userCodeFK1");

                entity.Property(e => e.UploadedDocDate)
                    .HasColumnType("date")
                    .HasColumnName("uploadedDocDate");

                entity.Property(e => e.AuthorizedStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("authorizedStatus");

                entity.Property(e => e.Document1).HasColumnName("document");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("documentType");

                entity.Property(e => e.UserEmailFk2)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("userEmailFK2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => new { d.UserCodeFk1, d.UserEmailFk2 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DOCUMENTS__160F4887");
            });

            modelBuilder.Entity<MedicalInsurance>(entity =>
            {
                entity.HasKey(e => e.MedicalInsuranceCodePk)
                    .HasName("medicalInsuranceCodePK");

                entity.ToTable("MEDICAL_INSURANCE");

                entity.Property(e => e.MedicalInsuranceCodePk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("medicalInsuranceCodePK");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasKey(e => e.OfficeCodePk)
                    .HasName("officeCodePK");

                entity.ToTable("OFFICE");

                entity.Property(e => e.OfficeCodePk)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("officeCodePK");

                entity.Property(e => e.AddressFirstLine)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("addressFirstLine");

                entity.Property(e => e.AddressSecondtLine)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("addressSecondtLine");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("postalCode");

                entity.Property(e => e.TelephoneNumber)
                    .HasMaxLength(10)
                    .HasColumnName("telephoneNumber")
                    .IsFixedLength();

                entity.HasMany(d => d.MedicalInsuranceCodeFk2s)
                    .WithMany(p => p.OfficeCodeFk1s)
                    .UsingEntity<Dictionary<string, object>>(
                        "OfficePlan",
                        l => l.HasOne<MedicalInsurance>().WithMany().HasForeignKey("MedicalInsuranceCodeFk2").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__OFFICE_PL__medic__5DCAEF64"),
                        r => r.HasOne<Office>().WithMany().HasForeignKey("OfficeCodeFk1").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__OFFICE_PL__offic__5CD6CB2B"),
                        j =>
                        {
                            j.HasKey("OfficeCodeFk1", "MedicalInsuranceCodeFk2").HasName("officePlansPK");

                            j.ToTable("OFFICE_PLANS");

                            j.IndexerProperty<string>("OfficeCodeFk1").HasMaxLength(8).IsUnicode(false).HasColumnName("officeCodeFK1");

                            j.IndexerProperty<string>("MedicalInsuranceCodeFk2").HasMaxLength(10).IsUnicode(false).HasColumnName("medicalInsuranceCodeFK2");
                        });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.UserCodePk1, e.UserEmailPk2 })
                    .HasName("userCodePK");

                entity.ToTable("USER");

                entity.Property(e => e.UserCodePk1)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("userCodePK1");

                entity.Property(e => e.UserEmailPk2)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("userEmailPK2");

                entity.Property(e => e.DoctorLicenseNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("doctorLicenseNumber");

                entity.Property(e => e.DoctorLicenseTitle)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("doctorLicenseTitle");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.MaternalLastName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("maternalLastName");

                entity.Property(e => e.PaternalLastName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("paternalLastName");

                entity.Property(e => e.PatientMedicalInsuranceCodeFk)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("patientMedicalInsuranceCodeFK");

                entity.Property(e => e.PolicyNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("policyNumber");

                entity.Property(e => e.TelephoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telephoneNumber");

                entity.Property(e => e.UserOfficeCodeFk)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("userOfficeCodeFK");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("userPassword");

                entity.Property(e => e.UserType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("userType");

                entity.HasOne(d => d.PatientMedicalInsuranceCodeFkNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PatientMedicalInsuranceCodeFk)
                    .HasConstraintName("FK__USER__patientMed__01142BA1");

                entity.HasOne(d => d.UserOfficeCodeFkNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserOfficeCodeFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USER__userOffice__6FE99F9F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
