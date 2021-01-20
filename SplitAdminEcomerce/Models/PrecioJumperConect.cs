using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SplitAdminEcomerce.Models
{
    /// <summary>
    /// Precios de elementos para jumper conector
    /// </summary>
    [DarkTable(Name = "t20_jumper_precio_conector", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class PrecioJumperConect
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "t20_pk01", IsMapped = true, IsKey = true)]
        public int IdPrecioJumperConect { get; set; }

        [DarkColumn(Name = "t20_f001", IsMapped = true, IsKey = false)]
        public float Precio { get; set; }

        [DarkColumn(Name = "t91_pk01", IsMapped = true, IsKey = false)]
        public int TipoJumper { get; set; }

        [DarkColumn(Name = "t91_pk01_", IsMapped = true, IsKey = false)]
        public int TipoFibra { get; set; }

        [DarkColumn(Name = "t91_pk01__", IsMapped = true, IsKey = false)]
        public int TipoCubierta { get; set; }

        [DarkColumn(Name = "t20_f098", IsMapped = true, IsKey = false)]
        public DateTime Creado { get; set; }

        [DarkColumn(Name = "t20_f099", IsMapped = true, IsKey = false)]
        public DateTime Modificado { get; set; }

        [Display(Name = "Tipo Jumper")]
        [DarkColumn(Name = "t91_pk01", IsMapped = false, IsKey = false)]
        public string TipoJumper_ { get; set; }

        [Display(Name = "Tipo de fibra")]
        [DarkColumn(Name = "t91_pk01_", IsMapped = false, IsKey = false)]
        public string TipoFibra_ { get; set; }

        [Display(Name = "Tipo de Cubierta")]
        [DarkColumn(Name = "t91_pk01__", IsMapped = false, IsKey = false)]
        public string TipoCubierta_ { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdPrecioJumperConect + ""); } }
    }
}
