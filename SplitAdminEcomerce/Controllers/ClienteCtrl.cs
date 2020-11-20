using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class ClienteCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public ClienteCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();
            if(Splittel.ViewAd_Clientes == null)
                Splittel.LoadObject(Enums.EcomObjects.ViewAd_Clientes);
        }
        #endregion

        #region Metodos
        public List<ViewAd_Clientes> GetClientes()
        {
            var result = Splittel.ViewAd_Clientes.Get();
            return result;
        }
        public ViewAd_Clientes GetView(int id)
        {
            var result = Splittel.ViewAd_Clientes.Get(id);
            if(result is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = "Cliente no encontrado", ErrorCode = 404 };
            }
            return result;
        }
        /// <summary>
        /// libera objetos usados
        /// </summary>
        public void Terminar()
        {
            Splittel.ViewAd_Clientes = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
