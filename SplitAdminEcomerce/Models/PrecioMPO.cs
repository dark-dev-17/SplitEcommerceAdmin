using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    /// <summary>
    /// Precios componentes para MPO
    /// </summary>
    [DarkTable(Name = "t24_precios_mpo", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class PrecioMPO
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdPrecioMPO { get; set; }

        [DarkColumn(Name = "componente", IsMapped = true, IsKey = false)]
        public string Componente { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+\.\d{0,3}$", ErrorMessage = "Solo se permiten 3 decimales")]
        [Required]
        [DarkColumn(Name = "precio", IsMapped = true, IsKey = false)]
        public double Precio { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdPrecioMPO + ""); } }
    }
}
