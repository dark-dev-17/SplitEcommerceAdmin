using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "t43_usuarios_internos", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class UsuarioInterno
    {
        [Display(Name = "Clave")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdUsuarioInterno { get; set; }

        [Display(Name = "Nombre")]
        [DarkColumn(Name = "nombre", IsMapped = true, IsKey = false)]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [DarkColumn(Name = "apellido", IsMapped = true, IsKey = false)]
        public string Apellido { get; set; }

        [Display(Name = "Imagen")]
        [DarkColumn(Name = "imagen", IsMapped = true, IsKey = false)]
        public string IdImagen { get; set; }

        [Display(Name = "IdSplinet")]
        [DarkColumn(Name = "IdSplitnet", IsMapped = true, IsKey = false)]
        public int IdSplitnet { get; set; }

        [Display(Name = "Email")]
        [DarkColumn(Name = "email", IsMapped = true, IsKey = false)]
        public string Email { get; set; }

        [Display(Name = "Nombre")]
        [DarkColumn(Name = "email", IsMapped = false, IsKey = false)]
        public string NombreCompleto { get { return Nombre + Apellido; } }
    }
}
