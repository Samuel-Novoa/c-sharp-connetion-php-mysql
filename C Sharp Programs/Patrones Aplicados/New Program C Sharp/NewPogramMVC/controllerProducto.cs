using NewPogramMVC;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPogramMVC
{
    public class ControllerProducto
    {
        private ViewProducto vista = new ViewProducto();
        private IProductoDao dao;

        public ControllerProducto(IProductoDao dao)
        {
            this.dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public bool CrearProducto(Producto producto)
        {
            try
            {
                return dao.Crear(producto);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al crear el producto: " + e.Message);
                return false;
            }
        }

        public bool ActualizarProducto(Producto producto)
        {
            try
            {
                return dao.Actualizar(producto);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al actualizar el producto: " + e.Message);
                return false;
            }
        }

        public bool EliminarProducto(Producto producto)
        {
            try
            {
                return dao.Eliminar(producto);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al eliminar el producto: " + e.Message);
                return false;
            }
        }

        public void VerProductos()
        {
            try
            {
                List<Producto> productos = dao.Obtener();
                vista.VerProductos(productos);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener los productos: " + e.Message);
            }
        }

        //public bool EliminarProducto(Producto producto)
        //{
        //    try
        //    {
        //        return dao.Eliminar(producto);
        //    }
        //    catch (DAOException e)
        //    {
        //        Console.WriteLine("Error al eliminar el producto: " + e.Message);
        //        return false;
        //    }
        //}

        public bool CalcularDescuentoPorId(Producto producto)
        {
            try
            {
                return dao.SeleccionCalcularDescuentoProductos(producto);
            }
            catch (DAOException e)
            {
                Console.WriteLine("Error al obtener la seleccion del producto: " + e.Message);
                return false;
            }
        }

        //public void CalculaDescuentoPorId(int id)
        //{
        //    Producto producto = dao.ObtenerProductoPorId(id);

        //    try
        //    {
        //        if (producto != null)
        //        {
        //            double descuento = producto.PrecioDelProducto * 0.10;

        //            Console.WriteLine($"║ Producto: {producto.NombreDelProducto}");
        //            Console.WriteLine($"║ Precio Original: {producto.PrecioDelProducto}");
        //            Console.WriteLine($"║ Descuento del 10%: {descuento}");
        //            Console.WriteLine($"║ Precio con descuento: {producto.PrecioDelProducto - descuento}");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Producto no encontrado.");
        //        }
        //    }
        //    catch (DAOException e)
        //    {
        //        Console.WriteLine("Error en calcular el descuento del producto: " + e.Message);
        //    }
        //}
    }
}