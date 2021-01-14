using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "t39_FileSecciones", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Cont_SeccionArchivo
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "t39_pk01", IsMapped = true, IsKey = true)]
        public int IdCont_SeccionArchivo { get; set; }

        [Display(Name = "Archivo")]
        [DarkColumn(Name = "t39_f001", IsMapped = true, IsKey = false)]
        public string ArchivoPath { get; set; }

        [Required]
        [Display(Name = "Descripción web")]
        [DarkColumn(Name = "t39_f002", IsMapped = true, IsKey = false)]
        public string DescripcionWeb { get; set; }

        [Display(Name = "Seccion")]
        [DarkColumn(Name = "t38_pk01", IsMapped = true, IsKey = false)]
        public int IdCont_Seccion { get; set; }

        [Display(Name = "Posicion")]
        [DarkColumn(Name = "t39_f003", IsMapped = true, IsKey = false)]
        public int Posicion { get; set; }

        [Display(Name = "Visible E-Commerce")]
        [DarkColumn(Name = "t39_f004", IsMapped = true, IsKey = false)]
        public int EsVisible { get; set; }

        [Required]
        [Display(Name = "Url")]
        [DarkColumn(Name = "t39_f005", IsMapped = true, IsKey = false)]
        public string Url { get; set; }

        [Display(Name = "Abrir en otra pestaña")]
        [DarkColumn(Name = "t39_f006", IsMapped = true, IsKey = false)]
        public int AbrirNuevatab { get; set; }

        #region Fuera de DB
        [Display(Name = "Abrir en otra pestaña")]
        [DarkColumn(Name = "t39_f006", IsMapped = false, IsKey = false)]
        public bool Che_AbrirNuevatab { get; set; }

        [Display(Name = "Visible E-Commerce")]
        [DarkColumn(Name = "t39_f004", IsMapped = false, IsKey = false)]
        public bool Che_EsVisible { get; set; }

        [Display(Name = "Artchivo")]
        [DarkColumn(Name = "img_landing", IsMapped = false, IsKey = false)]
        public IFormFile Archivo { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdCont_SeccionArchivo + ""); } }
        #endregion
    }
}
