using System.Data.SqlClient;
using WebApiSistemaGestionVentas.Models;

namespace WebApiSistemaGestionVentas.Repo

{
    public class ClientesRepo
    {
        private SqlConnection? conexion;
        private string cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ajomuch92_coderhouse_csharp_40930;" +
            "User Id=ajomuch92_coderhouse_csharp_40930;" +
            "Password=ElQuequit0Sexy2022;";

        public ClientesRepo()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public List<Clientes> listarClientes()
        {
            List<Clientes> lista = new List<Clientes>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM clientes", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Clientes clientes = new Clientes();
                                clientes.Id = int.Parse(reader["Id"].ToString());
                                clientes.Nombre = reader["Nombre"].ToString();
                                clientes.Telefono = int.Parse(reader["Telefono"].ToString());
                                clientes.Edad = int.Parse(reader["Edad"].ToString());
                                clientes.Documento_identificacion = int.Parse(reader["DocumentoIdentificacion"].ToString());
                                clientes.Id_Tipo_Documento = int.Parse(reader["IdTipoDocumento"].ToString());
                                lista.Add(clientes);
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
