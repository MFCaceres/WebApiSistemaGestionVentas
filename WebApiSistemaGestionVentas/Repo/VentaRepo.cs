using WebApiSistemaGestionVentas.Models;
using System.Data.SqlClient;


namespace WebApiSistemaGestionVentas.Repo
{
    public class VentaRepo
    {
        private SqlConnection? conexion;
        private string cadenaconexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ajomuch92_coderhouse_csharp_40930;" +
            "User Id=ajomuch92_coderhouse_csharp_40930;" +
            "Password=ElQuequit0Sexy2022;";

        public VentaRepo()
        {
            try
            {
                conexion = new SqlConnection(cadenaconexion);
            }
            catch
            {
                throw;
            }
        }

        public List<Venta> listarVenta()
        {
            List<Venta> lista = new List<Venta>();
            if(conexion == null) 
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta", conexion))
                {
                    conexion.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    {
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta= new Venta();
                                venta.Id = long.Parse(reader["Id"].ToString());
                                venta.Comentarios = reader["Comentarios"].ToString();
                                venta.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
                                lista.Add(venta);
                            }
                        }
                    }

                }
                conexion.Close();
            }
            catch(Exception ex)
            {
                throw;
            }
            return lista;
        }
    }
}
