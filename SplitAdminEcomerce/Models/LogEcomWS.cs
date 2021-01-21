using DbManagerDark.Attributes;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "t99_log", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class LogEcomWS
    {
        [Display(Name = "#")]
        [DarkColumn(Name = "t99_pk01", IsMapped = true, IsKey = true)]
        public int IdLogEcomWS { get; set; }

        [DarkColumn(Name = "t99_f002", IsMapped = true, IsKey = false)]
        public int CodeResult { get; set; }

        [DarkColumn(Name = "t99_f003", IsMapped = true, IsKey = false)]
        public int NoPedido { get; set; }

        [DarkColumn(Name = "t99_f007", IsMapped = true, IsKey = false)]
        public string TipoPedido { get; set; }

        [DarkColumn(Name = "t99_f004", IsMapped = true, IsKey = false)]
        public string Respuesta { get; set; }

        [DarkColumn(Name = "t99_f005", IsMapped = true, IsKey = false)]
        public string JsonRequest { get; set; }

        [DarkColumn(Name = "t99_f006", IsMapped = true, IsKey = false)]
        public string JsonResponse { get; set; }

        [DarkColumn(Name = "t99_f099", IsMapped = true, IsKey = false)]
        public DateTime Fecha { get; set; }
    }
}
