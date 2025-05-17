using API_LabProgra.Models;

namespace API_LabProgra.DTOs
{
    public class AreaMedicaDTO
    {
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public static AreaMedicaDTO FromEntity(AreasMedica entity)
        {
            if (entity == null) return null;

            return new AreaMedicaDTO
            {
                IdArea = entity.IdArea,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion
            };
        }
    }

    public class DoctorDTO
    {
        public int IdDoctor { get; set; }
        public int IdUsuario { get; set; }
        public string LicenciaMedica { get; set; }
        public int IdArea { get; set; }
        public string Especialidad { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellidos}";
        public string Correo { get; set; }
        public string Telefono { get; set; }

        public string AreaNombre { get; set; }

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


                Nombre = doctor.IdUsuarioNavigation?.Nombre,
                Apellidos = doctor.IdUsuarioNavigation?.Apellidos,
                Correo = doctor.IdUsuarioNavigation?.Correo,
                Telefono = doctor.IdUsuarioNavigation?.Telefono,

                AreaNombre = doctor.IdAreaNavigation?.Nombre
            };
        }
    }


    public class DoctorDetalleDTO : DoctorDTO
    {

        public int? Edad { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }


        public string AreaDescripcion { get; set; }


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


                Nombre = doctor.IdUsuarioNavigation?.Nombre,
                Apellidos = doctor.IdUsuarioNavigation?.Apellidos,
                Correo = doctor.IdUsuarioNavigation?.Correo,
                Telefono = doctor.IdUsuarioNavigation?.Telefono,


                Edad = doctor.IdUsuarioNavigation?.Edad,
                Direccion = doctor.IdUsuarioNavigation?.Direccion,
                Estado = doctor.IdUsuarioNavigation?.Estado,
                Ciudad = doctor.IdUsuarioNavigation?.Ciudad,

                AreaNombre = doctor.IdAreaNavigation?.Nombre,
                AreaDescripcion = doctor.IdAreaNavigation?.Descripcion
            };
        }
    }

    public class DoctorCreateUpdateDTO
    {
        public int IdUsuario { get; set; }
        public string LicenciaMedica { get; set; }
        public int IdArea { get; set; }
        public string Especialidad { get; set; }

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


        public Doctore ToEntity(int doctorId)
        {
            var entity = ToEntity();
            entity.IdDoctor = doctorId;
            return entity;
        }
    }
}