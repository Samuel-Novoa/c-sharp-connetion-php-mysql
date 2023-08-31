using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ConsoleAppArqSoft01
{
	class Program
	{
		static void Main(string[] args)
		{
			string connectionString = "server=localhost;user=root;password=;database=sampledb;";
			MySqlConnection connection = new MySqlConnection(connectionString);

			int numeroEmpleados;
			string cedula, apellido, nombre;
			double horas, sueldo;
			double total = 0;

			Console.WriteLine("Digite el numero de empleados: ");
			numeroEmpleados = Int32.Parse(Console.ReadLine());

			for (int i = 0; i < numeroEmpleados; i++)
			{
				Console.WriteLine("Digite la cedula del empleado: ");
				cedula = Console.ReadLine();
				Console.WriteLine("Digite el nombre del empleado: ");
				nombre = Console.ReadLine();
				Console.WriteLine("Digite el apellido del empleado: ");
				apellido = Console.ReadLine();
				Console.WriteLine("Digite el numero de horas trabajadas: ");
				horas = Int32.Parse(Console.ReadLine());
				Console.WriteLine("Digite el sueldo por hora del empleado: ");
				sueldo = Int32.Parse(Console.ReadLine());

				string insertQuery = $"INSERT INTO empleados (cedula, nombre, apellido, horas_trabajadas, sueldo_por_hora) VALUES ('{cedula}', '{nombre}', '{apellido}', {horas}, {sueldo})";

				MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
				connection.Open();
				insertCommand.ExecuteNonQuery();
				connection.Close();
			}

			// Menú de opciones
			while (true)
			{
				Console.WriteLine("\nMENU DE OPCIONES:");
				Console.WriteLine("1. Actualizar empleado");
				Console.WriteLine("2. Eliminar empleado");
				Console.WriteLine("3. Listar empleados");
				Console.WriteLine("4. Calcular nómina total");
				Console.WriteLine("5. Salir");
				Console.Write("Seleccione una opción: ");
				int opcion = Int32.Parse(Console.ReadLine());

				switch (opcion)
				{
					case 1:
						Console.Write("Digite la cedula del empleado a actualizar: ");
						string cedulaActualizar = Console.ReadLine();
						Console.Write("Nuevo Nombre: ");
						string nuevoNombre = Console.ReadLine();
						Console.Write("Nuevo Apellido: ");
						string nuevoApellido = Console.ReadLine();
						Console.Write("Nuevas horas trabajadas: ");
						double nuevasHoras = Double.Parse(Console.ReadLine());
						Console.Write("Nuevo sueldo por hora: ");
						double nuevoSueldo = Double.Parse(Console.ReadLine());
						ActualizarEmpleado(cedulaActualizar, nuevoNombre, nuevoApellido, nuevasHoras, nuevoSueldo, connection);
						break;
					case 2:
						Console.Write("Digite la cedula del empleado a eliminar: ");
						string cedulaEliminar = Console.ReadLine();
						EliminarEmpleado(cedulaEliminar, connection);
						break;
					case 3:
						ListarEmpleados(connection);
						break;
					case 4:
						CalcularNomina(connection);
						break;
					case 5:
						Console.WriteLine("Saliendo del programa.");
						return;
					default:
						Console.WriteLine("Opción no válida. Seleccione una opción válida del menú.");
						break;
				}
			}
		}

		// Función para actualizar un empleado por su cédula
		static void ActualizarEmpleado(string cedula, string nuevoNombre, string nuevoApellido, double nuevasHoras, double nuevoSueldo, MySqlConnection connection)
		{
			string updateQuery = $"UPDATE empleados SET nombre = '{nuevoNombre}', apellido = '{nuevoApellido}', horas_trabajadas = {nuevasHoras}, sueldo_por_hora = {nuevoSueldo} WHERE cedula = '{cedula}'";

			MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
			connection.Open();
			updateCommand.ExecuteNonQuery();
			connection.Close();

			Console.WriteLine("Empleado actualizado exitosamente.");
		}

		// Función para eliminar un empleado por su cédula
		static void EliminarEmpleado(string cedula, MySqlConnection connection)
		{
			string deleteQuery = $"DELETE FROM empleados WHERE cedula = '{cedula}'";

			MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
			connection.Open();
			deleteCommand.ExecuteNonQuery();
			connection.Close();

			Console.WriteLine("Empleado eliminado exitosamente.");
		}

		static void CalcularNomina(MySqlConnection connection)
		{
			string selectQuery = "SELECT sueldo_por_hora, horas_trabajadas FROM empleados";
			MySqlCommand command = new MySqlCommand(selectQuery, connection);
			connection.Open();

			using (MySqlDataReader reader = command.ExecuteReader())
			{
				double totalNomina = 0;
				while (reader.Read())
				{
					double sueldoPorHora = reader.GetDouble("sueldo_por_hora");
					double horasTrabajadas = reader.GetDouble("horas_trabajadas");
					totalNomina += sueldoPorHora * horasTrabajadas;
				}

				Console.WriteLine("La nómina total es: " + totalNomina);
			}

			connection.Close();
		}

		// Función para listar los empleados
		static void ListarEmpleados(MySqlConnection connection)
		{
			string selectQuery = "SELECT cedula, nombre, apellido, horas_trabajadas, sueldo_por_hora FROM empleados";
			MySqlCommand command = new MySqlCommand(selectQuery, connection);
			connection.Open();

			using (MySqlDataReader reader = command.ExecuteReader())
			{
				Console.WriteLine("Lista de empleados:");
				while (reader.Read())
				{
					string cedula = reader.GetString("cedula");
					string nombre = reader.GetString("nombre");
					string apellido = reader.GetString("apellido");
					double horas = reader.GetDouble("horas_trabajadas");
					double sueldo = reader.GetDouble("sueldo_por_hora"); ;
					Console.WriteLine($"Cédula: {cedula}, Nombre: {nombre}, Apellido: {apellido}, Horas: {horas}, Sueldo: {sueldo}");
				}
			}

			connection.Close();
		}
	}
}
