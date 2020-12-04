using DbManagerDark.Attributes;
using SplitAdminEcomerce.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "menu_subcategorias", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class SubCategoria
    {
        [Display(Name = "Id")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = false)]
        public string IdSubCategoria { get; set; }

        [Required]
        [Display(Name = "Clave")]
        [DarkColumn(Name = "id_subcategoria", IsMapped = true, IsKey = true)]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [DarkColumn(Name = "desc_subcategoria", IsMapped = true, IsKey = false)]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        [DarkColumn(Name = "id_familia", IsMapped = true, IsKey = false)]
        public string IdFamilia { get; set; }

        [Display(Name = "Tiene configurables")]
        [DarkColumn(Name = "subnivel", IsMapped = true, IsKey = false)]
        public string SubNivel { get; set; }

        [Display(Name = "Activo en e-commerce")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [DarkColumn(Name = "descripcion", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }


        [Required]
        [Display(Name = "Tiene configurables")]
        [DarkColumn(Name = "subnivel", IsMapped = false, IsKey = false)]
        public bool Che_SubNivel { get; set; }

        [Required]
        [Display(Name = "Activo en e-commerce")]
        [DarkColumn(Name = "activo", IsMapped = false, IsKey = false)]
        public bool Che_Activo { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(Codigo + ""); } }

    }
}
