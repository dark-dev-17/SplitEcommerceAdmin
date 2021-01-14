using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "t38_FilesType", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Cont_Seccion
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "t38_pk01", IsMapped = true, IsKey = true)]
        public int IdCont_Seccion { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [DarkColumn(Name = "t38_f001", IsMapped = true, IsKey = false)]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Ruta Ftp de archivos")]
        [DarkColumn(Name = "t38_f002", IsMapped = true, IsKey = false)]
        public string PathFtp { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdCont_Seccion + ""); } }
    }
}
