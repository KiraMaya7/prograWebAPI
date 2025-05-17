using API_LabProgra.Models;

namespace API_LabProgra.DTOS
{
    // DTO para mostrar datos básicos de doctor
    public class DoctorDTO
    {
        public int IdDoctor { get; set; }
        public int IdUsuario { get; set; }
        public string LicenciaMedica { get; set; }
        public int IdArea { get; set; }
        public string Especialidad { get; set; }

        // Datos del usuario (sin contraseña)
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellidos}";
        public string Correo { get; set; }
        public string Telefono { get; set; }

        // Datos del área
        public string AreaNombre { get; set; }

        // Constructor para mapear desde la entidad
        public static DoctorDTO FromEntity(Doctore doctor)
        {
            if (doctor == null) return null;

            return new DoctorDTO
            {
                IdDoctor = doctor.IdDoctor,
                IdUsuario = doctor.IdUsuario,
                LicenciaMedica = doctor.LicenciaMedica,
                IdArea = doctor.IdArea,
                Especialidad = doctor.Especialidad,

                // Mapear desde el usuario si está disponible
                Nombre = doctor.IdUsuarioNavigation?.Nombre,
                Apellidos = doctor.IdUsuarioNavigation?.Apellidos,
                Correo = doctor.IdUsuarioNavigation?.Correo,
                Telefono = doctor.IdUsuarioNavigation?.Telefono,

                // Mapear desde el área si está disponible
                AreaNombre = doctor.IdAreaNavigation?.Nombre
            };
        }
    }

    // DTO para datos detallados del doctor (admin)
    public class DoctorDetalleDTO : DoctorDTO
    {
        // Datos adicionales que solo el admin debería ver
        public int? Edad { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }

        // Descripción del área
        public string AreaDescripcion { get; set; }

        // Nueva función estática para mapear desde entidad
        public static new DoctorDetalleDTO FromEntity(Doctore doctor)
        {
            if (doctor == null) return null;

            return new DoctorDetalleDTO
            {
                IdDoctor = doctor.IdDoctor,
                IdUsuario = doctor.IdUsuario,
                LicenciaMedica = doctor.LicenciaMedica,
                IdArea = doctor.IdArea,
                Especialidad = doctor.Especialidad,

                // Mapear desde el usuario si está disponible
                Nombre = doctor.IdUsuarioNavigation?.Nombre,
                Apellidos = doctor.IdUsuarioNavigation?.Apellidos,
                Correo = doctor.IdUsuarioNavigation?.Correo,
                Telefono = doctor.IdUsuarioNavigation?.Telefono,

                // Datos adicionales
                Edad = doctor.IdUsuarioNavigation?.Edad,
                Direccion = doctor.IdUsuarioNavigation?.Direccion,
                Estado = doctor.IdUsuarioNavigation?.Estado,
                Ciudad = doctor.IdUsuarioNavigation?.Ciudad,

                // Mapear desde el área si está disponible
                AreaNombre = doctor.IdAreaNavigation?.Nombre,
                AreaDescripcion = doctor.IdAreaNavigation?.Descripcion
            };
        }
    }

    // DTO para crear o actualizar doctor (admin)
    public class DoctorCreateUpdateDTO
    {
        public int IdUsuario { get; set; }
        public string LicenciaMedica { get; set; }
        public int IdArea { get; set; }
        public string Especialidad { get; set; }

        // Convertir a entidad
        public Doctore ToEntity()
        {
            return new Doctore
            {
                IdUsuario = IdUsuario,
                LicenciaMedica = LicenciaMedica,
                IdArea = IdArea,
                Especialidad = Especialidad
            };
        }

        // Para actualizaciones
        public Doctore ToEntity(int doctorId)
        {
            var entity = ToEntity();
            entity.IdDoctor = doctorId;
            return entity;
        }
    }
}