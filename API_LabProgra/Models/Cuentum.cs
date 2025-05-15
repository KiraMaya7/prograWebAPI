using System;
using System.Collections.Generic;

namespace API_LabProgra.Models;

public partial class Cuentum
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public int? Edad { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Estado { get; set; }

    public string? Ciudad { get; set; }

    public string Correo { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int Rol { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Doctore> Doctores { get; set; } = new List<Doctore>();

    public virtual ICollection<HistorialMedico> HistorialMedicos { get; set; } = new List<HistorialMedico>();

    public virtual Role RolNavigation { get; set; } = null!;
}
