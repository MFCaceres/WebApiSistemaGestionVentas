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
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public int Edad { get; set; }
        public int Documento_identificacion { get; set; }
        public int Id_Tipo_Documento { get; set; }

        public Clientes(int id, string nombre, int telefono, int edad, int documento_identificacion, int id_tipo_documento)
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
            Telefono = 0;
            Edad = 0;
            Documento_identificacion = 0;
            Id_Tipo_Documento = 0;
        }
    }
}

