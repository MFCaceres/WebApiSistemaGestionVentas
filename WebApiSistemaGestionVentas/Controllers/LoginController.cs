using Microsoft.AspNetCore.Mvc;
using WebApiSistemaGestionVentas.Models;
using WebApiSistemaGestionVentas.Repo;


namespace WebApiSistemaGestionVentas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]    
    public class LoginController : Controller
    {
        LoginRepo repository = new LoginRepo();
        
        [HttpPost]
        public ActionResult<Usuario> Login(Usuario usuario)
        {
            try
            {
                bool usuarioExiste = repository.verificarUsuario(usuario);
                return usuarioExiste ? Ok() : NotFound();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
