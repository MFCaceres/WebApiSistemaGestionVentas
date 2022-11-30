using Microsoft.AspNetCore.Mvc;
using WebApiSistemaGestionVentas.Models;
using WebApiSistemaGestionVentas.Repo;

namespace WebApiSistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientesController : Controller
    {
        private ClientesRepo repository = new ClientesRepo();

        [HttpGet]
        public ActionResult<List<Clientes>> Get()
        {
            try
            {
                List<Clientes> lista = repository.listarClientes();
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
