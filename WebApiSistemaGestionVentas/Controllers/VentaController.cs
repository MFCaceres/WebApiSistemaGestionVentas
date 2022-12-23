using Microsoft.AspNetCore.Mvc;
using WebApiSistemaGestionVentas.Models;
using WebApiSistemaGestionVentas.Repo;

namespace WebApiSistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VentaController : Controller
    {
        VentaRepo repository = new VentaRepo();

        [HttpGet]
        public ActionResult<List<Venta>> Get()
        {
            try
            {
                List<Venta> lista = repository.listarVenta();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]

        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                repository.CargarVenta(venta);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            try
            {
                return Ok(repository.ObtenerVenta(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]

        public ActionResult Delete([FromBody] long id)
        {
            try
            {
                bool deleteVenta = repository.EliminarVenta(id);
                if (deleteVenta)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}





