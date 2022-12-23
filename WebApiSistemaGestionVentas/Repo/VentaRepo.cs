using WebApiSistemaGestionVentas.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Security.Cryptography.X509Certificates;

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
            if (conexion == null)
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
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = new Venta();
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
            catch (Exception ex)
            {
                throw;
            }
            return lista;
        }

        public void CargarVenta(Venta venta)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario) VALUES(@comentarios, @idUsuario); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Float) { Value = venta.IdUsuario });
                    venta.Id = long.Parse(cmd.ExecuteScalar().ToString());
                    if (venta.ProductosVendidos != null && venta.ProductosVendidos.Count > 0)
                    {
                        foreach (ProductoVendido productoVendido in venta.ProductosVendidos)
                        {
                            productoVendido.IdVenta = venta.Id;
                            ProductoVendido productoVendidoRegistrado = RegistrarProducto(productoVendido);
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

        private ProductoVendido RegistrarProducto(ProductoVendido productoVendido)
        {
            Producto? producto = ProductoRepo.obtenerProductoSimplificadoPorId(productoVendido.IdProducto, conexion);
            if (producto != null)
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta) VALUES(@stock, @idProducto, @idVenta); SELECT @@Identity;", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.BigInt) { Value = productoVendido.Stock });
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.Int) { Value = productoVendido.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta });
                    productoVendido.Id = long.Parse(cmd.ExecuteScalar().ToString());
                }
                DisminuiStock(producto, productoVendido.Stock);
            }
            else
            {
                throw new Exception("Producto no encontrado");
            }
            return productoVendido;
        }

        private void DisminuiStock(Producto producto, int cantidadVendida)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Producto SET stock = @stock WHERE id = @id", conexion))
            {
                cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock - cantidadVendida });
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = producto.Id });
                cmd.ExecuteNonQuery();
            }
        }

        public List<Venta> ObtenerVenta(long? id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            List<Venta> lista = new List<Venta>();
            try
            {
                string query = "SELECT id, Comentarios, idUsuario FROM venta";
                if (id != null)
                {
                    query += " WHERE id = @id";
                }
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    conexion.Open();
                    if (id != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = new Venta()
                                {
                                    Id = long.Parse(reader["id"].ToString()),
                                    Comentarios = reader["Comentarios"].ToString(),
                                    IdUsuario = int.Parse(reader["idUsuario"].ToString())
                                };
                                lista.Add(venta);
                            }
                        }
                    }
                    foreach (Venta venta in lista)
                    {
                        venta.ProductosVendidos = ObtenerProductosVendidos(venta.Id);
                    }
                }
                return lista;
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

        private List<ProductoVendido> ObtenerProductosVendidos(long id)
        {
            List<ProductoVendido> productoVendidos = new List<ProductoVendido>();
            string query = "SELECT A.Id, A.IdProducto, A.Stock, B.Descripciones, B.PrecioVenta " +
                "FROM ProductoVendido AS A " +
                "INNER JOIN Producto AS B " +
                "ON A.IdProducto = B.Id " +
                "WHERE A.IdVenta = @id";
            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
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
                            productoVendidos.Add(productoVendido);
                        }
                    }
                }
            }
            return productoVendidos;
        }

        public bool EliminarVenta(long id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasafectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Venta WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                    filasafectadas = cmd.ExecuteNonQuery();
                }
                conexion.Close();
                return filasafectadas > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}


