using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    /// <summary>
    /// Precios de elementos para distribuidor preconectorizado
    /// </summary>
    [DarkTable(Name = "t30_precios_distribuidores_preconectorizados", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class PrecioDistriPrecon
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdPrecioDistriPrecon { get; set; }

        [DarkColumn(Name = "tipo", IsMapped = true, IsKey = false)]
        public string Tipo { get; set; }

        [DarkColumn(Name = "componente", IsMapped = true, IsKey = false)]
        public string Componente { get; set; }

        [DarkColumn(Name = "cantidad", IsMapped = true, IsKey = false)]
        public string Cantidad { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+\.\d{0,3}$", ErrorMessage = "Solo se permiten 3 decimales")]
        [Required]
        [DarkColumn(Name = "precio", IsMapped = true, IsKey = false)]
        public float Precio { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdPrecioDistriPrecon + ""); } }
    }
}
