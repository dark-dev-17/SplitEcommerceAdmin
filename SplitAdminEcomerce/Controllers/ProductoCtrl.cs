using DbManagerDark.Managers;
using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Tools;
using SplitAdminEcomerce.Uniones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public enum ProductoTipoFile
    {
        Producto,
        Descripcion,
        InfoAdicional,
        Miniatura
    }
    public class ProductoCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        private Producto Producto;
        #endregion

        #region Constructores
        public ProductoCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.Producto == null)
                Splittel.LoadObject(Enums.EcomObjects.Producto);
            if (Splittel.ProductoBuscador == null)
                Splittel.LoadObject(Enums.EcomObjects.ProductoBuscador);
            if (Splittel.FichaTecnica == null)
                Splittel.LoadObject(Enums.EcomObjects.FichaTecnica);
            if (Splittel.DescripcionCompartida == null)
                Splittel.LoadObject(Enums.EcomObjects.DescripcionCompartida);
            if (Splittel.Categoria == null)
                Splittel.LoadObject(Enums.EcomObjects.Categoria);
            if (Splittel.SubCategoria == null)
                Splittel.LoadObject(Enums.EcomObjects.SubCategoria);
        }
        #endregion

        #region Metodos

        #region Archivos
        /// <summary>
        /// Renombrar archivo seleccionado
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="FileOld"></param>
        /// <param name="NameNew"></param>
        /// <param name="Tipo"></param>
        public void RenameFile(string Codigo, string FileOld, string NameNew, ProductoTipoFile Tipo = ProductoTipoFile.Producto)
        {
            if (string.IsNullOrEmpty(Codigo))
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un producto", ErrorCode = 100 };
            }

            string CodigoReal = Codigo;
            // Limpiar codigo de caracteres como [/] remplazar por  [-]

            Codigo = Codigo.Replace('-', '/');

            string Path = "";

            if (Tipo == ProductoTipoFile.Producto)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}";
            }
            else if (Tipo == ProductoTipoFile.Descripcion)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/descripcion";
            }
            else if (Tipo == ProductoTipoFile.InfoAdicional)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/adicional";
            }
            else if (Tipo == ProductoTipoFile.Miniatura)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/thumbnail";

            }
            NameNew = NameNew + "." + FileOld.Split('.')[1];

            Splittel.FtpServ.Rename(Path, FileOld, NameNew);

            if (Tipo == ProductoTipoFile.Miniatura)
            {
                UpdateMiniatura(CodigoReal, NameNew);
            }
        }
        /// <summary>
        /// Eliminar archivo seleccionado
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="FileName"></param>
        /// <param name="Tipo"></param>
        public void DeleteFile(string Codigo, string FileName, ProductoTipoFile Tipo = ProductoTipoFile.Producto)
        {
            if (string.IsNullOrEmpty(Codigo))
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un producto", ErrorCode = 100 };
            }

            string CodigoReal = Codigo;
            // Limpiar codigo de caracteres como [/] remplazar por  [-]

            Codigo = Codigo.Replace('-', '/');

            string Path = "";

            if (Tipo == ProductoTipoFile.Producto)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/{FileName}";
            }
            else if (Tipo == ProductoTipoFile.Descripcion)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/descripcion/{FileName}";
            }
            else if (Tipo == ProductoTipoFile.InfoAdicional)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/adicional/{FileName}";
            }
            else if (Tipo == ProductoTipoFile.Miniatura)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/thumbnail/{FileName}";

            }
            Splittel.FtpServ.DeleteFile(Path, FileName);

            if (Tipo == ProductoTipoFile.Miniatura)
            {
                UpdateMiniatura(CodigoReal, "");
            }

        }
        /// <summary>
        /// Subir archivos a directorio del producto
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="FormFile"></param>
        /// <param name="Tipo"></param>
        public string UpdateFile(string Codigo, IFormFile FormFile, ProductoTipoFile Tipo = ProductoTipoFile.Producto)
        {
            if (string.IsNullOrEmpty(Codigo))
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un producto", ErrorCode = 100 };
            }

            if(FormFile.Length <= 0)
            {
                throw new SplitException { Category = TypeException.Info, Description = "Tu archivo seleccionado esta dañado", ErrorCode = 100 };
            }

            string CodigoReal = Codigo;
            // Limpiar codigo de caracteres como [/] remplazar por  [-]

            Codigo = Codigo.Replace('-', '/');

            string Path = "";
            string RutePublic = "";

            string nombreNuewFile = FormFile.FileName;

            if (Tipo == ProductoTipoFile.Producto)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/{nombreNuewFile}";
                RutePublic = $"{Splittel.FtpServ.Site}images/img_spl/productos/{Codigo}/{nombreNuewFile}";
            }
            else if (Tipo == ProductoTipoFile.Descripcion)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/descripcion/{nombreNuewFile}";
                RutePublic = $"{Splittel.FtpServ.Site}images/img_spl/productos/{Codigo}/descripcion/{nombreNuewFile}";
            }
            else if (Tipo == ProductoTipoFile.InfoAdicional)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/adicional/{nombreNuewFile}";
                RutePublic = $"{Splittel.FtpServ.Site}images/img_spl/productos/{Codigo}/adicional/{nombreNuewFile}";
            }
            else if (Tipo == ProductoTipoFile.Miniatura)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/thumbnail/{nombreNuewFile}";
                RutePublic = $"{Splittel.FtpServ.Site}images/img_spl/productos/{Codigo}/thumbnail/{nombreNuewFile}";
            }
            Splittel.FtpServ.UpdateFile(Path, FormFile);

            if (Tipo == ProductoTipoFile.Miniatura)
            {
                UpdateMiniatura(CodigoReal, nombreNuewFile);
            }

            return RutePublic;
        }
        /// <summary>
        /// Extraer imagenes de acuerdo a la seccion selccionada(variable Tipo)
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="Tipo"></param>
        /// <returns></returns>
        public ImagenesSeccion GetFileProducto(string Codigo, ProductoTipoFile Tipo = ProductoTipoFile.Producto)
        {
            if (string.IsNullOrEmpty(Codigo))
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un producto", ErrorCode = 100 };
            }
            // Limpiar codigo de caracteres como [/] remplazar por  [-]
            string CodigoReal = Codigo;
            Codigo = Codigo.Replace('-', '/');
            string Path = "";
            string RutePublic = "";
            if (Tipo == ProductoTipoFile.Producto)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/*.jpg";
                RutePublic = string.Format(@"images/img_spl/productos/{0}/", Codigo);
            }
            else if (Tipo == ProductoTipoFile.Descripcion)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/descripcion/*.jpg";
                RutePublic = string.Format(@"images/img_spl/productos/{0}/descripcion/", Codigo);
            }
            else if (Tipo == ProductoTipoFile.InfoAdicional)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/adicional/*.jpg";
                RutePublic = string.Format(@"images/img_spl/productos/{0}/adicional/", Codigo);
            }
            else if (Tipo == ProductoTipoFile.Miniatura)
            {
                Path = $"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/thumbnail/*.jpg";
                RutePublic = string.Format(@"images/img_spl/productos/{0}/thumbnail/", Codigo);
            }

            var result = Splittel.FtpServ.Getfiles(Path, RutePublic);

            result.ForEach(a=> a.Seccion = Tipo.ToString());

            return new ImagenesSeccion { Tipo = Tipo, Imagenes = result };
        }
        #endregion


        #region Informacion
        public void UpdateMiniatura(string codigo, string filename)
        {
            List<ActionsMode> valore = new List<ActionsMode>();
            valore.Add(new ActionsMode { Columnname = Splittel.Producto.ColumName("ImgPrincipal"), ColumPR = "ImgPrincipal", Value = filename });
            Splittel.Producto.Update(valore, $"codigo = '{codigo}'");
        }
        public void Activar(string codigo, bool active)
        {
            List<ActionsMode> valore = new List<ActionsMode>();
            valore.Add(new ActionsMode { Columnname = Splittel.Producto.ColumName(nameof(Splittel.Producto.Element.Activo)), ColumPR = "Activo", Value = (active ? "si" : "no") });
            Splittel.Producto.Update(valore, $"codigo = '{codigo}'");
        }
        /// <summary>
        /// obtener Categoria de acuerdo al producto seleccionado
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public SubCategoria GetSubCategoria(string Codigo, string clave = "")
        {
            if (string.IsNullOrEmpty(clave))
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un producto", ErrorCode = 100 };
                }
                var result = Splittel.SubCategoria.GetOpenquery($"where id_subcategoria = (select subcategoria from catalogo_productos where codigo = '{Codigo}')");
                return result;
            }
            else
            {
                var result = Splittel.SubCategoria.GetOpenquery($"where id_subcategoria = '{clave}'");
                return result;
            }
        }
        /// <summary>
        /// obtener Categoria de acuerdo al producto seleccionado
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public Categoria GetCategoria(string Codigo, string clave = "")
        {
            if (string.IsNullOrEmpty(clave))
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un producto", ErrorCode = 100 };
                }
                var result = Splittel.Categoria.GetOpenquery($"where Id_codigo = (select categoria from catalogo_productos where codigo = '{Codigo}')");
                return result;
            }
            else
            {
                var result = Splittel.Categoria.GetOpenquery($"where Id_codigo = '{clave}'");
                return result;
            }
        }
        /// <summary>
        /// obtener descripcion de acuerdo al producto seleccionado
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public DescripcionCompartida GetDescripcionCompartida(string Codigo, string clave = "")
        {
            if (string.IsNullOrEmpty(clave))
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un producto", ErrorCode = 100 };
                }
                var result = Splittel.DescripcionCompartida.GetOpenquery($"where id_desc_larga = (select id_desc_larga from catalogo_productos where codigo = '{Codigo}')");
                return result;
            }
            else
            {
                var result = Splittel.DescripcionCompartida.GetOpenquery($"where id_desc_larga = '{clave}'");
                return result;
            }
        }
        /// <summary>
        /// obtener ficha tecnica de acuerdo al producto seleccionado
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        public FichaTecnica GetFichaTecnica(string Codigo, string clave = "")
        {
            if (string.IsNullOrEmpty(clave))
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un producto", ErrorCode = 100 };
                }
                var result = Splittel.FichaTecnica.GetOpenquery($"where id_ficha = (select id_producto from u_producto_ficha where id_ficha = (select info_tecnica from catalogo_productos where codigo = '{Codigo}'))");
                if (result != null)
                {
                    result.Rutapath = $"{Splittel.FtpServ.Site}/images/img_spl/{result.Rutapath}";
                }
                return result;
            }
            else
            {
                var result = Splittel.FichaTecnica.GetOpenquery($"where id_ficha = '{clave}'");
                if (result != null)
                {
                    result.Rutapath = $"{Splittel.FtpServ.Site}/images/img_spl/{result.Rutapath}";
                }
                return result;
            }
        }
        /// <summary>
        /// extraer producto seleccionado
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public Producto Get(string Codigo)
        {
            if (string.IsNullOrEmpty(Codigo))
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un producto", ErrorCode = 100 };
            }
            var result = Splittel.Producto.GetString(Codigo);
            if (result != null)
            {
                result.View360 = $"{Splittel.FtpServ.Site}images/img_spl/productos/{Codigo}/360/{Codigo}.html";
            }
            return result;
        }
        /// <summary>
        /// extraer listado de productos
        /// </summary>
        /// <returns></returns>
        public List<Producto> Get()
        {
            var result = Splittel.Producto.Get();

            return result;
        }
        /// <summary>
        /// extraer listado de productos en base a una busqueda
        /// </summary>
        /// <returns></returns>
        public List<ProductoBuscador> Buscador(string Patron, string columna)
        {
            if (string.IsNullOrEmpty(columna))
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona un tipo de busqueda", ErrorCode = 100 };
            }
            var result = new List<ProductoBuscador>();
            if (columna == "Codigo")
            {
                result = Splittel.ProductoBuscador.GetOpenquery($"where codigo like '%{Patron}%'", "Order by desc_producto asc");
            }
            else if (columna == "Descripcion")
            {
                result = Splittel.ProductoBuscador.GetOpenquery($"where desc_producto like '%{Patron}%'", "Order by desc_producto asc");
            }
            return result;
        }
        /// <summary>
        /// libera objetos usados
        /// </summary>
        public void Terminar()
        {
            Splittel.Producto = null;
            Splittel.ProductoBuscador = null;
            Splittel.FichaTecnica = null;
            Splittel.DescripcionCompartida = null;
            Splittel.SubCategoria = null;
            Splittel.Categoria = null;
            Splittel.DisConnect();
            Splittel.Terminar();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion

        #endregion
    }
}
