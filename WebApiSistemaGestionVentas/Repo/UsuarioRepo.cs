using System.Data.SqlClient;
using WebApiSistemaGestionVentas.Models;

namespace WebApiSistemaGestionVentas.Repo
{
    public class UsuarioRepo
    {
        private SqlConnection? conexion;
        private string cadenaconexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ajomuch92_coderhouse_csharp_40930;" +
            "User Id=ajomuch92_coderhouse_csharp_40930;" +
            "Password=ElQuequit0Sexy2022;";

        public UsuarioRepo()
        {
            try
            {
                conexion = new SqlConnection(cadenaconexion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Usuario> listarUsuario()
        {
            List<Usuario> lista = new List<Usuario>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read()) 
                            { 
                                Usuario usuario= new Usuario(); 
                                usuario.Id = int.Parse(reader["Id"].ToString());
                                usuario.Nombre = reader["Nombre"].ToString();
                                usuario.Apellido = reader["Apellido"].ToString();
                                usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                                usuario.Contraseña = reader["Contraseña"].ToString();
                                usuario.Mail = reader["Mail"].ToString();
                                lista.Add(usuario);                        
                            }
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return lista;
        }
    }
}
