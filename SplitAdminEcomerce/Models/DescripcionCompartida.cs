using DbManagerDark.Attributes;
using SplitAdminEcomerce.Tools;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "catalogo_descripciones", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class DescripcionCompartida
    {
        [Display(Name = "Id")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = false)]
        public int IdDescripcionCompartida { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "id_desc_larga", IsMapped = true, IsKey = true)]
        public string Codigo { get; set; }

        [Display(Name = "Descripción")]
        [DarkColumn(Name = "desc_larga", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }

        [Display(Name = "Copygriting para SEO")]
        [DarkColumn(Name = "desc_ceo", IsMapped = true, IsKey = false)]
        public string CEO { get; set; }

        [Display(Name = "Codigo")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(Codigo + ""); } }
    }
}
