using System;
using System.Collections.Generic;

namespace API_LabProgra.Models;

public partial class HistorialMedico
{
    public int HistorialId { get; set; }

    public int IdUsuario { get; set; }

    public int IdDoctor { get; set; }

    public string? Alergias { get; set; }

    public string? Enfermedades { get; set; }

    public string? ConsumoBebidas { get; set; }

    public string? Habitos { get; set; }

    public string? Cirugias { get; set; }

    public string? Receta { get; set; }

    public string? Diagnostico { get; set; }

    public virtual Doctore IdDoctorNavigation { get; set; } = null!;

    public virtual Cuentum IdUsuarioNavigation { get; set; } = null!;
}
