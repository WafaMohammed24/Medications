using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medications.Models
{
    public partial class Prescription
    {
        public Prescription()
        {
            Requests = new HashSet<Request>();
        }

        [Key]
        [Column("PrescriptionID")]
        public int PrescriptionId { get; set; }
        [Column("DoctorID")]
        public int? DoctorId { get; set; }
        [Column("PatientID")]
        public int? PatientId { get; set; }
        [Column("MedicationID")]
        public int? MedicationId { get; set; }
        [StringLength(50)]
        public string? Dosage { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        [StringLength(50)]
        public string? Frequency { get; set; }

        [ForeignKey(nameof(DoctorId))]
        [InverseProperty(nameof(User.PrescriptionDoctors))]
        public virtual User? Doctor { get; set; }
        [ForeignKey(nameof(MedicationId))]
        [InverseProperty("Prescriptions")]
        public virtual Medication? Medication { get; set; }
        [ForeignKey(nameof(PatientId))]
        [InverseProperty(nameof(User.PrescriptionPatients))]
        public virtual User? Patient { get; set; }
        [InverseProperty(nameof(Request.Prescription))]
        public virtual ICollection<Request> Requests { get; set; }
    }
}
