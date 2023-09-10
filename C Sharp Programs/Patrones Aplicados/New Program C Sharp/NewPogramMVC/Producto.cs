using Org.BouncyCastle.Asn1.Mozilla;
using System.Globalization;
using System.Text;

namespace NewPogramMVC
{
    public class Producto
    {
        //public int Id { get; set; }
        //public string Cedula { get; set; }
        //public string Apellido { get; set; }
        //public string Nombre { get; set; }
        //public double Horas { get; set; }
        //public double Sueldo { get; set; }

        public int Id { get; set; }
        public string NombreDelProducto { get; set; }
        public string CodigoDelProducto { get; set; }
        public double PrecioDelProducto { get; set; }
        public int CantidadDelProducto { get; set; }
        public DateTime FechaDeCreacionDelProducto { get; set; }
        public string CategoriaDelProducto { get; set; }

        public Producto()
        {
        }

        public Producto(int id, string nombreDelProducto, string codigoDelProducto, double precioDelProducto, int cantidadDelProducto, DateTime fechaDeCreacionDelProducto, string categoriaDelProducto)
        {
            Id = id;
            NombreDelProducto = nombreDelProducto;
            CodigoDelProducto = codigoDelProducto;
            PrecioDelProducto = precioDelProducto;
            CantidadDelProducto = cantidadDelProducto;
            FechaDeCreacionDelProducto = fechaDeCreacionDelProducto;
            CategoriaDelProducto = categoriaDelProducto;
        }

        public Producto(string nombreDelProducto, string codigoDelProducto, double precioDelProducto, int cantidadDelProducto, DateTime fechaDeCreacionDelProducto, string categoriaDelProducto)
        {
            NombreDelProducto = nombreDelProducto;
            CodigoDelProducto = codigoDelProducto;
            PrecioDelProducto = precioDelProducto;
            CantidadDelProducto = cantidadDelProducto;
            FechaDeCreacionDelProducto = fechaDeCreacionDelProducto;
            CategoriaDelProducto = categoriaDelProducto; 
        }

        //public Producto(string cedula, string apellido, string nombre, double horas, double sueldo)
        //{
        //    Cedula = cedula;
        //    Apellido = apellido;
        //    Nombre = nombre;
        //    Horas = horas;
        //    Sueldo = sueldo;
        //}


        public override string ToString()
        {
            string productoInfo = $"ID: {Id}\nNombre del producto: {NombreDelProducto}\nCodigo del producto: {CodigoDelProducto}\nPrecio del producto: {PrecioDelProducto}\nCantidad en stock: {CantidadDelProducto}\nFecha de creacion del producto: {FechaDeCreacionDelProducto}\nCategoria del producto: {CategoriaDelProducto}";

            // Obtener el ancho máximo de línea en el texto
            int lineaMaxima = productoInfo.Split('\n').Max(line => line.Length);

            // Ajustar el ancho de la consola según la longitud máxima de línea
            int consolaAnchoMinimo = lineaMaxima + 4; // +4 para el espacio de los bordes laterales
            int consolaAltoMinimo = productoInfo.Split('\n').Length + 4; // +4 para el espacio de los bordes superior e inferior

            if (Console.WindowWidth < consolaAnchoMinimo)
            {
                Console.WindowWidth = consolaAnchoMinimo;
            }

            if (Console.WindowHeight < consolaAltoMinimo)
            {
                Console.WindowHeight = consolaAltoMinimo;
            }

            // Crear el borde superior en ASCII
            string bordeSuperior = $"╔{new string('═', lineaMaxima + 2)}╗";

            // Crear el borde inferior en ASCII
            string bordeInferior = $"╚{new string('═', lineaMaxima + 2)}╝";

            StringBuilder resultado = new StringBuilder();

            // Agregar el borde superior
            resultado.AppendLine(bordeSuperior);

            // Agregar el contenido con bordes laterales
            foreach (string linea in productoInfo.Split('\n'))
            {
                string lineaConBordes = String.Format("║ {0,-" + lineaMaxima + "} ║", linea); // Añadir bordes laterales
                resultado.AppendLine(lineaConBordes);
            }

            // Agregar el borde inferior
            resultado.AppendLine(bordeInferior);

            return resultado.ToString();
        }
        //public override string ToString()
        //{
        //    return $"ID: {Id}\nNombre del producto: {NombreDelProducto}\nCodigo del producto: {CodigoDelProducto}\nPrecio del producto: {PrecioDelProducto}\nCantidad en stock: {CantidadDelProducto}\nFecha de creacion del producto: {FechaDeCreacionDelProducto}\nCategoria del producto: {CategoriaDelProducto}";
        //}
        //public override string ToString()
        //{
        //    return $"ID: {Id}\nCédula: {Cedula}\nApellido: {Apellido}\nNombre: {Nombre}\nHoras: {Horas}\nSueldo: {Sueldo}";
        //}
    }
}
