using MySql.Data.MySqlClient;

namespace NewPogramMVC
{
    public class ProductoDAOFactory
    {
        public static IProductoDao CrearProductoDAO()
        {
            MySqlConnection connection = Conexion.Instance.AbrirConexion();
            return new ProductoDaoImpl(connection);
        }
    }
}