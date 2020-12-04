using SplitAdminEcomerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class BlogCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public BlogCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.Blog == null)
                Splittel.LoadObject(Enums.EcomObjects.Blog);
            if (Splittel.BlogBuscador == null)
                Splittel.LoadObject(Enums.EcomObjects.BlogBuscador);
            if (Splittel.BlogComentario == null)
                Splittel.LoadObject(Enums.EcomObjects.BlogComentario);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Detalle de blog
        /// </summary>
        /// <param name="IdBlog"></param>
        /// <returns></returns>
        public Blog GetBlog(int IdBlog)
        {
            var result = Splittel.Blog.Get(IdBlog);
            if(result != null)
            {
                result.Che_Activo = result.Activo == "1" ? true : false;
            }

            return result;
        }
        /// <summary>
        /// Obtener listado de blogs
        /// </summary>
        /// <returns></returns>
        public List<BlogBuscador> Getblogs()
        {
            var result = Splittel.BlogBuscador.Get();
            return result;
        }
        public void Terminar()
        {
            Splittel.Blog = null;
            Splittel.BlogBuscador = null;
            Splittel.BlogComentario = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
