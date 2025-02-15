using AspNetApi.Models;
using AspNetApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController(CrearUsuarioService crearUsuarioService, ObtenerUsuariosService obtenerUsuariosService) : ControllerBase
    {
        private readonly CrearUsuarioService _crearUsuarioService = crearUsuarioService;
        private readonly ObtenerUsuariosService _obtenerUsuariosService = obtenerUsuariosService;

        // Endpoint para obtener todos los usuarios
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Usuarios()
        {
            var response = await _obtenerUsuariosService.Handle();
            return Ok(response);
        }

        // Endpoint para guardar un nuevo usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> CrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Usuario no puede ser nulo");
            }

            var response = await _crearUsuarioService.Handle(usuario);
            return CreatedAtAction(nameof(Usuarios), new { id = response.Id }, response);
        }
    }
}
