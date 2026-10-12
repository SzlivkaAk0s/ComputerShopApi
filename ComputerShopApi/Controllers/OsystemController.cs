using ComputerShopApi.Models;
using ComputerShopApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
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
    }
}
