using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "Admin_moduloSubPer", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Admin_moduloSubPer
    {
        [Display(Name = "Clave")]
        [DarkColumn(Name = "idAdmin_moduloSubPer", IsMapped = true, IsKey = true)]
        public int idAdmin_moduloSubPer { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "Admin_moduloSub", IsMapped = true, IsKey = false)]
        public int Admin_moduloSub { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "IdSplitnet", IsMapped = true, IsKey = false)]
        public int IdSplitnet { get; set; }


        [Display(Name = "Clave")]
        [DarkColumn(Name = "HasPermise", IsMapped = true, IsKey = false)]
        public bool HasPermise { get; set; }
    }
}
