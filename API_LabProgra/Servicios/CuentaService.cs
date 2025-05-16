using API_LabProgra.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API_LabProgra.Servicios
{
    public class CuentaService : ICuentaService
    {
        private readonly SistemaMedicoContext _context;
        private readonly DbSet<Cuentum> _dbSet;

        public CuentaService(SistemaMedicoContext context)
        {
            _context = context;
            _dbSet = context.Set<Cuentum>();
        }

        public async Task<List<Cuentum>> Alluser()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<int> AddUser(Cuentum modelo)
        {
            try
            {
                _dbSet.Add(modelo);
                await _context.SaveChangesAsync();
                return modelo.IdUsuario; 
            }
            catch (Exception)
            {
                return 0; 
            }
        }

        public async Task<int> UpdateUser(Cuentum modelo)
        {
            try
            {
                var usuarioExistente = await _dbSet.FindAsync(modelo.IdUsuario);

                if (usuarioExistente == null)
                    return 0;

                _context.Entry(usuarioExistente).CurrentValues.SetValues(modelo);

                if (string.IsNullOrEmpty(modelo.Contraseña))
                {
                    _context.Entry(usuarioExistente).Property(x => x.Contraseña).IsModified = false;
                }

                await _context.SaveChangesAsync();
                return modelo.IdUsuario;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<int> DeleteUser(int id)
        {
            try
            {
                var usuario = await _dbSet.FindAsync(id);

                if (usuario == null)
                    return 0;

                _dbSet.Remove(usuario);
                await _context.SaveChangesAsync();
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<Cuentum> GetUserById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Cuentum> GetUserByUsername(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Usuario == username);
        }

        public async Task<Cuentum> ValidateUser(string username, string password)
        {
            var usuario = await _dbSet.FirstOrDefaultAsync(u => u.Usuario == username);

            if (usuario == null)
                return null;
            bool passwordIsValid = VerifyPassword(password, usuario.Contraseña);

            if (!passwordIsValid)
                return null;

            return usuario;
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            string hashedInput = Convert.ToBase64String(
                System.Security.Cryptography.SHA256.HashData(
                    System.Text.Encoding.UTF8.GetBytes(inputPassword)
                )
            );

            return hashedInput == storedPassword;
        }

        public async Task<List<Cuentum>> GetUsersByRole(int rolId)
        {
            return await _dbSet.Where(u => u.Rol == rolId).ToListAsync();
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _dbSet.AnyAsync(u => u.Usuario == username);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _dbSet.AnyAsync(u => u.Correo == email);
        }
    }
}