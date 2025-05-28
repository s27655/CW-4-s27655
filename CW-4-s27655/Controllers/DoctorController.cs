using CW_4_s27655.DTOs;
using CW_4_s27655.Models;
using CW_4_s27655.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_4_s27655.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        //Import kontekstu
        private readonly IDoctorServices _doctorServices;

        public DoctorController(IDoctorServices doctorServices)
        {
            _doctorServices = doctorServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = await _doctorServices.GetDoctor(id);

            if (doctor == null)
            {
                return NotFound();
            }
            
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDoctor doctor)
        {
            //Jeżeli wszystko zgadza się z modelem
            if (ModelState.IsValid)
            {
                //Nadajemy wartości nowemu doktorowi
                var newDoctor = new Doctor
                {
                    Name = doctor.FirstName,
                    Surname = doctor.LastName,
                    email = doctor.Email
                };
                
                //Wywołanie metody
                var addDoctor = await _doctorServices.AddDoctor(newDoctor);
                //201 wraz z dodanym doktorem + get do nowo powstałego zasobu
                return CreatedAtAction(nameof(Get), new { id = addDoctor.Id }, addDoctor);
            }
            //Zwracamy że nie zgadza się z modelem i co
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id ,AddDoctor doctor)
        {
            //Jeżeli wszystko zgadza się z modelem
            if (ModelState.IsValid)
            {
                //Dajemy nowe dane
                var updateDoctor = await _doctorServices.UpdateDoctor(id, doctor);
                
                //Jeżeli doktor nie istnieje w bazie
                if (updateDoctor == null)
                {
                    return NotFound();
                }
                
                //Zwracamy zaktualizowanego doktora
                return Ok(updateDoctor);
            }
            //Zwracamy że nie zgadza się z modelem i co
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //Próbujemy usunąć doktora
            var doctor = await _doctorServices.DeleteDoctor(id);
            
            //Jeżeli doktor nie istnieje
            if (!doctor)
            {
                return NotFound();
            }
            //Jeżeli udało się usunąć doktora
            return Ok();
        }
    }
}