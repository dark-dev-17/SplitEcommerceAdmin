using DbManagerDark.Attributes;
using Newtonsoft.Json;
using SplitAdminEcomerce.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "t12_open_pay_webhook_log", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class OPWebHookLog
    {
        [Display(Name = "Id Log")]
        [DarkColumn(Name = "t12_pk01", IsMapped = true, IsKey = true)]
        public int IdOPWebHookLog { get; set; }

        [Display(Name = "Titulo")]
        [DarkColumn(Name = "t12_f001", IsMapped = true, IsKey = false)]
        public string Titulo { get; set; }

        [Display(Name = "No.Pedido")]
        [DarkColumn(Name = "t12_f002", IsMapped = true, IsKey = false)]
        public int IdPedido { get; set; }

        [Display(Name = "Tipo log")]
        [DarkColumn(Name = "t12_f003", IsMapped = true, IsKey = false)]
        public int TipoTracsaction { get; set; }

        [Display(Name = "Estatus")]
        [DarkColumn(Name = "t12_f004", IsMapped = true, IsKey = false)]
        public string Estatus { get; set; }

        [Display(Name = "Detalles")]
        [DarkColumn(Name = "t12_f005", IsMapped = true, IsKey = false)]
        public string Detalles { get; set; }

        [Display(Name = "Tipo pago")]
        [DarkColumn(Name = "t12_f006", IsMapped = true, IsKey = false)]
        public string TipoPago { get; set; }

        [Display(Name = "Creado")]
        [DarkColumn(Name = "t12_f098", IsMapped = true, IsKey = false)]
        public DateTime Creado { get; set; }

        [Display(Name = "Actualizado")]
        [DarkColumn(Name = "t12_f099", IsMapped = true, IsKey = false)]
        public DateTime Actualizado { get; set; }
        
        [Display(Name = "Detalles")]
        [DarkColumn(Name = "t12_f099", IsMapped = false, IsKey = false)]
        public OpenPayResponse Details { get { return JsonConvert.DeserializeObject<OpenPayResponse>(Detalles); } }

    }
}
