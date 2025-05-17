using API_LabProgra.Models;

namespace API_LabProgra.DTOs
{

    public class HistorialMedicoDTO
    {
        public int HistorialId { get; set; }
        public int IdUsuario { get; set; }
        public int IdDoctor { get; set; }
        public string Alergias { get; set; }
        public string Enfermedades { get; set; }
        public string ConsumoBebidas { get; set; }
        public string Habitos { get; set; }
        public string Cirugias { get; set; }
        public string Receta { get; set; }
        public string Diagnostico { get; set; }

        public string NombreDoctor { get; set; }
        public string EspecialidadDoctor { get; set; }


        public string NombrePaciente { get; set; }

        public static HistorialMedicoDTO FromEntity(HistorialMedico historial)
        {
            if (historial == null) return null;

            return new HistorialMedicoDTO
            {
                HistorialId = historial.HistorialId,
                IdUsuario = historial.IdUsuario,
                IdDoctor = historial.IdDoctor,
                Alergias = historial.Alergias,
                Enfermedades = historial.Enfermedades,
                ConsumoBebidas = historial.ConsumoBebidas,
                Habitos = historial.Habitos,
                Cirugias = historial.Cirugias,
                Receta = historial.Receta,
                Diagnostico = historial.Diagnostico,

                NombreDoctor = historial.IdDoctorNavigation?.IdUsuarioNavigation != null
                    ? $"{historial.IdDoctorNavigation.IdUsuarioNavigation.Nombre} {historial.IdDoctorNavigation.IdUsuarioNavigation.Apellidos}"
                    : null,
                EspecialidadDoctor = historial.IdDoctorNavigation?.Especialidad,

                NombrePaciente = historial.IdUsuarioNavigation != null
                    ? $"{historial.IdUsuarioNavigation.Nombre} {historial.IdUsuarioNavigation.Apellidos}"
                    : null
            };
        }
    }

    public class HistorialMedicoCreateDTO
    {
        public int IdUsuario { get; set; }
        public int IdDoctor { get; set; }
        public string Alergias { get; set; }
        public string Enfermedades { get; set; }
        public string ConsumoBebidas { get; set; }
        public string Habitos { get; set; }
        public string Cirugias { get; set; }
        public string Receta { get; set; }
        public string Diagnostico { get; set; }

        public HistorialMedico ToEntity()
        {
            return new HistorialMedico
            {
                IdUsuario = IdUsuario,
                IdDoctor = IdDoctor,
                Alergias = Alergias,
                Enfermedades = Enfermedades,
                ConsumoBebidas = ConsumoBebidas,
                Habitos = Habitos,
                Cirugias = Cirugias,
                Receta = Receta,
                Diagnostico = Diagnostico
            };
        }
    }

    public class HistorialMedicoUpdateDTO
    {
        public int HistorialId { get; set; }
        public string Alergias { get; set; }
        public string Enfermedades { get; set; }
        public string ConsumoBebidas { get; set; }
        public string Habitos { get; set; }
        public string Cirugias { get; set; }
        public string Receta { get; set; }
        public string Diagnostico { get; set; }

        public HistorialMedico ToEntity(HistorialMedico original)
        {
            original.Alergias = Alergias ?? original.Alergias;
            original.Enfermedades = Enfermedades ?? original.Enfermedades;
            original.ConsumoBebidas = ConsumoBebidas ?? original.ConsumoBebidas;
            original.Habitos = Habitos ?? original.Habitos;
            original.Cirugias = Cirugias ?? original.Cirugias;
            original.Receta = Receta ?? original.Receta;
            original.Diagnostico = Diagnostico ?? original.Diagnostico;

            return original;
        }
    }

    public class DiagnosticoUpdateDTO
    {
        public string Diagnostico { get; set; }
    }

    public class RecetaUpdateDTO
    {
        public string Receta { get; set; }
    }
}