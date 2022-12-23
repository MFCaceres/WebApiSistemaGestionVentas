using System.Data;
using System.Data.SqlClient;
using WebApiSistemaGestionVentas.Models;

namespace WebApiSistemaGestionVentas.Repo
{
    public class ProductoVendidoRepo
    {
        private SqlConnection? conexion;
        private string cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ajomuch92_coderhouse_csharp_40930;" +
            "User Id=ajomuch92_coderhouse_csharp_40930;" +
            "Password=ElQuequit0Sexy2022;";

        public ProductoVendidoRepo()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public List<ProductoVendido> TraerProductosVendidos()
        {
            List<ProductoVendido> productosVendidos1 = new List<ProductoVendido>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
            string query = "SELECT * FROM ProductoVendido inner join Producto ON ProductoVendido.IdProducto = Producto.Id";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                conexion.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ProductoVendido productoVendido = new ProductoVendido()
                            {
                                Id = long.Parse(reader["Id"].ToString()),
                                IdProducto = int.Parse(reader["IdProducto"].ToString()),
                                Stock = int.Parse(reader["Stock"].ToString()),
                                producto = new Producto()
                                {
                                    Descripcion = reader["Descripciones"].ToString(),
                                    PrecioVenta = double.Parse(reader["PrecioVenta"].ToString())
                                }
                            };
                            productosVendidos1.Add(productoVendido);
                        }
                    }
                }
                    conexion.Close();
                }
            }
            catch
            {
                throw;
            }
            return productosVendidos1;
        }
    }
}
