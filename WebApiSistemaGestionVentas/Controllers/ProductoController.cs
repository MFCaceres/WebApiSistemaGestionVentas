using Microsoft.AspNetCore.Mvc;
using WebApiSistemaGestionVentas.Models;
using WebApiSistemaGestionVentas.Repo;

namespace WebApiSistemaGestionVentas.Controllers
{
        [ApiController]
        [Route("api/v1/[controller]")]
        public class ProductoController : Controller
        {
            private ProductoVendidoRepo repository = new ProductoVendidoRepo();

            [HttpGet]
            public ActionResult<List<Producto>> Get()
            {
                try
                {
                    List<Producto> lista = repository.listarProductos();
                    return Ok(lista);
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
            }

            [HttpPost]
            public IActionResult Post()
            {
                return Ok();
            }

        }
}
