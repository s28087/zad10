using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(Prescription prescription)
    {
        try
        {
            var addedPrescription = await _prescriptionService.AddPrescription(prescription);
            return Ok(addedPrescription);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }
}