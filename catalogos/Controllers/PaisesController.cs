namespace Paises.Controllers
{
    using dbModel.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly PostgresContext _context;
        public PaisesController(PostgresContext context){
            _context=context;
        }

        [HttpGet]
        public  ActionResult Get()
        {
            
            return Ok("HOLA UN otro UPDATE");
        }
        [HttpGet("pais")]
        public  ActionResult GetPais()
        {
            try
            {
                
            var paises= _context.Paises.ToList();
            return Ok(paises);
            }
            catch (Exception ex)
            {
                
                return NotFound(ex.ToString());
            }
        }
    }
}
