using DbManagerDark.Attributes;
using SplitAdminEcomerce.Tools;
using System.ComponentModel.DataAnnotations;
namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "catalogo_fichas_tecnicas", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class FichaTecnica
    {
        [Display(Name = "Id")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdFichaTecnica { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "id_ficha", IsMapped = true, IsKey = false)]
        public string Clave { get; set; }

        [Display(Name = "Ruta Archivo")]
        [DarkColumn(Name = "ruta", IsMapped = true, IsKey = false)]
        public string Rutapath { get; set; }
    }
}
