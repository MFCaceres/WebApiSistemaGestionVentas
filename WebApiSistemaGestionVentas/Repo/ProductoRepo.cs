using System.Data.SqlClient;
using WebApiSistemaGestionVentas.Models;

namespace WebApiSistemaGestionVentas.Repo
{
    public class ProductoRepo
    {
        private SqlConnection? conexion;
        private string cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ajomuch92_coderhouse_csharp_40930;" +
            "User Id=ajomuch92_coderhouse_csharp_40930;" +
            "Password=ElQuequit0Sexy2022;";

        public ProductoRepo()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public List<Producto> listarProductos()
        {
            List<Producto> lista = new List<Producto>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM producto", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = int.Parse(reader["Id"].ToString());
                                producto.Descripcion = reader["Descripciones"].ToString();
                                producto.Costo = double.Parse(reader["Costo"].ToString());
                                producto.PrecioVenta = double.Parse(reader["PrecioVenta"].ToString());
                                producto.Stock = int.Parse(reader["Stock"].ToString());
                                lista.Add(producto);
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