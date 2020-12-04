using DbManagerDark.Attributes;
using SplitAdminEcomerce.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "menu_subcategorias_n1", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Configurable
    {
        [Display(Name = "Id")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = false)]
        public string IdConfigurable { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        [DarkColumn(Name = "id_categoria", IsMapped = true, IsKey = false)]
        public string IdCategoria { get; set; }


        [Required]
        [Display(Name = "Sub-Categoria")]
        [DarkColumn(Name = "id_subcategoria", IsMapped = true, IsKey = false)]
        public string IdSubCategoria { get; set; }


        [Required]
        [Display(Name = "Código")]
        [DarkColumn(Name = "codigo", IsMapped = true, IsKey = true)]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [DarkColumn(Name = "desc_subcategoria", IsMapped = true, IsKey = false)]
        public string Descripcio { get; set; }

        [Display(Name = "Directorio Site")]
        [DarkColumn(Name = "folder_name", IsMapped = true, IsKey = false)]
        public string FolderDirectory { get; set; }

        [Display(Name = "Clave código")]
        [DarkColumn(Name = "cable_codigo", IsMapped = true, IsKey = false)]
        public string ClaveCodigo { get; set; }

        [Display(Name = "Nivel configurable")]
        [DarkColumn(Name = "nivel_configurable", IsMapped = true, IsKey = false)]
        public int NivelConf { get; set; }

        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "Activo proximamente")]
        [DarkColumn(Name = "configuracion", IsMapped = true, IsKey = false)]
        public int IsProximamente { get; set; }

        [Required]
        [Display(Name = "Descuento")]
        [DarkColumn(Name = "descuento", IsMapped = true, IsKey = false)]
        public int Descuento { get; set; }

        [Required]
        [Display(Name = "Activo E-commerce")]
        [DarkColumn(Name = "descuento", IsMapped = false, IsKey = false)]
        public bool Che_Activo { get; set; }

        [Required]
        [Display(Name = "Activo proximamente")]
        [DarkColumn(Name = "descuento", IsMapped = false, IsKey = false)]
        public bool Che_IsProximamente { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(Codigo + ""); } }
    }
}
