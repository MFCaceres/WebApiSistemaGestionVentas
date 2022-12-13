using WebApiSistemaGestionVentas.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

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

        public Venta CargarVenta(Venta venta, ProductoVendido productoVendido)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Venta(Id, Comentarios, IdUsuario) " +
                    "VALUES(@Id, @comentarios, @idUsuario)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = venta.Id });
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = venta.IdUsuario });
                    cmd.ExecuteNonQuery();

                    var actualizarStock = new SqlCommand("UPDATE ProductoVendido Set Stock=Stock-@venta " +
                        "WHERE Id=@Id", conexion);
                    actualizarStock.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = productoVendido.Id });
                    actualizarStock.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int) { Value = productoVendido.Stock});
                    actualizarStock.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int) { Value = productoVendido.IdProducto});
                    actualizarStock.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta});
                    actualizarStock.ExecuteNonQuery();

                    return venta;
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
    }
}
