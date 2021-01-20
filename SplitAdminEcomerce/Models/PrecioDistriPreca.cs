using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    /// <summary>
    /// Precios de los distribuidores precargados
    /// </summary>
    [DarkTable(Name = "t26_precios_distribuidores_precargados", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class PrecioDistriPreca
    {
        [Display(Name = "Clave")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdPrecioDistriPreca { get; set; }

        [DarkColumn(Name = "tipo", IsMapped = true, IsKey = false)]
        public string Tipo { get; set; }

        [DarkColumn(Name = "componente", IsMapped = true, IsKey = false)]
        public string Componente { get; set; }

        [DarkColumn(Name = "placas", IsMapped = true, IsKey = false)]
        public string Placas { get; set; }

        [DarkColumn(Name = "referencia", IsMapped = true, IsKey = false)]
        public string Refrencia { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+\.\d{0,3}$", ErrorMessage = "Solo se permiten 3 decimales")]
        [Required]
        [DarkColumn(Name = "precio", IsMapped = true, IsKey = false)]
        public string Precio { get; set; }

        [DarkColumn(Name = "descripcion", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdPrecioDistriPreca + ""); } }
    }
}
