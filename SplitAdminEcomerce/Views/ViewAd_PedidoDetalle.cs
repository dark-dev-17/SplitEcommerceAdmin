using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SplitAdminEcomerce.Views
{
    /// <summary>
    /// Listado de partidas
    /// </summary>
    [DarkTable(Name = "ViewAd_PedidoDetalle", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class ViewAd_PedidoDetalle
    {
        [Display(Name = "No.Pedido")]
        [DarkColumn(Name = "id_cotizacion", IsMapped = true, IsKey = true)]
        public int IdPedido { get; set; }

        [Display(Name = "Codigo")]
        [DarkColumn(Name = "codigo", IsMapped = true, IsKey = false)]
        public string Codigo { get; set; }

        [Display(Name = "Descripción")]
        [DarkColumn(Name = "desc_producto", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        [Display(Name = "Cantidad")]
        [DarkColumn(Name = "cantidad", IsMapped = true, IsKey = false)]
        public float Cantidad { get; set; }

        [Display(Name = "Sub total")]
        [DarkColumn(Name = "subtotal", IsMapped = true, IsKey = false)]
        public float Subtotal { get; set; }

        [Display(Name = "Total")]
        [DarkColumn(Name = "total", IsMapped = true, IsKey = false)]
        public float Total { get; set; }

        [Display(Name = "Moneda")]
        [DarkColumn(Name = "currency", IsMapped = true, IsKey = false)]
        public string Moneda { get; set; }

        [Display(Name = "Descuento")]
        [DarkColumn(Name = "descuento", IsMapped = true, IsKey = false)]
        public float Descuento { get; set; }

        [Display(Name = "Iva")]
        [DarkColumn(Name = "iva", IsMapped = true, IsKey = false)]
        public float Iva { get; set; }

        [Display(Name = "Activo")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "Imagen")]
        [DarkColumn(Name = "img_principal", IsMapped = true, IsKey = false)]
        public string ImgPrincipal { get; set; }
    }
}
