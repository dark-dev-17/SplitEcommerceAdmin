using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "menu_blog", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Blog
    {
        [Display(Name = "Clave")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdBlog { get; set; }

        [Required]
        [Display(Name = "Titulo")]
        [DarkColumn(Name = "titulo", IsMapped = true, IsKey = false)]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Titulo landing")]
        [DarkColumn(Name = "titulo_landing", IsMapped = true, IsKey = false)]
        public string Titulo2 { get; set; }

        [Required]
        [Display(Name = "Contenido externo(Tarjeta)")]
        [DarkColumn(Name = "contenido", IsMapped = true, IsKey = false)]
        public string Contenido1 { get; set; }

        [Required]
        [Display(Name = "Contenido interno")]
        [DarkColumn(Name = "contenido_landing", IsMapped = true, IsKey = false)]
        public string Contenido2 { get; set; }

        [Required]
        [Display(Name = "Posicionamiento web")]
        [DarkColumn(Name = "comillas", IsMapped = true, IsKey = false)]
        public string WebDescripcion { get; set; }

        [Display(Name = "Portada externa(Tarjeta)")]
        [DarkColumn(Name = "img", IsMapped = true, IsKey = false)]
        public string ImgCaratula { get; set; }

        [Display(Name = "Portada externa(Tarjeta)")]
        [DarkColumn(Name = "img", IsMapped = false, IsKey = false)]
        public IFormFile ImgCaratulaFile { get; set; }

        [Display(Name = "Portada interna")]
        [DarkColumn(Name = "img_landing", IsMapped = true, IsKey = false)]
        public string ImgLanding { get; set; }

        [Display(Name = "Portada interna")]
        [DarkColumn(Name = "img_landing", IsMapped = false, IsKey = false)]
        public IFormFile ImgLandingFile { get; set; }

        [Display(Name = "Fecha")]
        [DarkColumn(Name = "fecha", IsMapped = true, IsKey = false)]
        public DateTime Fecha { get; set; }

        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "activo", IsMapped = false, IsKey = false)]
        public bool Che_Activo { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdBlog + ""); } }
    }

    [DarkTable(Name = "menu_blog", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class BlogBuscador
    {
        [Display(Name = "Clave")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdBlog { get; set; }

        [Required]
        [Display(Name = "Titulo")]
        [DarkColumn(Name = "titulo", IsMapped = true, IsKey = false)]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Titulo landing")]
        [DarkColumn(Name = "titulo_landing", IsMapped = true, IsKey = false)]
        public string Titulo2 { get; set; }

        [Required]
        [Display(Name = "Contenido externo(Tarjeta)")]
        [DarkColumn(Name = "contenido", IsMapped = true, IsKey = false)]
        public string Contenido1 { get; set; }
        
        [Display(Name = "Portada externa(Tarjeta)")]
        [DarkColumn(Name = "img", IsMapped = true, IsKey = false)]
        public string ImgCaratula { get; set; }

        [Display(Name = "Portada interna")]
        [DarkColumn(Name = "img_landing", IsMapped = true, IsKey = false)]
        public string ImgLanding { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        [DarkColumn(Name = "fecha", IsMapped = true, IsKey = false)]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdBlog + ""); } }

        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "activo", IsMapped = false, IsKey = false)]
        public bool Che_Activo { get { return Activo == "si" ? true : false; } }
    }
}
