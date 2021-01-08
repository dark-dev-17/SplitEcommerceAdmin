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

        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "t41_f006", IsMapped = true, IsKey = false)]
        public int Activo { get; set; }

        [Required]
        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "t41_f006", IsMapped = false, IsKey = false)]
        public bool Che_Activo { get; set; }
        
        [Display(Name = "Fue respondida")]
        [DarkColumn(Name = "t41_f006", IsMapped = false, IsKey = false)]
        public bool Che_ConRespuesta { get; set; }

        [Display(Name = "Categoria")]
        [DarkColumn(Name = "t41_f006", IsMapped = false, IsKey = false)]
        public string Categoria { get; set; }

        [Required]
        [Display(Name = "Consultor")]
        [DarkColumn(Name = "IdConsultor", IsMapped = false, IsKey = false)]
        public int IdConsultor { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdConsultorTecnico + ""); } }
    }
    [DarkTable(Name = "consultor_consultor", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class ConsultorConsultor
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "IdConsultor_consultor", IsMapped = true, IsKey = true)]
        public int IdConsultorConsultor { get; set; }

        [Display(Name = "Pregunta")]
        [DarkColumn(Name = "IdPregunta", IsMapped = true, IsKey = false)]
        public int IdPregunta { get; set; }

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
        [Display(Name = "Creado por")]
        [DarkColumn(Name = "t42_f001", IsMapped = true, IsKey = false)]
        public string Respuesta { get; set; }

        [Required]
        [Display(Name = "Respuesta")]
        [DarkColumn(Name = "t42_f002", IsMapped = true, IsKey = false)]
        public string Fuente { get; set; }

        [Required]
        [Display(Name = "Consulta técnica")]
        [DarkColumn(Name = "t41_pk01", IsMapped = true, IsKey = false)]
        public int IdConsultorTecnico { get; set; }

        [Required]
        [Display(Name = "Creado")]
        [DarkColumn(Name = "t42_f098", IsMapped = true, IsKey = false)]
        public DateTime Creado { get; set; }

        [Required]
        [Display(Name = "Editado")]
        [DarkColumn(Name = "t42_f099", IsMapped = true, IsKey = false)]
        public DateTime Editado { get; set; }

        [Required]
        [Display(Name = "Archivo")]
        [DarkColumn(Name = "t42_f003", IsMapped = true, IsKey = false)]
        public string RutaAchivo { get; set; }

        [Required]
        [Display(Name = "Consultor")]
        [DarkColumn(Name = "IdConsultor", IsMapped = true, IsKey = false)]
        public int IdConsultor { get; set; }
        
        [DarkColumn(Name = "IdConsultor", IsMapped = false, IsKey = false)]
        public UsuarioInterno UsuarioInterno { get; set; }
    }
}
