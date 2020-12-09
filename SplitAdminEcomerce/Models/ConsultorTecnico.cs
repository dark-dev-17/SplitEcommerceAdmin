using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "t41_consultecnico_pregunta", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class ConsultorTecnico
    {
        [Display(Name = "Clave")]
        [DarkColumn(Name = "t41_pk01", IsMapped = true, IsKey = true)]
        public int IdConsultorTecnico { get; set; }

        [Required]
        [Display(Name = "Solicitante")]
        [DarkColumn(Name = "t41_f001", IsMapped = true, IsKey = false)]
        public string Solicitante { get; set; }

        [Required]
        [Display(Name = "Correo")]
        [DarkColumn(Name = "t41_f002", IsMapped = true, IsKey = false)]
        public string Correo { get; set; }

        [Required]
        [Display(Name = "Titulo")]
        [DarkColumn(Name = "t41_f003", IsMapped = true, IsKey = false)]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        [DarkColumn(Name = "t41_f004", IsMapped = true, IsKey = false)]
        public string IdCategoria { get; set; }

        [Required]
        [Display(Name = "Pregunta")]
        [DarkColumn(Name = "t41_f005", IsMapped = true, IsKey = false)]
        public string Pregunta { get; set; }

        [Required]
        [Display(Name = "Creado")]
        [DarkColumn(Name = "t41_f098", IsMapped = true, IsKey = false)]
        public DateTime Creado { get; set; }

        [Required]
        [Display(Name = "Editado")]
        [DarkColumn(Name = "t41_f099", IsMapped = true, IsKey = false)]
        public DateTime Editado { get; set; }

        [Required]
        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "t41_f006", IsMapped = true, IsKey = false)]
        public int Activo { get; set; }

        [Required]
        [Display(Name = "Consultor")]
        [DarkColumn(Name = "IdConsultor", IsMapped = true, IsKey = false)]
        public int IdConsultor { get; set; }
    }
    [DarkTable(Name = "t42_consultecnico_respuestas", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class ConsultorRespuestas
    {
        [Display(Name = "Clave")]
        [DarkColumn(Name = "t42_pk01", IsMapped = true, IsKey = true)]
        public int IdConsultorRespuestas { get; set; }

        [Required]
        [Display(Name = "Respuesta")]
        [DarkColumn(Name = "t42_f001", IsMapped = true, IsKey = false)]
        public string Respuesta { get; set; }

        [Required]
        [Display(Name = "Respuesta")]
        [DarkColumn(Name = "t42_f002", IsMapped = true, IsKey = false)]
        public string Fuente { get; set; }

        [Required]
        [Display(Name = "Consulta técnica")]
        [DarkColumn(Name = "t42_f001", IsMapped = true, IsKey = false)]
        public int IdConsultorTecnico { get; set; }

        [Required]
        [Display(Name = "Creado")]
        [DarkColumn(Name = "t42_f001", IsMapped = true, IsKey = false)]
        public DateTime Creado { get; set; }

        [Required]
        [Display(Name = "Editado")]
        [DarkColumn(Name = "t42_f001", IsMapped = true, IsKey = false)]
        public DateTime Editado { get; set; }

        [Required]
        [Display(Name = "Archivo")]
        [DarkColumn(Name = "t42_f001", IsMapped = true, IsKey = false)]
        public string RutaAchivo { get; set; }

        [Required]
        [Display(Name = "Consultor")]
        [DarkColumn(Name = "t42_f001", IsMapped = true, IsKey = false)]
        public int IdConsultor { get; set; }
    }
}
