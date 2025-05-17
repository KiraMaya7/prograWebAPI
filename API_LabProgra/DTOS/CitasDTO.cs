using API_LabProgra.Models;

namespace API_LabProgra.DTOs
{
 
    public class EstadoCitaDTO
    {
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public static EstadoCitaDTO FromEntity(EstadoCitum entity)
        {
            if (entity == null) return null;

            return new EstadoCitaDTO
            {
                IdEstado = entity.IdEstado,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion
            };
        }
    }


    public class CitaDTO
    {
        public int IdCita { get; set; }
        public int IdDoctor { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaHora { get; set; }
        public int EstadoCita { get; set; }
        public string Notas { get; set; }
        public int? Calificacion { get; set; }
        public string Comentarios { get; set; }
        public DateTime? FechaCalificacion { get; set; }

        public string NombreDoctor { get; set; }
        public string EspecialidadDoctor { get; set; }

        public string NombrePaciente { get; set; }

        public string EstadoNombre { get; set; }

        public static CitaDTO FromEntity(Cita cita)
        {
            if (cita == null) return null;

            return new CitaDTO
            {
                IdCita = cita.IdCita,
                IdDoctor = cita.IdDoctor,
                IdUsuario = cita.IdUsuario,
                FechaHora = cita.FechaHora,
                EstadoCita = cita.EstadoCita,
                Notas = cita.Notas,
                Calificacion = cita.Calificacion,
                Comentarios = cita.Comentarios,
                FechaCalificacion = cita.FechaCalificacion,

                NombreDoctor = cita.IdDoctorNavigation?.IdUsuarioNavigation != null
                    ? $"{cita.IdDoctorNavigation.IdUsuarioNavigation.Nombre} {cita.IdDoctorNavigation.IdUsuarioNavigation.Apellidos}"
                    : null,
                EspecialidadDoctor = cita.IdDoctorNavigation?.Especialidad,

                NombrePaciente = cita.IdUsuarioNavigation != null
                    ? $"{cita.IdUsuarioNavigation.Nombre} {cita.IdUsuarioNavigation.Apellidos}"
                    : null,

                EstadoNombre = cita.EstadoCitaNavigation?.Nombre
            };
        }
    }


    public class CitaCreateDTO
    {
        public int IdDoctor { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaHora { get; set; }
        public int EstadoCita { get; set; } = 1; 
        public string Notas { get; set; }

        public Cita ToEntity()
        {
            return new Cita
            {
                IdDoctor = IdDoctor,
                IdUsuario = IdUsuario,
                FechaHora = FechaHora,
                EstadoCita = EstadoCita,
                Notas = Notas
            };
        }
    }

    public class CitaUpdateDTO
    {
        public int IdCita { get; set; }
        public int IdDoctor { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaHora { get; set; }
        public int EstadoCita { get; set; }
        public string Notas { get; set; }

        public Cita ToEntity()
        {
            return new Cita
            {
                IdCita = IdCita,
                IdDoctor = IdDoctor,
                IdUsuario = IdUsuario,
                FechaHora = FechaHora,
                EstadoCita = EstadoCita,
                Notas = Notas
            };
        }
    }

    public class CalificacionCitaDTO
    {
        public int Calificacion { get; set; }
        public string Comentarios { get; set; }
    }
}