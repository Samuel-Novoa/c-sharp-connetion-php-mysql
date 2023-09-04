using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppArquiSoftDao02
{
    public interface IEmpleadoDao
    {
        bool Registrar(Empleado empleado);
        List<Empleado> Obtener();
        bool Actualizar(Empleado empleado);
        bool Eliminar(Empleado empleado);
        Empleado ObtenerEmpleadoPorId(int id);
        List<Empleado> ObtenerTodosLosEmpleados();

    }
}