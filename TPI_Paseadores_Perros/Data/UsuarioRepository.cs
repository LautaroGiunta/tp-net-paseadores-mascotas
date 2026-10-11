using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PaseadoresContext _context;

        public UsuarioRepository(PaseadoresContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            // Va a buscar en la tabla general de Usuarios. 
            // Busca el usuario por email; la contraseña se verifica en AuthService
            return await _context.Usuarios
                                 .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
