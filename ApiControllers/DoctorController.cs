using Medications.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medications.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly MedicationsContext context;

        public DoctorController(MedicationsContext context)
        {
            this.context = context;
        }

        [HttpPost("prescription")]
        public IActionResult AddOrUpdatePrescription(Prescription dto)
        {
            var prescription = new Prescription
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                MedicationId= dto.MedicationId,
                Dosage = dto.Dosage,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Frequency = dto.Frequency
            };
            context.Prescriptions.Update(prescription);
            context.SaveChanges();
            return Ok(new { message = "Prescription saved successfully." });
        }

        [HttpGet("patient-prescriptions")]
        public IActionResult GetPatientPrescriptions(int patientId)
        {
            var prescriptions = context.Prescriptions
                .Where(p => p.PatientId == patientId)
                .ToList();
            return Ok(prescriptions);
        }
        [HttpGet("requests")]
        public IActionResult GetRenewalRequests(int doctorId)
        {
            var requests = context.Requests
                .Where(r => r.Prescription.DoctorId == doctorId && r.Status == "Pending")
                .ToList();
            return Ok(requests);
        }

        [HttpPost("request-status")]
        public IActionResult UpdateRequestStatus(int requestId, string status)
        {
            var request = context.Requests.Find(requestId);
            if (request == null) return NotFound();

            request.Status = status;
            context.SaveChanges();
            return Ok(new { message = "Request status updated successfully." });
        }
        [HttpPost("send-notification")]
        public IActionResult SendNotification(int patientId, string message, string type)
        {
            var notification = new Notification
            {
                PatientId = patientId,
                Message = message,
                Type = type
            };
            context.Notifications.Add(notification);
            context.SaveChanges();
            return Ok(new { message = "Notification sent successfully." });
        }
    }
}
