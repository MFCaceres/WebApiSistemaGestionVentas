using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSistemaGestionVentas.Models
{
    public class ProductoVendido
    {
        public long Id { get; set; }
              
        public int Stock { get; set; }

        public int IdProducto { get; set; }
        public long IdVenta { get; set; }

        public ProductoVendido(long id, int idProducto, int stock, long idVenta)
        {
            Id = id;
            IdProducto = idProducto;
            Stock = stock;
            IdVenta = idVenta;
        }

        public ProductoVendido() 
        { 
            Id = 0;
            IdProducto = 0;
            Stock = 0;  
            IdVenta= 0;           
        
        }
    }
}