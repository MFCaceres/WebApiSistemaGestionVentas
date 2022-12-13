using Microsoft.AspNetCore.Mvc;
using WebApiSistemaGestionVentas.Models;
using WebApiSistemaGestionVentas.Repo;

namespace WebApiSistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepo repository = new UsuarioRepo();

        [HttpGet]

        public ActionResult<List<Usuario>> Get()
        {
            try
            {
                List<Usuario> lista = repository.listarUsuario();
                return Ok(lista);

            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        [HttpPost]
        public ActionResult Post()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put(int id, [FromBody] Usuario modificarUsuario)
        {
            try
            {
                Usuario? usuarioActualizado = repository.modificarUsuario(id, modificarUsuario);
                if (usuarioActualizado != null)
                {
                    return Ok(usuarioActualizado);
                }
                else
                {
                    return NotFound("El usuario no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}

