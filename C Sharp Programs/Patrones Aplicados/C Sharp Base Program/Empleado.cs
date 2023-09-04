using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppArquiSoftDao02
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public double Horas { get; set; }
        public double Sueldo { get; set; }

        public Empleado()
        {
        }

        public Empleado(int id, string cedula, string apellido, string nombre, double horas, double sueldo)
        {
            Id = id;
            Cedula = cedula;
            Apellido = apellido;
            Nombre = nombre;
            Horas = horas;
            Sueldo = sueldo;
        }

        public Empleado(string cedula, string apellido, string nombre, double horas, double sueldo)
        {
            Cedula = cedula;
            Apellido = apellido;
            Nombre = nombre;
            Horas = horas;
            Sueldo = sueldo;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nCédula: {Cedula}\nApellido: {Apellido}\nNombre: {Nombre}\nHoras: {Horas}\nSueldo: {Sueldo}";
        }
    }
}

