using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    /// <summary>
    /// Precios de elementos para Pigtail
    /// </summary>
    [DarkTable(Name = "t18_precios_pigtails", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class PrecioPigtail
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdPrecioPigtail { get; set; }

        [DarkColumn(Name = "tipo", IsMapped = true, IsKey = false)]
        public string Tipo { get; set; }

        [DarkColumn(Name = "componente", IsMapped = true, IsKey = false)]
        public string Componente { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+\.\d{0,3}$", ErrorMessage = "Solo se permiten 3 decimales")]
        [Required]
        [DarkColumn(Name = "precio", IsMapped = true, IsKey = false)]
        public float Precio { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdPrecioPigtail + ""); } }
    }
}
