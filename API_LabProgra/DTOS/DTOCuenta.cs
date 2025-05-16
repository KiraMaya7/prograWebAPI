using API_LabProgra.Models;

namespace API_LabProgra.DTOS
{
    public class DTOCuenta
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int? Edad { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public int Rol { get; set; }


        // Constructor para mapear desde la entidad
        public static DTOCuenta FromEntity(Cuentum cuenta)
        {
            return new DTOCuenta
            {
                IdUsuario = cuenta.IdUsuario,
                Nombre = cuenta.Nombre,
                Apellidos = cuenta.Apellidos,
                Edad = cuenta.Edad,
                Telefono = cuenta.Telefono,
                Direccion = cuenta.Direccion,
                Estado = cuenta.Estado,
                Ciudad = cuenta.Ciudad,
                Correo = cuenta.Correo,
                Usuario = cuenta.Usuario,
                Rol = cuenta.Rol
            };
        }
    }

    // DTO para crear una cuenta
    public class CreateDTOCuenta
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int? Edad { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int Rol { get; set; }

        // Convertir a entidad
        public Cuentum ToEntity()
        {
            return new Cuentum
            {
                Nombre = Nombre,
                Apellidos = Apellidos,
                Edad = Edad,
                Telefono = Telefono,
                Direccion = Direccion,
                Estado = Estado,
                Ciudad = Ciudad,
                Correo = Correo,
                Usuario = Usuario,
                Contraseña = Contraseña,
                Rol = Rol
            };
        }

        internal object? FromEntity(Cuentum cuentaCreada)
        {
            throw new NotImplementedException();
        }
    }

    // DTO para actualizar cuenta
    public class UpdateDTOCuenta
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int? Edad { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int Rol { get; set; }

        // Convertir a entidad
        public Cuentum ToEntity()
        {
            return new Cuentum
            {
                IdUsuario = IdUsuario,
                Nombre = Nombre,
                Apellidos = Apellidos,
                Edad = Edad,
                Telefono = Telefono,
                Direccion = Direccion,
                Estado = Estado,
                Ciudad = Ciudad,
                Correo = Correo,
                Usuario = Usuario,
                Contraseña = Contraseña,
                Rol = Rol
            };
        }
    }

    // Clase para el modelo de login
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    // Clase para la respuesta de login
    public class LoginResponse
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Usuario { get; set; }
        public int Rol { get; set; }
        public string Token { get; set; }
    }

}
