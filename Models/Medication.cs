using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medications.Models
{
    public partial class Medication
    {
        public Medication()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        [Key]
        [Column("MedicationID")]
        public int MedicationId { get; set; }
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Dosage { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [InverseProperty(nameof(Prescription.Medication))]
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
