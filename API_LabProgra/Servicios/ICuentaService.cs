using API_LabProgra.Models;

namespace API_LabProgra.Servicios
{
    public interface ICuentaService
    {
        Task<List<Cuentum>> Alluser();
        Task<int> AddUser(Cuentum modelo);
        Task<int> UpdateUser(Cuentum modelo);
        Task<int> DeleteUser(int id);
        Task<Cuentum> GetUserById(int id);
        Task<Cuentum> GetUserByUsername(string username);
        Task<Cuentum> ValidateUser(string username, string password);
        Task<List<Cuentum>> GetUsersByRole(int rolId);
        Task<bool> UsernameExists(string username);
        Task<bool> EmailExists(string email);
    }
}
