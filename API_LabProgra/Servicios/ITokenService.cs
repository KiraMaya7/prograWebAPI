using API_LabProgra.Models;

namespace API_LabProgra.Servicios
{
    public interface ITokenService
    {
        string GenerateToken(Cuentum user);
    }
}
