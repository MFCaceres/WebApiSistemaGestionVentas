using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSistemaGestionVentas.Models
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Edad { get; set; }
        public string? Documento_identificacion { get; set; }
        public string? Id_Tipo_Documento { get; set; }

        public Clientes(int id, string? nombre, string? telefono, string? edad, string? documento_identificacion, string? id_tipo_documento)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
            Edad = edad;
            Documento_identificacion = documento_identificacion;
            Id_Tipo_Documento = id_tipo_documento;
        }

        public Clientes()
        {
            Id = 0;
            Nombre = "";
            Telefono = "";
            Edad = "";
            Documento_identificacion = "";
            Id_Tipo_Documento = "";
        }
    }
}

