using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebApiSistemaGestionVentas.Repo
{
    public class ProductoVendido
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

        public List<ProductoVendido> listarProductoVendido()
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido productovendido = new ProductoVendido();
                                ProductoVendido.Id = int.Parse(reader["Id"].ToString());
                                ProductoVendido.Stock = int.Parse(reader["Stock"].ToString());
                                ProductoVendido.IdProducto = int.Parse(reader["IdProducto"].ToString());
                                ProductoVendido.IdVenta = int.Parse(reader["IdVenta"].ToString());
                                lista.Add(productovendido);
                            }
                        }
                    }
                }
                conexion.Close();
            }
            catch
            {
                throw;
            }
            return lista;
        }

    }
}
