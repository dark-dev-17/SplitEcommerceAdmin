using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    /// <summary>
    /// Precios de elementos para PatchCord
    /// </summary>
    [DarkTable(Name = "t07_precios_patchcord", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class PrecioPatchCord
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdPrecioPatchCord { get; set; }

        [DarkColumn(Name = "tipo", IsMapped = true, IsKey = false)]
        public string Tipo { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+\.\d{0,3}$", ErrorMessage = "Solo se permiten 3 decimales")]
        [Required]
        [DarkColumn(Name = "base", IsMapped = true, IsKey = false)]
        public double Base { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+\.\d{0,3}$", ErrorMessage = "Solo se permiten 3 decimales")]
        [Required]
        [DarkColumn(Name = "factor", IsMapped = true, IsKey = false)]
        public double Factor { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdPrecioPatchCord + ""); } }
    }
}
