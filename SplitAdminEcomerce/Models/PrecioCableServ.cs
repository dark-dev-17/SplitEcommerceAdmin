using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    /// <summary>
    /// Precio de los componentes de cable de servicio
    /// </summary>
    [DarkTable(Name = "t19_precios_cable_servicio", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class PrecioCableServ
    {
        [Display(Name = "Clave")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdPrecioCableServ { get; set; }

        [DarkColumn(Name = "componente", IsMapped = true, IsKey = false)]
        public string Componente { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+\.\d{0,3}$", ErrorMessage = "Solo se permiten 3 decimales")]
        [Required]
        [DarkColumn(Name = "precio", IsMapped = true, IsKey = false)]
        public string Precio { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdPrecioCableServ + ""); } }
    }
}
