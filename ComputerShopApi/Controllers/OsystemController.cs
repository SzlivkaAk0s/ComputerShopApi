using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopApi.Controllers
{
    [Route("osystems")]
    [ApiController]
    public class OsystemController : ControllerBase
    {
        public ComputerShopDBContext _context = new();

        [HttpPost("addOsystem")]
        public object AddNewOsystem([FromBody]AddNewOsystemDTO addNewOsystemDTO)
        {
            var osystem = new Osystem
            {
                Id = Guid.NewGuid(),
                Name = addNewOsystemDTO.Name,
                Version = addNewOsystemDTO.Version,
                RegisterTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            if (osystem != null) 
            { 
                _context.Osystems.Add(osystem); 
                _context.SaveChanges();
                return StatusCode(201, new { message = "Sikeres felvétel", result = osystem }); 
            }
            return StatusCode(400, new { message = "Sikertelen felvétel", result = osystem });
        }

        [HttpGet("allOsystem")]
        public object GetAllOsystem()
        {
            var result = _context.Osystems.ToList();
            Exception e = new();
            if (result.Count > 0)
            {
                return StatusCode(200, new { message = "Sikeres lekérdezés", result = result });
            }
            else if (result.Count < 1)
            {
                return StatusCode(200, new { message = "Üres tábla", result = result });
            }
            else
            {
                return StatusCode(400, new { message = "Hiba a lekérdezésben", result = e.Message });
            }
        }

        [HttpPut("updateOsystem")]
        public object UpdateOsystem([FromQuery]Guid id, [FromBody]UpdateOsystemDTO updateOsystemDTO)
        {
            var osystem = _context.Osystems.FirstOrDefault(os => os.Id == id);

            if(osystem != null)
            {
                osystem.Name = updateOsystemDTO.Name;
                osystem.Version = updateOsystemDTO.Version;
                osystem.UpdateTime = DateTime.Now;

                _context.Osystems.Update(osystem);
                _context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres frissítés", result = osystem });
            }
            else
            {
                return StatusCode(404, new { message = "Nincs ilyen ID-vel osystem", result = osystem });
            }
        }

        [HttpDelete("deleteOsystem")]
        public object DeleteOsystem([FromQuery] Guid id)
        {
            var osystem = _context.Osystems.SingleOrDefault(os => os.Id == id);
            Exception e = new();

            if(osystem != null)
            {
                _context.Osystems.Remove(osystem);
                _context.SaveChanges();

                return StatusCode(200, new { message = "Sikeres törlés", result = osystem });
            }
        }
    }
}
