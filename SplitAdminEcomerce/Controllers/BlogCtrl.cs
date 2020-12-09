using SplitAdminEcomerce.Exceptions;
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
        public void Update(Blog blog)
        {
            if(blog is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor envia datos correctos", ErrorCode = 404 };
            }
            var result = Splittel.Blog.Get(blog.IdBlog);
            if (result is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = "No se encontró el blog seleccionado", ErrorCode = 404 };
            }
            //validar si se esta cambiando imagen1
            if(blog.ImgCaratulaFile != null)
            {
                if(blog.ImgCaratulaFile.Length <= 0)
                    throw new SplitException { Category = TypeException.Info, Description = $"el archivo {blog.ImgCaratulaFile.FileName} esta dañado", ErrorCode = 10, IdAux = "ImgCaratulaFile" };
                string RutaImg = $"public_html/fibra-optica/public/images/img_spl/blog/{result.ImgCaratula}";
                if (Splittel.FtpServ.ExistsFile(RutaImg))
                {
                    Splittel.FtpServ.DeleteFile(RutaImg, result.ImgCaratula);
                }
                RutaImg = $"public_html/fibra-optica/public/images/img_spl/blog/{blog.ImgCaratulaFile.FileName}";
                Splittel.FtpServ.UpdateFile(RutaImg, blog.ImgCaratulaFile);

                result.ImgCaratula = blog.ImgCaratulaFile.FileName;
            }
            //validar si se esta cambiando imagen2
            if (blog.ImgLandingFile != null)
            {
                if (blog.ImgLandingFile.Length <= 0)
                    throw new SplitException { Category = TypeException.Info, Description = $"el archivo {blog.ImgLandingFile.FileName} esta dañado", ErrorCode = 10, IdAux = "ImgLandingFile" };

                string RutaImg = $"public_html/fibra-optica/public/images/img_spl/blog/{result.ImgLanding}";
                if (Splittel.FtpServ.ExistsFile(RutaImg))
                {
                    Splittel.FtpServ.DeleteFile(RutaImg, result.ImgLanding);
                }
                RutaImg = $"public_html/fibra-optica/public/images/img_spl/blog/{blog.ImgLandingFile.FileName}";
                Splittel.FtpServ.UpdateFile(RutaImg, blog.ImgLandingFile);

                result.ImgLanding = blog.ImgLandingFile.FileName;
            }
            result.Titulo = blog.Titulo;
            result.Titulo2 = blog.Titulo2;
            result.WebDescripcion = blog.WebDescripcion;
            result.Contenido1 = blog.Contenido1;
            result.Contenido2 = blog.Contenido2;
            //result.ImgCaratula = blog.ImgCaratula;
            //result.ImgLanding = blog.ImgLanding;
            result.Fecha = DateTime.Now;
            result.Activo = blog.Che_Activo ? "si" : "no";
            Splittel.Blog.Element = result;

            if (!Splittel.Blog.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al actualizar, el blog {result.Titulo}", ErrorCode = 100 };
            }

        }
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
                result.Che_Activo = result.Activo == "si" ? true : false;
                result.ImgCaratula = $"{Splittel.FtpServ.Site}images/img_spl/blog/{result.ImgCaratula}";
                result.ImgLanding = $"{Splittel.FtpServ.Site}images/img_spl/blog/{result.ImgLanding}";
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
            result.ForEach(a => {
                a.ImgCaratula = $"{Splittel.FtpServ.Site}images/img_spl/blog/{a.ImgCaratula}";
                a.ImgLanding = $"{Splittel.FtpServ.Site}images/img_spl/blog/{a.ImgLanding}";
            });
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
