using DbManagerDark.Attributes;
using SplitAdminEcomerce.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "menu_categorias", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class Categoria
    {
        [Display(Name = "Id")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = false)]
        public int IdCategoria { get; set; }

        [Required]
        [Display(Name = "Clave")]
        [DarkColumn(Name = "id_codigo", IsMapped = true, IsKey = true)]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [DarkColumn(Name = "desc_familia", IsMapped = true, IsKey = false)]
        public string DescripcionFamilia { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        [DarkColumn(Name = "descripcion", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        [Display(Name = "Order")]
        [DarkColumn(Name = "orden", IsMapped = true, IsKey = false)]
        public int Orden { get; set; }

        [Display(Name = "Imagen")]
        [DarkColumn(Name = "id_imagen", IsMapped = true, IsKey = false)]
        public string id_imagen { get; set; }

        [Display(Name = "Activo en E-commerce")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "Mostrar en información técnica")]
        [DarkColumn(Name = "menu1", IsMapped = true, IsKey = false)]
        public string ActivoInfoTec { get; set; }

        [Display(Name = "Mostrar en hojas técnicas")]
        [DarkColumn(Name = "menu2", IsMapped = true, IsKey = false)]
        public string ActivoInfoTec2 { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(Codigo + ""); } }


        [Required]
        [Display(Name = "Activo en E-commerce")]
        [DarkColumn(Name = "activo", IsMapped = false, IsKey = false)]
        public bool Che_Activo { get; set; }

        [Required]
        [Display(Name = "Mostrar en información técnica")]
        [DarkColumn(Name = "menu1", IsMapped = false, IsKey = false)]
        public bool Che_ActivoInfoTec { get; set; }

        [Required]
        [Display(Name = "Mostrar en hojas técnicas")]
        [DarkColumn(Name = "menu2", IsMapped = false, IsKey = false)]
        public bool Che_ActivoInfoTec2 { get; set; }

    }
}
