using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebProgramalamaProje.Data;
using WebProgramalamaProje.Models;
using WebProgramalamaProje.Models.CombinedModels;

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
        public IEnumerable<AppointmentExtraModel> Get()
        {
            var query = from app in _db.Appointments
                        join doc in _db.Doctors on app.DoctorId equals doc.Id 
                        join cli in _db.Clinics on doc.ClinicId equals cli.Id
                        join use in _db.Users on app.ClientId equals use.Id
                        select new AppointmentExtraModel
                        {
                            Id=app.Id,
                            ClientId=app.ClientId,
                            ClientName=use.UserName,
                            DoctorId=app.DoctorId,
                            DoctorName=doc.Name,
                            ClinicName= cli.Name,
                            StartDateTime=app.StartDateTime,
                            EndDateTime=app.EndDateTime
                        };
            return query.ToList();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public AppointmentExtraModel Get(int id)
        {
            var query = from app in _db.Appointments
                        join doc in _db.Doctors on app.DoctorId equals doc.Id
                        join cli in _db.Clinics on doc.ClinicId equals cli.Id
                        join use in _db.Users on app.ClientId equals use.Id
                        where app.Id == id
                        select new AppointmentExtraModel
                        {
                            Id = app.Id,
                            ClientId = app.ClientId,
                            ClientName = use.UserName,
                            DoctorId = app.DoctorId,
                            DoctorName = doc.Name,
                            ClinicName = cli.Name,
                            StartDateTime = app.StartDateTime,
                            EndDateTime = app.EndDateTime
                        };
            return query.FirstOrDefault();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] AppointmentModel value)
        {
            if(value!=null)
            {
                _db.Appointments.Add(value);
                _db.SaveChanges();
                return Ok(value);
            }
            return NotFound();
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
