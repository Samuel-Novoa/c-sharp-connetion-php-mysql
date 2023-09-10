using NewPogramMVC;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewPogramMVC
{
    public interface IProductoDao
    {
        bool Crear(Producto producto);
        List<Producto> Obtener();
        bool Actualizar(Producto producto);
        bool Eliminar(Producto producto);
        Producto ObtenerProductoPorId(int id);
        List<Producto> ObtenerTodosLosProductos();

        //
        bool SeleccionCalcularDescuentoProductos(Producto producto);
        //List<DescProducto> CalcularDescuentoProductos();
    }
}