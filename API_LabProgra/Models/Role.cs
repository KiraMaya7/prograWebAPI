using System;
using System.Collections.Generic;

namespace API_LabProgra.Models;

public partial class Role
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
