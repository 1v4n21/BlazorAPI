using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Departamento
{
    //Clase Departamento en base a EntityFramework con la BD

    public int IdDepartamento { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
