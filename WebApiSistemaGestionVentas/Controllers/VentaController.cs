using Microsoft.AspNetCore.Mvc;
using WebApiSistemaGestionVentas.Models;
using WebApiSistemaGestionVentas.Repo;

namespace WebApiSistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VentaController : Controller
    {
        private VentaRepo repository = new VentaRepo();

        [HttpGet]
        public ActionResult <List<Venta>> Get() 
        {
            try
            {
                List<Venta> lista = repository.listarVenta();
                return Ok(lista);
            }
            catch(Exception ex) 
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Venta venta, ProductoVendido productoVendido)
        {
            try
            {
                Venta CargarVenta = repository.CargarVenta(venta, productoVendido);
                return StatusCode(StatusCodes.Status201Created, CargarVenta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

    }
}
