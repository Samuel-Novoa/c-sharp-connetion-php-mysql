using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppArquiSoftDao02
{
    public class ControllerEmpleado
    {
        private ViewEmpleado vista = new ViewEmpleado();
        private IEmpleadoDao dao;

        public ControllerEmpleado(IEmpleadoDao dao)
        {
            this.dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public bool RegistrarEmpleado(Empleado empleado)
        {
            try
            {
                return dao.Registrar(empleado);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al registrar el empleado: " + e.Message);
                return false;
            }
        }

        public bool ActualizarEmpleado(Empleado empleado)
        {
            try
            {
                return dao.Actualizar(empleado);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el empleado: " + e.Message);
                return false;
            }
        }

        public bool EliminarEmpleado(Empleado empleado)
        {
            try
            {
                return dao.Eliminar(empleado);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al eliminar el empleado: " + e.Message);
                return false;
            }
        }

        public void VerEmpleados()
        {
            try
            {
                List<Empleado> empleados = dao.Obtener();
                vista.VerEmpleados(empleados);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener los empleados: " + e.Message);
            }
        }
    }
}
