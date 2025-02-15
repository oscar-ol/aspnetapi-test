using AspNetApi.Models;
using AspNetApi.Data;
using System.Threading.Tasks;
using AspNetApi.Interfaces;

namespace AspNetApi.Services
{
    public class CrearUsuarioService(AppDbContext context) : IService<Usuario, Usuario>
    {
        private readonly AppDbContext _context = context;

        public async Task<Usuario> Handle(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
