using CW_4_s27655.DTOs;
using CW_4_s27655.Models;
using CW_4_s27655.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_4_s27655.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;
    
    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] AddPrescriptionDto prescriptionDto)
    {
        try
        {
            var id = await _prescriptionService.AddPrescriptionAsync(prescriptionDto);
            return CreatedAtAction(nameof(CreatePrescription), new { id }, new { prescriptionId = id });
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}