using AspNetApi.Models;
using AspNetApi.Data;
using AspNetApi.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AspNetApi.Services
{
    public class ObtenerUsuariosService(AppDbContext context) : IService<List<Usuario>>
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Usuario>> Handle()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
