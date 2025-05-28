namespace CW_4_s27655.Controllers;

using Microsoft.AspNetCore.Mvc;
using CW_4_s27655.Services;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var details = await _patientService.GetPatientDetailsAsync(id);
        if (details == null)
            return NotFound();

        return Ok(details);
    }
}
