using NewPogramMVC;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPogramMVC
{
    public class ViewProducto
    {
        public void VerProducto(Producto producto)
        {
            Console.WriteLine("Datos del producto:\n" + producto.ToString());
        }

        public void VerProductos(List<Producto> productos)
        {
            if (productos.Count == 0)
            {
                Console.WriteLine("No hay productos para mostrar.");
                return;
            }

            Console.WriteLine("Lista de productos:");
            foreach (Producto producto in productos)
            {
                Console.WriteLine("------------");
                Console.WriteLine(producto.ToString());
            }
        }
    }
}