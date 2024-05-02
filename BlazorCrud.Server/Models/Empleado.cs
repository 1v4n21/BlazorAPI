using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Empleado
{
    //Clase Empleado en base a EntityFramework con la BD

    public int IdEmpleado { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public int IdDepartamento { get; set; }

    public int Sueldo { get; set; }

    public DateOnly FechaContrato { get; set; }

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
