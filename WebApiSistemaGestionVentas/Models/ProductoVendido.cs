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
        public int Id { get; set; }
        [Required]
        public int IdProducto { get; set; }
        public int Stock { get; set; }
        [Required]
        public int IdVenta { get; set; }

        public ProductoVendido(int id, int idProducto, int stock, int idVenta)
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