using Microsoft.AspNetCore.Mvc;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebProgramalamaProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public AppointmentApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<AppointmentModel> Get()
        {
            return _db.Appointments.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public AppointmentModel Get(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(a => a.Id == id);
            return appointment;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] AppointmentModel value)
        {
            _db.Appointments.Add(value);
            _db.SaveChanges();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AppointmentModel value)
        {
            var appointment=_db.Appointments.FirstOrDefault(x => x.Id == id);
            if(appointment is null)
            {
                return NotFound();
            }
            else
            {
                _db.Appointments.Update(value);
                _db.SaveChanges();
                return Ok();
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            if(appointment is null)
            {
                return NotFound();
            }
            else
            {
                _db.Appointments.Remove(appointment);
                _db.SaveChanges();
                return Ok();
            }
        }
    }
}
