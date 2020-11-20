using DbManagerDark.Attributes;
using SplitAdminEcomerce.Catalogos;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SplitAdminEcomerce.Views
{
    /// <summary>
    /// Vista, lista de pedido
    /// </summary>
    [DarkTable(Name = "ViewAd_Pedidos", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class ViewAd_Pedidos
    {
        [Display(Name ="No-Ecom")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdPedido { get; set; }

        [Display(Name = "No.Cliente")]
        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = false)]
        public int IdCliente { get; set; }

        [Display(Name = "Total")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "${0:00}")]
        [DarkColumn(Name = "total", IsMapped = true, IsKey = false)]
        public float Total { get; set; }

        [Display(Name = "Fecha")]
        [DarkColumn(Name = "fecha", IsMapped = true, IsKey = false)]
        public DateTime Fecha { get; set; }

        [Display(Name = "Moneda")]
        [DarkColumn(Name = "moneda_pago", IsMapped = true, IsKey = false)]
        public string Moneda { get; set; }

        [Display(Name = "MetodoPago")]
        [DarkColumn(Name = "metodo_pago", IsMapped = true, IsKey = false)]
        public string MetodoPago { get; set; }

        [Display(Name = "Estatus")]
        [DarkColumn(Name = "estatus", IsMapped = true, IsKey = false)]
        public string Estatus { get; set; }

        [Display(Name = "Activo")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "TipoCambio")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "${0:00}")]
        [DarkColumn(Name = "tipoCambio", IsMapped = true, IsKey = false)]
        public float TipoCambio { get; set; }

        [Display(Name = "Cliente")]
        [DarkColumn(Name = "nombre", IsMapped = true, IsKey = false)]
        public string NombreCliente { get; set; }

        [DarkColumn(Name = "apellidos", IsMapped = true, IsKey = false)]
        public string Apellidoscliente { get; set; }

        
        [DarkColumn(Name = "tipo_cliente", IsMapped = true, IsKey = false)]
        public string TipoCliente { get; set; }

        [DarkColumn(Name = "cardcode", IsMapped = true, IsKey = false)]
        public string CardCodeCliente { get; set; }

        [DarkColumn(Name = "sociedad", IsMapped = true, IsKey = false)]
        public string SociedadCliente { get; set; }

        [Display(Name = "Ws estatus")]
        [DarkColumn(Name = "EstatusWS", IsMapped = true, IsKey = false)]
        public int EstatusWS { get; set; }

        [Display(Name = "Cliente")]
        [DarkColumn(Name = "sociedad", IsMapped = false, IsKey = false)]
        public string NombreFull { get { return string.Format("{0} {1}", NombreCliente, Apellidoscliente); } }

        [Display(Name = "Folio Encripted")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdPedido+""); } }
    }
}
