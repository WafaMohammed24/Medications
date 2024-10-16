using Medications.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medications.ApiControllers
{
   // [Authorize(Roles = "Patient")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        // GET: api/<EventsController>
        private readonly MedicationsContext context;

        public PatientController(MedicationsContext context)
        {
            this.context = context;
        }
        [HttpGet("medications")]
        public IActionResult GetMedications(int patientId)
        {
            var medications = context.Prescriptions
                .Where(p => p.PatientId == patientId)
                .Select(p => new {
                    p.Medication.Name,
                    p.Medication.Dosage,
                    p.StartDate,
                    p.EndDate,
                    p.Frequency
                }).ToList();
            return Ok(medications);
        }

        [HttpPost("request-renewal")]
        public IActionResult RequestRenewal(int patientId, int prescriptionId)
        {
            var request = new Request
            {
                PatientId = patientId,
                PrescriptionId = prescriptionId,
                Status = "Pending"
            };
            context.Requests.Add(request);
            context.SaveChanges();
            return Ok(new { message = "Request added successfully." });
        }
        [HttpGet("notifications")]
        public IActionResult GetNotificationHistory(int patientId)
        {
            var notifications = context.Notifications
                .Where(n => n.PatientId == patientId)
                .ToList();
            return Ok(notifications);
        }
    }

}
