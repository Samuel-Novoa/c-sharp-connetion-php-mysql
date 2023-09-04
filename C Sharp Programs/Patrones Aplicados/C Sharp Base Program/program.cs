using System;
using System.Collections.Generic;

namespace ConsoleAppArquiSoftDao02
{
    class Program
    {
        /*static void Main()
        {
            try
            {
                Conexion conexion = Conexion.Instance;
                MySqlConnection connection = conexion.AbrirConexion();

                // Realiza operaciones con la base de datos
                Console.WriteLine("Conexión exitosa a la base de datos");

                // Aquí puedes agregar las operaciones específicas de la base de datos que necesitas realizar

                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
            }
        }*/
        private static IEmpleadoDao dao = EmpleadoDAOFactory.CrearEmpleadoDAO();

        public static void Main(string[] args)
        {
            string action;

            while (true)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("[L]istar | [R]egistrar | [A]ctualizar | [E]liminar | [S]alir: ");
                action = Console.ReadLine()?.ToUpper();

                if (!string.IsNullOrEmpty(action))
                {
                    try
                    {
                        switch (action)
                        {
                            case "L":
                                ListarEmpleados();
                                break;
                            case "R":
                                RegistrarEmpleado();
                                break;
                            case "A":
                                ActualizarEmpleado();
                                break;
                            case "E":
                                EliminarEmpleado();
                                break;
                            case "S":
                                return;
                            default:
                                Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                                break;
                        }
                    }
                    catch (DAOException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }
        }

        private static void RegistrarEmpleado()
        {
            try
            {
                Empleado empleado = InputEmpleado();
                if (dao.Registrar(empleado))
                {
                    Console.WriteLine("Registro exitoso: " + empleado.Id);
                    Console.WriteLine("\n\nCreado: " + empleado);
                }
                else
                {
                    Console.WriteLine("Error al registrar el empleado.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al registrar el empleado: " + e.Message);
            }
        }

        private static void ActualizarEmpleado()
        {
            int id = InputId();
            Empleado empleado = dao.ObtenerEmpleadoPorId(id);
            Console.WriteLine("------------Datos originales------------");
            Console.WriteLine(empleado);
            Console.WriteLine("Ingrese los nuevos datos");

            string cedula = InputCedula();
            string nombre = InputNombre();
            string apellido = InputApellido();
            double horas = InputHoras("Ingrese las horas trabajadas: ");
            double sueldo = InputSueldo("Ingrese el sueldo: ");

            empleado = new Empleado(id, cedula, nombre, apellido, horas, sueldo);
            try
            {
                if (dao.Actualizar(empleado))
                {
                    Console.WriteLine("Actualización exitosa");
                }
                else
                {
                    Console.WriteLine("Error al actualizar el empleado.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el empleado: " + e.Message);
            }
        }

        private static void EliminarEmpleado()
        {
            int id = InputId();
            Empleado empleado = null;

            try
            {
                empleado = dao.ObtenerEmpleadoPorId(id);
            }
            catch (DAOException daoe)
            {
                Console.WriteLine("Error: " + daoe.Message);
            }

            if (empleado != null && dao.Eliminar(empleado))
            {
                Console.WriteLine("Empleado eliminado: " + empleado.Id);
            }
            else
            {
                Console.WriteLine("Error al eliminar el empleado.");
            }
        }

        private static void ListarEmpleados()
        {
            try
            {
                List<Empleado> todosLosEmpleados = dao.ObtenerTodosLosEmpleados();
                foreach (Empleado empleado in todosLosEmpleados)
                {
                    Console.WriteLine(empleado.ToString() + "\n");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener todos los empleados: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
            }
        }

        private static Empleado InputEmpleado()
        {
            string cedula = InputCedula();
            string nombre = InputNombre();
            string apellido = InputApellido();
            double horas = InputHoras("Ingrese las horas trabajadas: ");
            double sueldo = InputSueldo("Ingrese el sueldo: ");
            return new Empleado(cedula, nombre, apellido, horas, sueldo);
        }

        private static int InputId()
        {
            int id;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese un valor entero para el ID del empleado: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return id;
        }

        private static string InputCedula()
        {
            return InputString("Ingrese el número de cédula del empleado: ");
        }

        private static string InputNombre()
        {
            return InputString("Ingrese el nombre del empleado: ");
        }

        private static string InputApellido()
        {
            return InputString("Ingrese el apellido del empleado: ");
        }

        private static double InputHoras(string message)
        {
            double horas;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (double.TryParse(Console.ReadLine(), out horas))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return horas;
        }

        private static double InputSueldo(string message)
        {
            double sueldo;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (double.TryParse(Console.ReadLine(), out sueldo))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de número");
                }
            }
            return sueldo;
        }

        private static string InputString(string message)
        {
            string s;
            while (true)
            {
                Console.WriteLine(message);
                s = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(s) && s.Length >= 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("La longitud de la cadena debe ser >= 2");
                }
            }
            return s;
        }
    }
}
