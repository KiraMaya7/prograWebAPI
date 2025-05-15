using System;
using System.Collections.Generic;

namespace API_LabProgra.Models;

public partial class Doctore
{
    public int IdDoctor { get; set; }

    public int IdUsuario { get; set; }

    public string LicenciaMedica { get; set; } = null!;

    public int IdArea { get; set; }

    public string Especialidad { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<HistorialMedico> HistorialMedicos { get; set; } = new List<HistorialMedico>();

    public virtual AreasMedica IdAreaNavigation { get; set; } = null!;

    public virtual Cuentum IdUsuarioNavigation { get; set; } = null!;
}
