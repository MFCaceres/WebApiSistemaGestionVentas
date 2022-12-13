using Microsoft.AspNetCore.Mvc;
using System.Data;
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
                                Usuario usuario = new Usuario();
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

        public Usuario? obtenerUsuario(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM usuario WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario = obtenerUsuarioDesdeReader(reader);
                            return usuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
        public Usuario? modificarUsuario(int id, Usuario modificarUsuario)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Usuario? usuario = obtenerUsuario(id);
                if (usuario == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (usuario.Nombre != modificarUsuario.Nombre && !string.IsNullOrEmpty(modificarUsuario.Nombre))
                {
                    camposAActualizar.Add("Nombre = @Nombre");
                    usuario.Nombre = modificarUsuario.Nombre;
                }
                if (usuario.Apellido != modificarUsuario.Apellido && !string.IsNullOrEmpty(modificarUsuario.Apellido))
                {
                    camposAActualizar.Add("Apellido = @Apellido");
                    usuario.Apellido = modificarUsuario.Apellido;
                }
                if (usuario.NombreUsuario != modificarUsuario.NombreUsuario && !string.IsNullOrEmpty(modificarUsuario.NombreUsuario))
                {
                    camposAActualizar.Add("NombreUsuario = @NombreUsuario");
                    usuario.NombreUsuario = modificarUsuario.NombreUsuario;
                }
                if (usuario.Contraseña != modificarUsuario.Contraseña && !string.IsNullOrEmpty(modificarUsuario.Contraseña))
                {
                    camposAActualizar.Add("Contraseña = @Contraseña");
                    usuario.Contraseña = modificarUsuario.Contraseña;
                }
                if (usuario.Mail != modificarUsuario.Mail && !string.IsNullOrEmpty(modificarUsuario.Mail))
                {
                    camposAActualizar.Add("Mail = @Mail");
                    usuario.Mail = modificarUsuario.Mail;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Usuario SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = modificarUsuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = modificarUsuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = modificarUsuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = modificarUsuario.Contraseña });
                    cmd.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = modificarUsuario.Mail });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return usuario;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        private Usuario obtenerUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.Id = int.Parse(reader["Id"].ToString());
            usuario.Nombre = reader["Nombre"].ToString();
            usuario.Apellido = reader["Apellido"].ToString();
            usuario.NombreUsuario = reader["NombreUsuario"].ToString();
            usuario.Contraseña = reader["Contraseña"].ToString();
            usuario.Mail = reader["Mail"].ToString();
            return usuario;
        }

    }
}
