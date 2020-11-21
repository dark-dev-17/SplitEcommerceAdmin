using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class ProductoCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
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
        }
        #endregion

        #region Metodos
        public List<FileFtp> GetFileProducto(string Codigo)
        {
            string PathPublicItem = string.Format(@"images/img_spl/productos/{1}/", Splittel.FtpServ.Site, Codigo);
            var result = Splittel.FtpServ.Getfiles($"public_html/fibra-optica/public/images/img_spl/productos/{Codigo}/*.jpg", PathPublicItem);

            return result;
        }
        /// <summary>
        /// extraer producto seleccionado
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public Producto Get(string Codigo)
        {
            var result = Splittel.Producto.GetString(Codigo);
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
            else if(columna == "Descripcion")
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
            Splittel.DisConnect();
            Splittel.Terminar();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
