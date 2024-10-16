using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medications.Models
{
    public partial class Request
    {
        [Key]
        [Column("RequestID")]
        public int RequestId { get; set; }
        [Column("PatientID")]
        public int? PatientId { get; set; }
        [Column("PrescriptionID")]
        public int? PrescriptionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequestDate { get; set; }
        [StringLength(50)]
        public string? Status { get; set; }

        [ForeignKey(nameof(PatientId))]
        [InverseProperty(nameof(User.Requests))]
        public virtual User? Patient { get; set; }
        [ForeignKey(nameof(PrescriptionId))]
        [InverseProperty("Requests")]
        public virtual Prescription? Prescription { get; set; }
    }
}
