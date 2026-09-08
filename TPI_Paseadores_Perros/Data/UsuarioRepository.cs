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

        public async Task<Usuario?> GetByEmailAndPassAsync(string email, string contrasena)
        {
            // Va a buscar en la tabla general de Usuarios. 
            // Si el mail y la contraseña coinciden, te devuelve el usuario. Si no, devuelve null.
            return await _context.Usuarios
                                 .FirstOrDefaultAsync(u => u.Email == email && u.Contrasena == contrasena);
        }
    }
}
