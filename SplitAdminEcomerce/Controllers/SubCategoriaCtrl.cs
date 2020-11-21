using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class SubCategoriaCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public SubCategoriaCtrl(Splittel Splittel)
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
