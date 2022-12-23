using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSistemaGestionVentas.Models
{
    public class Venta
    {
        public long Id { get; set; } 
        public string? Comentarios { get; set; }
        public int IdUsuario { get; set; }  
        public List<ProductoVendido>? ProductosVendidos { get; set; }

        public Venta(long id, string? comentarios, int idUsuario) 
        { 
            Id = id;
            Comentarios = comentarios;
            IdUsuario= idUsuario;
        }

        public Venta() 
        { 
            Id= 0;
            Comentarios = "";
            IdUsuario= 0;  
            ProductosVendidos= new List<ProductoVendido>();
        }
    }
}
