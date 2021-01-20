using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    /// <summary>
    /// Precios para elementos de jumper cable
    /// </summary>
    [DarkTable(Name = "t21_jumpers_precio_cable", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class PrecioJumperCable
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "t21_pk01", IsMapped = true, IsKey = true)]
        public int IdPrecioJumperCable { get; set; }

        [DarkColumn(Name = "t21_f001", IsMapped = true, IsKey = false)]
        [Display(Name = "Precio")]
        [RegularExpression(@"^\d+\.\d{0,3}$", ErrorMessage = "Solo se permiten 3 decimales")]
        [Required]
        public float Precio { get; set; }

        [DarkColumn(Name = "t91_pk01", IsMapped = true, IsKey = false)]
        public int TipoJumper { get; set; }

        [DarkColumn(Name = "t91_pk01_", IsMapped = true, IsKey = false)]
        public int TipoFibra { get; set; }

        [DarkColumn(Name = "t91_pk01__", IsMapped = true, IsKey = false)]
        public int TipoCubierta { get; set; }

        [DarkColumn(Name = "t21_f098", IsMapped = true, IsKey = false)]
        public DateTime Creado { get; set; }

        [DarkColumn(Name = "t21_f099", IsMapped = true, IsKey = false)]
        public DateTime Modificado { get; set; }

        [DarkColumn(Name = "t91_pk01___", IsMapped = true, IsKey = false)]
        public int TipoHilo { get; set; }


        [Display(Name = "Tipo Jumper")]
        [DarkColumn(Name = "t91_pk01", IsMapped = false, IsKey = false)]
        public string TipoJumper_ { get; set; }

        [Display(Name = "Tipo de fibra")]
        [DarkColumn(Name = "t91_pk01_", IsMapped = false, IsKey = false)]
        public string TipoFibra_ { get; set; }

        [Display(Name = "Tipo de Cubierta")]
        [DarkColumn(Name = "t91_pk01__", IsMapped = false, IsKey = false)]
        public string TipoCubierta_ { get; set; }

        [Display(Name = "Tipo de Hilo")]
        [DarkColumn(Name = "t91_pk01__", IsMapped = false, IsKey = false)]
        public string TipoHilo_ { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdPrecioJumperCable + ""); } }
    }
}
