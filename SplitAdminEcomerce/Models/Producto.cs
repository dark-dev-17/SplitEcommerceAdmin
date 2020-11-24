using DbManagerDark.Attributes;
using SplitAdminEcomerce.Tools;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{

    [DarkTable(Name = "catalogo_productos", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class ProductoBuscador
    {
        [Display(Name = "Codigo")]
        [DarkColumn(Name = "codigo", IsMapped = true, IsKey = true)]
        public string Codigo { get; set; }

        [Display(Name = "Descripcion")]
        [DarkColumn(Name = "desc_producto", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        [Display(Name = "Codigo")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(Codigo + ""); } }
    }

    [DarkTable(Name = "catalogo_productos", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Producto
    {
        [Display(Name = "Codigo")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = false)]
        public int IdProducto { get; set; }

        [Display(Name = "Codigo")]
        [DarkColumn(Name = "codigo", IsMapped = true, IsKey = true)]
        public string Codigo { get; set; }

        [Display(Name = "Descripcion")]
        [DarkColumn(Name = "desc_producto", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        [Display(Name = "Descripción lagra")]
        [DarkColumn(Name = "id_desc_larga", IsMapped = true, IsKey = false)]
        public string IdDesclarga { get; set; }

        [Display(Name = "Imagen")]
        [DarkColumn(Name = "id_imagen", IsMapped = true, IsKey = false)]
        public string IdImagen { get; set; }

        [Display(Name = "Categoria")]
        [DarkColumn(Name = "categoria", IsMapped = true, IsKey = false)]
        public string IdCategoria { get; set; }

        [Display(Name = "Subcategoria")]
        [DarkColumn(Name = "subcategoria", IsMapped = true, IsKey = false)]
        public string IdSubcategoria { get; set; }

        [Display(Name = "Marca")]
        [DarkColumn(Name = "id_marca", IsMapped = true, IsKey = false)]
        public string IdMarca { get; set; }

        [Display(Name = "Valoraciones")]
        [DarkColumn(Name = "valoraciones", IsMapped = true, IsKey = false)]
        public string IdValoraciones { get; set; }

        [Display(Name = "Precio Unitario")]
        [DarkColumn(Name = "precio", IsMapped = true, IsKey = false)]
        public float PrecioUnitario { get; set; }

        [Display(Name = "Descuento")]
        [DarkColumn(Name = "descuento_producto ", IsMapped = true, IsKey = false)]
        public int Descuento { get; set; }

        [Display(Name = "Stock")]
        [DarkColumn(Name = "existencia", IsMapped = true, IsKey = false)]
        public int Stock { get; set; }

        [Display(Name = "Caracteristicas")]
        [DarkColumn(Name = "caracteristicas", IsMapped = true, IsKey = false)]
        public string IdCaraceristicas { get; set; }

        [Display(Name = "Información técnica")]
        [DarkColumn(Name = "info_tecnica", IsMapped = true, IsKey = false)]
        public string IdInfo_tecnica { get; set; }

        [Display(Name = "Novedades")]
        [DarkColumn(Name = "novedades", IsMapped = true, IsKey = false)]
        public string Novedades { get; set; }

        [Display(Name = "Hoja ténica")]
        [DarkColumn(Name = "hoja_tecnica", IsMapped = true, IsKey = false)]
        public string HojaTecnica { get; set; }

        [Display(Name = "Pesos y dimensiones")]
        [DarkColumn(Name = "pesos_dimensiones", IsMapped = true, IsKey = false)]
        public string PesoDimension { get; set; }

        [Display(Name = "Información adicional")]
        [DarkColumn(Name = "info_adicional", IsMapped = true, IsKey = false)]
        public string InfoAdicional { get; set; }

        [Display(Name = "Producto relacionados")]
        [DarkColumn(Name = "productos_relacionados", IsMapped = true, IsKey = false)]
        public string IdProdRelacionados { get; set; }

        [Display(Name = "Codigo configurable")]
        [DarkColumn(Name = "codigo_configurable", IsMapped = true, IsKey = false)]
        public string CodigoConfig { get; set; }

        [Display(Name = "Activo")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "Imagen")]
        [DarkColumn(Name = "img_principal ", IsMapped = true, IsKey = false)]
        public string ImgPrincipal { get; set; }

        [Display(Name = "¿Requiere costo de envio?")]
        [DarkColumn(Name = "costo_envio", IsMapped = true, IsKey = false)]
        public string ReqCosto { get; set; }

        [Display(Name = "Codigo")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(Codigo + ""); } }


        [Display(Name = "Ruta360")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string View360 { get; set; }
    }
}
