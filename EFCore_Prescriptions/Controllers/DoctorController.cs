using EFCore_Prescriptions.DTOs;
using EFCore_Prescriptions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_Prescriptions.Controllers
{
    [Authorize]
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        public readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var results = await _doctorService.GetDoctorAsync();

            if (results.Count <1 || results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpPost("{doctor}")]
        public async Task<IActionResult> post([FromBody] DoctorDTO doctor)
        {

            var result = await _doctorService.AddDoctorAsync(doctor);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> put([FromBody] DoctorDTO doctor)
        {

            var result = await _doctorService.UpdateDoctorAsync(doctor);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> remove(int id)
        {

            var result = await _doctorService.RemoveDoctorAsync(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
