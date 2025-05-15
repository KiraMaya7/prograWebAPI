using System;
using System.Collections.Generic;

namespace API_LabProgra.Models;

public partial class AreasMedica
{
    public int IdArea { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Doctore> Doctores { get; set; } = new List<Doctore>();
}
