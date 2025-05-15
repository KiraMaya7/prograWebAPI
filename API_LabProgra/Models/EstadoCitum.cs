using System;
using System.Collections.Generic;

namespace API_LabProgra.Models;

public partial class EstadoCitum
{
    public int IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
