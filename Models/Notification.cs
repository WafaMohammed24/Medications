using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medications.Models
{
    public partial class Notification
    {
        [Key]
        [Column("NotificationID")]
        public int NotificationId { get; set; }
        [Column("PatientID")]
        public int? PatientId { get; set; }
        [StringLength(255)]
        public string? Message { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SentDate { get; set; }
        [StringLength(50)]
        public string? Type { get; set; }

        [ForeignKey(nameof(PatientId))]
        [InverseProperty(nameof(User.Notifications))]
        public virtual User? Patient { get; set; }
    }
}
