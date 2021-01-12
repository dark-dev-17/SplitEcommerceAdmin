using DbManagerDark.Attributes;
using SplitAdminEcomerce.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SplitAdminEcomerce.ModelsSAP
{
    [DarkTable(Name = "OCRG", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class CardGroup
    {
        [Display(Name = "GroupCode")]
        [DarkColumn(Name = "GroupCode", IsMapped = true, IsKey = true)]
        public int GroupCode { set; get; }
        [Display(Name = "GroupName")]
        [DarkColumn(Name = "GroupName", IsMapped = true, IsKey = false)]
        public string GroupName { set; get; }
    }
}
