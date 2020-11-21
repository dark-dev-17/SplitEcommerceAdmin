using DbManagerDark.Attributes;
using SplitAdminEcomerce.Catalogos;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SplitAdminEcomerce.Models
{
    /// <summary>
    /// Control de web services documentos en firme
    /// </summary>
    [DarkTable(Name = "t05_factura_b2c", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class WsB2C
    {
        [Display(Name = "IdWsB2C")]
        [DarkColumn(Name = "t05_pk01", IsMapped = true, IsKey = true)]
        public int IdWsB2C { get; set; }

        [Display(Name = "Ref. Open pay")]
        [DarkColumn(Name = "t05_f009", IsMapped = true, IsKey = false)]
        public string ReferenciaOpenPay { get; set; }

        [Display(Name = "Cotización")]
        [DarkColumn(Name = "t05_f004", IsMapped = true, IsKey = false)]
        public string CotizacionSAP { get; set; }

        [Display(Name = "Factura")]
        [DarkColumn(Name = "t05_f005", IsMapped = true, IsKey = false)]
        public string FacturaSAP { get; set; }

        [Display(Name = "Pago")]
        [DarkColumn(Name = "t05_f012", IsMapped = true, IsKey = false)]
        public string PagoSAP { get; set; }

        [Display(Name = "No.Pedido")]
        [DarkColumn(Name = "t06_f006", IsMapped = true, IsKey = false)]
        public string IdPedido { get; set; }

        [Display(Name = "Requiere factura")]
        [DarkColumn(Name = "t05_f007", IsMapped = true, IsKey = false)]
        public string RequiereFactura { get; set; }

        [Display(Name = "Ws estatus")]
        [DarkColumn(Name = "t05_f008", IsMapped = true, IsKey = false)]
        public string EstatusWS { get; set; }

        [Display(Name = "Referencias paqueteria")]
        [DarkColumn(Name = "t05_f010", IsMapped = true, IsKey = false)]
        public string ReferenciasPaque { get; set; }

        [Display(Name = "Intentos")]
        [DarkColumn(Name = "t05_f011", IsMapped = true, IsKey = false)]
        public int Intentos { get; set; }

        [Display(Name = "Ws estatus")]
        [DarkColumn(Name = "t05_f098", IsMapped = true, IsKey = false)]
        public DateTime Creado { get; set; }

        [Display(Name = "Ws estatus")]
        [DarkColumn(Name = "t05_f099", IsMapped = true, IsKey = false)]
        public DateTime Actualizado { get; set; }
    }
}
