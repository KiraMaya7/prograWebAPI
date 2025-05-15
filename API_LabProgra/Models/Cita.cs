using System;
using System.Collections.Generic;

namespace API_LabProgra.Models;

public partial class Cita
{
    public int IdCita { get; set; }

    public int IdDoctor { get; set; }

    public int IdUsuario { get; set; }

    public DateTime FechaHora { get; set; }

    public int EstadoCita { get; set; }

    public string? Notas { get; set; }

    public int? Calificacion { get; set; }

    public string? Comentarios { get; set; }

    public DateTime? FechaCalificacion { get; set; }

    public virtual EstadoCitum EstadoCitaNavigation { get; set; } = null!;

    public virtual Doctore IdDoctorNavigation { get; set; } = null!;

    public virtual Cuentum IdUsuarioNavigation { get; set; } = null!;
}
