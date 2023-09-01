using System;
using System.Data;
using System.Reflection.PortableExecutable;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cmp;

namespace proyectData
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbProductsConnection = "server=localhost;user=root;password=;database=products;";
            MySqlConnection conDB = new MySqlConnection(dbProductsConnection);

            while (true)
            {
                Console.WriteLine("******************************************");
                Console.WriteLine("*                MENU                    *");
                Console.WriteLine("******************************************");
                Console.WriteLine("* 1. Listar producto                     *");
                Console.WriteLine("* 2. Actualizar producto                 *");
                Console.WriteLine("* 3. Eliminar producto                   *");
                Console.WriteLine("* 4. Calcular descuento del producto     *");
                Console.WriteLine("* 5. Agregar producto                    *");
                Console.WriteLine("* 6. Reiniciar auto increment            *");
                Console.WriteLine("* 7. Salir                               *");
                Console.WriteLine("******************************************\n");

                Console.Write("Opción: ");
                int opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        ListarProductos(conDB);
                        break;
                    case 2:
                        Console.WriteLine("Actualizar producto\n");
                        Console.Write("Digite el codigo del producto a actuializar: ");
                        string codProductoActualizar = Console.ReadLine();
                        Console.Write("Nuevo codigo:");
                        string NWcodProducto = Console.ReadLine();
                        Console.Write("Nuevo nombre:");
                        string NWnombreProducto = Console.ReadLine();
                        Console.Write("Nuevo precio:");
                        double NWprecio = double.Parse(Console.ReadLine());
                        
                        ActualizarProductos(codProductoActualizar, NWcodProducto, NWnombreProducto, NWprecio, conDB);
                        break;
                    case 3:
                        Console.WriteLine("Elimanar producto\n");
                        Console.Write("Digite el codigo el producto: ");
                        string productoEliminar = Console.ReadLine();
                        EliminarProducto(productoEliminar, conDB);
                        break;
                    case 4:
                        Console.WriteLine("Calcular el descuento del producto\n");
                        CalcularDescuento(conDB);
                        break;
                    case 5:
                        Console.WriteLine("Agregar productos...");
                        AgragarProductos(conDB);
                        break;
                    case 6:
                        Console.Clear();

                        Console.WriteLine("Reiniciando el ID...");
                        string restartAutoIncrement = "ALERT TABLE product AUTO_INCREMENT = 1";
                        MySqlCommand restarAI = new MySqlCommand(restartAutoIncrement, conDB);
                        Console.WriteLine("Reinicio exitoso...");
                        break;
                    case 7:
                        Console.WriteLine("Saliendo...");
                        return;
                    default:
                        Console.WriteLine("Opcion invalida...");
                        break;
                }
            }
        }

        // Funcion agregar productos
        static void AgragarProductos(MySqlConnection conDB)
        {
            Console.Clear();

            Console.WriteLine("******************************************");
            Console.WriteLine("*           AGREGAR PRODUCTOS            *");
            Console.WriteLine("******************************************\n");

            int cantidadProductos; // No almacenada en la db
            string nombreProducto, codProducto;
            double precio;
            // double total = 0;
            Console.WriteLine("Digite el numero de productos: ");
            cantidadProductos = Int32.Parse(Console.ReadLine());

            cantidadProductos += 1;

            for (int i = 1; i < cantidadProductos; i++)
            {
                Console.Write($"Digite el codigo del producto #{i}: ");
                codProducto = Console.ReadLine();
                Console.Write($"Digite el nombre del producto #{i}: ");
                nombreProducto = Console.ReadLine();
                Console.Write($"Digite el precio del producto #{i}: ");
                precio = Int32.Parse(Console.ReadLine());

                // Funcion agregar productos
                string insertQueryDB = $"INSERT INTO product (nombre_producto, codigo_producto, precio) VALUES ('{nombreProducto}', '{codProducto}', '{precio}')";

                MySqlCommand insertCommand = new MySqlCommand(insertQueryDB, conDB);
                conDB.Open();
                insertCommand.ExecuteNonQuery();
                conDB.Close();
            }
            Console.WriteLine("\n\n");
        }

        // Funcion listar productos
        static void ListarProductos(MySqlConnection conDB)
        {
            Console.Clear();

            Console.WriteLine("******************************************");
            Console.WriteLine("*               LISTANDO                 *");
            Console.WriteLine("******************************************\n");

            string selectQuery = "SELECT * FROM product";

            conDB.Open();

            using (MySqlCommand command = new MySqlCommand(selectQuery, conDB))
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    // Obtener nombres de columnas
                    List<string> columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columnNames.Add(reader.GetName(i));
                    }

                    // Imprimir encabezados de columna con diseño ASCII
                    Console.WriteLine("┌" + new string('─', 20) + "┬" + new string('─', 20) + "┬" + new string('─', 20) + "┬" + new string('─', 20) + "┐");
                    Console.WriteLine("│" + columnNames[0].PadRight(20) + "│" + columnNames[1].PadRight(20) + "│" + columnNames[2].PadRight(20) + "│" + columnNames[3].PadRight(20) + "│");
                    Console.WriteLine("├" + new string('─', 20) + "┼" + new string('─', 20) + "┼" + new string('─', 20) + "┼" + new string('─', 20) + "┤");

                    // Leer y mostrar datos con diseño ASCII
                    while (reader.Read())
                    {
                        List<string> rowData = new List<string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            rowData.Add(reader[i].ToString().PadRight(20));
                        }
                        Console.WriteLine("│" + string.Join("│", rowData) + "│");
                        Console.WriteLine("├" + new string('─', 20) + "┼" + new string('─', 20) + "┼" + new string('─', 20) + "┼" + new string('─', 20) + "┤");
                    }
                }
                else
                {
                    Console.WriteLine("No hay productos para mostrar...");
                }
                Console.WriteLine("\n\n");
            }

            conDB.Close();
        }

        // Funcion actualizar productos
        static void ActualizarProductos(string codProductoActualizar, string NWcodProducto, string NWnombreProducto, double NWprecio, MySqlConnection conDB)
        {
            Console.Clear();

            Console.WriteLine("******************************************");
            Console.WriteLine("*          ACTUALIZAR PRODUCTOS          *");
            Console.WriteLine("******************************************\n");

            string updateQuery = $"UPDATE product SET nombre_producto = '{NWnombreProducto}', codigo_producto = '{NWcodProducto}', precio = '{NWprecio}' WHERE codigo_producto = '{codProductoActualizar}'";

            MySqlCommand updateCommand = new MySqlCommand(updateQuery, conDB);

            conDB.Open();
            updateCommand.ExecuteNonQuery();
            conDB.Close();

            Console.WriteLine("Producto actualizado...");
            Console.WriteLine("\n\n");
        }


        // Funcion elimnar productos
        static void EliminarProducto(string codProducto, MySqlConnection conDB)
        {
            Console.Clear();

            Console.WriteLine("******************************************");
            Console.WriteLine("*           ELIMINAR PRODUCTOS           *");
            Console.WriteLine("******************************************");

            string deleteQuery = $"DELETE FROM product WHERE codigo_producto = '{codProducto}'";

            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, conDB);
            conDB.Open();
            deleteCommand.ExecuteNonQuery();
            conDB.Close();

            Console.WriteLine("El producto se ha borrado...");
            Console.WriteLine("\n\n");
        }

        /* --------------------------------- */

        // Funcion calcular el descuento de un producto basado en su precio
        static void CalcularDescuento(MySqlConnection conDB)
        {
            Console.Clear();
            Console.Clear();

            Console.WriteLine("******************************************");
            Console.WriteLine("*           CALCULAR DESCUENTO           *");
            Console.WriteLine("******************************************\n");

            string selectQuery = "SELECT precio FROM product";
            MySqlCommand command = new MySqlCommand(selectQuery, conDB);
            conDB.Open();

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    double precioTotal = reader.GetDouble("precio");
                    Console.Write("Digite el porcentaje de desuento: ");
                    double decuentoPorcentaje = double.Parse(Console.ReadLine());

                    double descuento = (precioTotal * decuentoPorcentaje) / 100;
                    double precioConDescuento = precioTotal - descuento;

                    Console.WriteLine("Descuento aplicado: $" + descuento);
                    Console.WriteLine("Precio despues del descuento: $" + precioConDescuento);
                }
            }
            Console.WriteLine("\n\n");
        }
    }
}