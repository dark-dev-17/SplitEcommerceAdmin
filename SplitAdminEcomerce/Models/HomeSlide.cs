using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "t35_HomeSlide", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class HomeSlide
    {

        [Display(Name = "#")]
        [DarkColumn(Name = "t35_pk01", IsMapped = true, IsKey = true)]
        public int IdHomeSlide { get; set; }

        [Required]
        [Display(Name = "Titulo")]
        [DarkColumn(Name = "t35_f001", IsMapped = true, IsKey = false)]
        public string Titulo { get; set; }

        [Display(Name = "Img.Izquierda")]
        [DarkColumn(Name = "t35_f002", IsMapped = true, IsKey = false)]
        public string Imagen1_name { get; set; }

        [Display(Name = "Img.Derecha")]
        [DarkColumn(Name = "t35_f003", IsMapped = true, IsKey = false)]
        public string Imagen2_name { get; set; }

        [Required]
        [Display(Name = "Link 1")]
        [DarkColumn(Name = "t35_f004", IsMapped = true, IsKey = false)]
        public string Link1 { get; set; }

        [Required]
        [Display(Name = "Link 2")]
        [DarkColumn(Name = "t35_f005", IsMapped = true, IsKey = false)]
        public string Link2 { get; set; }

        [Display(Name = "Link general")]
        [DarkColumn(Name = "t35_f006", IsMapped = true, IsKey = false)]
        public string LinkGeneral { get; set; }

        [Required]
        [Display(Name = "Segmento")]
        [DarkColumn(Name = "t35_f007", IsMapped = true, IsKey = false)]
        public string Segmento { get; set; }

        [Display(Name = "Grupo Sap")]
        [DarkColumn(Name = "t35_f008", IsMapped = true, IsKey = false)]
        public string SapGrupo { get; set; }

        [Display(Name = "Cod.Producto")]
        [DarkColumn(Name = "t35_f009", IsMapped = true, IsKey = false)]
        public string CodProducto { get; set; }

        [Display(Name = "Regla")]
        [DarkColumn(Name = "t35_f010", IsMapped = true, IsKey = false)]
        public string Regla { get; set; }

        [Display(Name = "Posición")]
        [DarkColumn(Name = "t35_f011", IsMapped = true, IsKey = false)]
        public int Posicion { get; set; }


        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "t35_f012", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "Categoria")]
        [DarkColumn(Name = "t35_f013", IsMapped = true, IsKey = false)]
        public string Categoria { get; set; }

        [Display(Name = "Abrir en nueva pestaña")]
        [DarkColumn(Name = "t35_f014", IsMapped = true, IsKey = false)]
        public string NewWindows1 { get; set; }

        [Display(Name = "Abrir en nueva pestaña")]
        [DarkColumn(Name = "t35_f015", IsMapped = true, IsKey = false)]
        public string NewWindows2 { get; set; }

        [Display(Name = "Imagen 1")]
        [DarkColumn(Name = "t35_f015", IsMapped = false, IsKey = false)]
        public IFormFile Imagen1 { get; set; }

        [Display(Name = "Imagen 2")]
        [DarkColumn(Name = "t35_f015", IsMapped = false, IsKey = false)]
        public IFormFile Imagen2 { get; set; }

        [Display(Name = "Abrir en nueva pestaña")]
        [DarkColumn(Name = "t35_f014", IsMapped = false, IsKey = false)]
        public bool Che_NewWindows1 { get; set; }

        [Display(Name = "Abrir en nueva pestaña")]
        [DarkColumn(Name = "t35_f015", IsMapped = false, IsKey = false)]
        public bool Che_NewWindows2 { get; set; }

        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "t35_f015", IsMapped = false, IsKey = false)]
        public bool Che_active { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdHomeSlide + ""); } }
    }
}
