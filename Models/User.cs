using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medications.Models
{
    public partial class User
    {
        public User()
        {
            Notifications = new HashSet<Notification>();
            PrescriptionDoctors = new HashSet<Prescription>();
            PrescriptionPatients = new HashSet<Prescription>();
            Requests = new HashSet<Request>();
        }

        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [StringLength(50)]
        public string? Role { get; set; }
        [StringLength(100)]
        public string? FullName { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(255)]
        public string? Password { get; set; }
        [StringLength(15)]
        public string? PhoneNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [InverseProperty(nameof(Notification.Patient))]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty(nameof(Prescription.Doctor))]
        public virtual ICollection<Prescription> PrescriptionDoctors { get; set; }
        [InverseProperty(nameof(Prescription.Patient))]
        public virtual ICollection<Prescription> PrescriptionPatients { get; set; }
        [InverseProperty(nameof(Request.Patient))]
        public virtual ICollection<Request> Requests { get; set; }
    }
}
