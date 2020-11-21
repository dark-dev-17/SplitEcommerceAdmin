using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class CategoriaCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public CategoriaCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.Producto == null)
                Splittel.LoadObject(Enums.EcomObjects.Producto);
        }
        #endregion

        #region Metodos

        #endregion
    }
}
