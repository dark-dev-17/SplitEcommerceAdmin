using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SplitAdminEcomerce.Views
{
    /// <summary>
    /// Listado de clientes
    /// </summary>
    [DarkTable(Name = "ViewAd_Clientes", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class ViewAd_Clientes
    {
        [Display(Name = "No.Cliente")]
        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = true)]
        public int IdCliente { get; set; }

        [Display(Name = "Cliente")]
        [DarkColumn(Name = "nombre", IsMapped = true, IsKey = false)]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos")]
        [DarkColumn(Name = "apellidos", IsMapped = true, IsKey = false)]
        public string Apellidos { get; set; }

        [Display(Name = "telefono")]
        [DarkColumn(Name = "telefono", IsMapped = true, IsKey = false)]
        public string Telefono { get; set; }

        [Display(Name = "Email")]
        [DarkColumn(Name = "email", IsMapped = true, IsKey = false)]
        public string Email { get; set; }

        [Display(Name = "Fe.registro")]
        [DarkColumn(Name = "fecha_registro", IsMapped = true, IsKey = false)]
        public DateTime FechaRegistro { get; set; }

        [Display(Name = "Fe.Ult ingreso")]
        [DarkColumn(Name = "last_login", IsMapped = true, IsKey = false)]
        public DateTime FechaUltimoIngre { get; set; }

        [Display(Name = "Estatus")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "Tipo Cliente")]
        [DarkColumn(Name = "tipo_cliente", IsMapped = true, IsKey = false)]
        public string TipoCliente { get; set; }

        [Display(Name = "Cod.Ctrl")]
        [DarkColumn(Name = "cardcode", IsMapped = true, IsKey = false)]
        public string CardCode { get; set; }

        [Display(Name = "Email.Ejecutivo")]
        [DarkColumn(Name = "email_ejecutivo", IsMapped = true, IsKey = false)]
        public string EmailEjecutivo { get; set; }

        [Display(Name = "GroupCode")]
        [DarkColumn(Name = "groupcode", IsMapped = true, IsKey = false)]
        public int GroupCode { get; set; }

        [Display(Name = "Segmento")]
        [DarkColumn(Name = "segmento", IsMapped = true, IsKey = false)]
        public string Segmento { get; set; }

        [Display(Name = "IdCliente")]
        [DarkColumn(Name = "segmento", IsMapped = false, IsKey = false)]
        public string Crypptedid { get { return Tools.EncryptData.Encrypt(IdCliente + ""); } }

        [Display(Name = "Cliente")]
        [DarkColumn(Name = "sociedad", IsMapped = false, IsKey = false)]
        public string NombreFull { get { return string.Format("{0} {1}", Nombre, Apellidos); } }
    }
}
