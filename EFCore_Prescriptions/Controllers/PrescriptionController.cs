using EFCore_Prescriptions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    
namespace EFCore_Prescriptions.Controllers
{
    [Authorize]
    [Route("api/prescriptions")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {

        public readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> get(int id)
        {

            var result = await _prescriptionService.GetPrescriptionAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
