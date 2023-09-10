namespace NewPogramMVC
{
    class Program
    {
        private static IProductoDao dao = ProductoDAOFactory.CrearProductoDAO();

        public static void Main(string[] args)
        {
            string action;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║     Menú Principal       ║");
                Console.WriteLine("╠══════════════════════════╣");
                Console.WriteLine("║                          ║");
                Console.WriteLine("║ [L]istar Producto        ║");
                Console.WriteLine("║ [C]rear Productos        ║");
                Console.WriteLine("║ [A]ctualizar Producto    ║");
                Console.WriteLine("║ [E]liminar Producto      ║");
                Console.WriteLine("║ [O]btener Descuentos     ║");
                Console.WriteLine("║ [S]alir                  ║");
                Console.WriteLine("║                          ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.ResetColor();

                action = Console.ReadLine()?.ToUpper();

                if (!string.IsNullOrEmpty(action))
                {
                    try
                    {
                        switch (action)
                        {
                            case "L":
                                ListarProductos();
                                break;
                            case "C":
                                CrearProducto();
                                break;
                            case "A":
                                ActualizarProducto();
                                break;
                            case "E":
                                EliminarProducto();
                                break;
                            case "O":
                                ProcesarCalcularDescuento();
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
        private static void ProcesarCalcularDescuento()
        {
            // Solicitar al usuario que ingrese el ID del producto
            Console.Write("Ingrese el ID del producto: ");
            if (int.TryParse(Console.ReadLine(), out int idProducto))
            {
                // Crear un objeto Producto con el ID ingresado
                Producto producto = new Producto { Id = idProducto };

                // Llamar al método para calcular el descuento
                dao.SeleccionCalcularDescuentoProductos(producto);
            }
            else
            {
                Console.WriteLine("ID no válido. Por favor, ingrese un número válido.");
            }
        }

        //private static void ObtenerDescuento(string message)
        //{
        //    try
        //    {
        //        Console.WriteLine(message);
        //    }
        //    catch (DAOException e)
        //    {
        //        Console.WriteLine("Error al obtener el descuento " + e.Message);
        //    }
        //}

        //private static void ObtenerDescuento(string message)
        //{
        //    try
        //    {
        //        Console.WriteLine(message);
        //        if (int.TryParse(Console.ReadLine(), out int productoId))
        //        {
        //            // Call the non-static method on the instance
        //            dao.CalculaDescuentoPorId(productoId);
        //        }
        //        else
        //        {
        //            Console.WriteLine("ID no valido");
        //        }
        //    }
        //    catch (DAOException e)
        //    {
        //        Console.WriteLine("Error al obtener el descuento " + e.Message);
        //    }
        //}

        //private static int InputDescuento(string message)
        //{
        //    int descuento;
        //    while (true)
        //    {
        //        try
        //        {
        //            Console.WriteLine(message);
        //            if (int.TryParse(Console.ReadLine(), out descuento))
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Error de formato de número");
        //            }
        //        }
        //        catch (FormatException)
        //        {
        //            Console.WriteLine("Error de formato de número");
        //        }
        //    }
        //    return descuento;
        //}

        //private static int InputDescuentoProducto()
        //{
        //    int descuento = InputDescuento("Ingrese el ID del producto para calcular el descuento:");
        //    return new int(descuento);
        //}

        private static void CrearProducto()
        {
            try
            {
                Producto producto = InputProducto();
                if (dao.Crear(producto))
                {
                    Console.WriteLine("Registro exitoso: " + producto.Id);
                    Console.WriteLine("\n\nCreado:\n" + producto);
                }
                else
                {
                    Console.WriteLine("Error al registrar el producto.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al registrar el producto: " + e.Message);
            }
        }

        private static void ActualizarProducto()
        {
            int id = InputId();
            Producto producto = dao.ObtenerProductoPorId(id);
            Console.WriteLine("------------Datos originales------------");
            Console.WriteLine(producto);
            Console.WriteLine("Ingrese los nuevos datos");

            string nombre = InputNombre();
            string codigo = InputCodigo();
            double precio = InputPrecio("Ingrese el precio del producto: ");
            int cantidad = InputCantidad("Ingrese las cantidad en stock: ");
            DateTime fecha = InputFecha("Ingrese la fecha de creacion del producto: ");
            string categoria = InputCategoria();

            producto = new Producto(id, nombre, codigo, precio, cantidad, fecha, categoria);
            try
            {
                if (dao.Actualizar(producto))
                {
                    Console.WriteLine("Actualización exitosa");
                }
                else
                {
                    Console.WriteLine("Error al actualizar el producto.");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el producto: " + e.Message);
            }
        }

        private static void EliminarProducto()
        {
            int id = InputId();
            Producto producto = null;

            try
            {
                producto = dao.ObtenerProductoPorId(id);
            }
            catch (DAOException daoe)
            {
                Console.WriteLine("Error: " + daoe.Message);
            }

            if (producto != null && dao.Eliminar(producto))
            {
                Console.WriteLine("Producto eliminado: " + producto.Id);
            }
            else
            {
                Console.WriteLine("Error al eliminar el producto.");
            }
        }

        private static void ListarProductos()
        {
            try
            {
                List<Producto> todosLosProductos = dao.ObtenerTodosLosProductos();
                foreach (Producto producto in todosLosProductos)
                {
                    Console.WriteLine(producto.ToString() + "\n");
                }
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener todos los productos: " + e.Message);
                Console.WriteLine("StackTrace: " + e.StackTrace);
            }
        }

        private static Producto InputProducto()
        {
            string nombre = InputNombre();
            string codigo = InputCodigo();
            double precio = InputPrecio("Ingrese el precio para el producto: ");
            int cantidad = InputCantidad("Ingrese la cantidad en stock: ");
            DateTime fecha = InputFecha("Ingrese la fecha de creacion del producto:");
            string categoria = InputCategoria();
            return new Producto(codigo, codigo, precio, cantidad, fecha, categoria);
        }

        private static int InputId()
        {
            int id;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese un valor entero para el ID del producto: ");
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

        private static string InputNombre()
        {
            return InputString("Ingrese el nombre del producto: ");
        }

        private static string InputCodigo()
        {
            return InputString("Ingrese el codigo del producto: ");
        }

        private static double InputPrecio(string message)
        {
            double precio;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (double.TryParse(Console.ReadLine(), out precio))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de numero");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de numero");
                }
            }
            return precio;
            //return InputString("Ingrese el apellido del producto: ");
        }

        private static int InputCantidad(string message)
        {
            int cantidad;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (int.TryParse(Console.ReadLine(), out cantidad))
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
            return cantidad;
        }

        private static DateTime InputFecha(string message)
        {
            DateTime fecha;
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    if (DateTime.TryParse(Console.ReadLine(), out fecha))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error de formato de fecha. Utilice el formato correcto.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error de formato de fecha. Utilice el formato correcto.");
                }
            }
            return fecha;
        }

        private static string InputCategoria()
        {
            return InputString("Ingrese la categoria del producto: ");
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