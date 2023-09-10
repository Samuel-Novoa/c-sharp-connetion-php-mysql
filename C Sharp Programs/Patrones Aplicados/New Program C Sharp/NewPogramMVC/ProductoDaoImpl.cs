using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace NewPogramMVC
{
    public class ProductoDaoImpl : IProductoDao
    {
        private const string INSERT_QUERY = "INSERT INTO productos (nombre, codigo, precio, cantidad_stock, fecha_creacion, categoria) VALUES (@nombre, @codigo, @precio, @cantidad_stock, @fecha_creacion, @categoria)";
        private const string SELECT_ALL_QUERY = "SELECT * FROM productos ORDER BY ID";
        private const string UPDATE_QUERY = "UPDATE productos SET nombre=@nombre, codigo=@codigo, cantidad_stock=@cantidad_stock, fecha_creacion=@fecha_creacion, categoria=@categoria WHERE ID=@id";
        private const string DELETE_QUERY = "DELETE FROM productos WHERE ID=@id";
        private const string SELECT_BY_ID_QUERY = "SELECT * FROM productos WHERE id=@id";
        private const string SELECT_ALL_PRODUCTOS_QUERY = "SELECT * FROM productos";
        //
        private const string SELECT_PRODUCTO_BY_PRECIO = "SELECT precio FROM productos WHERE ID = @id;";

        private readonly MySqlConnection _connection;

        public ProductoDaoImpl(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public bool Crear(Producto producto)
        {
            bool registrado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(INSERT_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@nombre", producto.NombreDelProducto);
                    cmd.Parameters.AddWithValue("@codigo", producto.CodigoDelProducto);
                    cmd.Parameters.AddWithValue("@precio", producto.PrecioDelProducto);
                    cmd.Parameters.AddWithValue("@cantidad_stock", producto.CantidadDelProducto);
                    cmd.Parameters.AddWithValue("@fecha_creacion", producto.FechaDeCreacionDelProducto);
                    cmd.Parameters.AddWithValue("@categoria", producto.CategoriaDelProducto);
                    cmd.ExecuteNonQuery();

                    producto.Id = (int)cmd.LastInsertedId;

                    registrado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al registrar el producto", ex);
            }
            finally
            {
                _connection.Close();
            }

            return registrado;
        }

        public List<Producto> Obtener()
        {
            List<Producto> listaProductos = new List<Producto>();

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = CrearProductoDesdeDataReader(reader);
                            listaProductos.Add(producto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener los productos", ex);
            }
            finally
            {
                _connection.Close();
            }

            return listaProductos;
        }

        public bool Actualizar(Producto producto)
        {
            bool actualizado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(UPDATE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@nombre", producto.NombreDelProducto);
                    cmd.Parameters.AddWithValue("@codigo", producto.CodigoDelProducto);
                    cmd.Parameters.AddWithValue("@precio", producto.PrecioDelProducto);
                    cmd.Parameters.AddWithValue("@cantidad_stock", producto.CantidadDelProducto);
                    cmd.Parameters.AddWithValue("@fecha_creacion", producto.FechaDeCreacionDelProducto);
                    cmd.Parameters.AddWithValue("@categoria", producto.CategoriaDelProducto);
                    cmd.Parameters.AddWithValue("@id", producto.Id);
                    //cmd.Parameters.AddWithValue("@cedula", producto.Cedula);
                    //cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    //cmd.Parameters.AddWithValue("@apellido", producto.Apellido);
                    //cmd.Parameters.AddWithValue("@horas", producto.Horas);
                    //cmd.Parameters.AddWithValue("@sueldo", producto.Sueldo);
                    //cmd.Parameters.AddWithValue("@id", producto.Id);
                    cmd.ExecuteNonQuery();
                    actualizado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al actualizar el producto", ex);
            }
            finally
            {
                _connection.Close();
            }

            return actualizado;
        }

        public bool Eliminar(Producto producto)
        {
            bool eliminado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(DELETE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", producto.Id);
                    cmd.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al eliminar el producto", ex);
            }
            finally
            {
                _connection.Close();
            }

            return eliminado;
        }

        public Producto ObtenerProductoPorId(int id)
        {
            Producto producto = null;

            try
            {

                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_BY_ID_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = CrearProductoDesdeDataReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener el producto por ID", ex);
            }
            finally
            {
                _connection.Close();
            }

            return producto;
        }

        public List<Producto> ObtenerTodosLosProductos()
        {
            List<Producto> listaProductos = new List<Producto>();

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_PRODUCTOS_QUERY, _connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = CrearProductoDesdeDataReader(reader);
                            listaProductos.Add(producto);
                        }
                    }
                }
            }
            
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener todos los productos", ex);
            }
            finally
            {
                _connection.Close();
            }

            return listaProductos;
        }

        public bool SeleccionCalcularDescuentoProductos(Producto producto)
        {
            bool seleccionCalculo = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_BY_ID_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", producto.Id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Crear el producto desde el resultado de la consulta
                            Producto productoSeleccionado = CrearProductoDesdeDataReader(reader);

                            // Calcular un descuento del 10% en el precio del producto seleccionado
                            double descuento = productoSeleccionado.PrecioDelProducto * 0.10; // 10% de descuento
                            double precioConDescuento = productoSeleccionado.PrecioDelProducto - descuento;

                            // Mostrar el resultado
                            Console.WriteLine($"Producto Seleccionado: {productoSeleccionado.NombreDelProducto}");
                            Console.WriteLine($"Precio Original: ${productoSeleccionado.PrecioDelProducto}");
                            Console.WriteLine($"Aplicando descuento del 10%: ${descuento}");
                            Console.WriteLine($"Precio con Descuento: ${precioConDescuento}");

                            seleccionCalculo = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DAOException("Error al obtener el producto por precio", ex);
            }
            finally
            {
                _connection.Close();
            }

            return seleccionCalculo;
        }

        private Producto CrearProductoDesdeDataReader(MySqlDataReader reader)
        {
            int id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
            string nombre = reader.GetString("nombre");
            string codigo = reader.GetString("codigo");
            double precio = reader.GetDouble("precio");
            int cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad_stock")) ? 0 : reader.GetInt32("cantidad_stock");
            DateTime fecha = reader.GetDateTime("fecha_creacion");
            string categoria = reader.GetString("categoria");
            return new Producto(id, nombre, codigo, precio, cantidad, fecha, categoria);
        }

        private void ProveState()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

    }
}