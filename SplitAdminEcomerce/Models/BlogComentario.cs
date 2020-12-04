using DbManagerDark.Attributes;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SplitAdminEcomerce.Models
{
    [DarkTable(Name = "menu_blog_comentarios", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class BlogComentario
    {
        [Display(Name = "Clave")]
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdBlogComentario { get; set; }

        [Required]
        [Display(Name = "Blog")]
        [DarkColumn(Name = "id_blog", IsMapped = true, IsKey = false)]
        public int Idblog { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        [DarkColumn(Name = "id_cliente", IsMapped = true, IsKey = false)]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Replica")]
        [DarkColumn(Name = "id_comentario", IsMapped = true, IsKey = false)]
        public int IdComentarioReplica { get; set; }

        [Required]
        [Display(Name = "Comentario")]
        [DarkColumn(Name = "comentario", IsMapped = true, IsKey = false)]
        public string Comentario { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        [DarkColumn(Name = "tipo", IsMapped = true, IsKey = false)]
        public int Tipo { get; set; }

        [Display(Name = "Fecha")]
        [DarkColumn(Name = "fecha", IsMapped = true, IsKey = false)]
        public DateTime Fecha { get; set; }

        [Display(Name = "Activo")]
        [DarkColumn(Name = "activo", IsMapped = true, IsKey = false)]
        public string Activo { get; set; }

        [Display(Name = "Clave")]
        [DarkColumn(Name = "EncriptId", IsMapped = false, IsKey = false)]
        public string EncriptId { get { return EncryptData.Encrypt(IdBlogComentario + ""); } }
    }
}
