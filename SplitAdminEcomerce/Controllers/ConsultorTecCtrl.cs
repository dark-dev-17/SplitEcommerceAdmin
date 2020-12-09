using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class ConsultorTecCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public ConsultorTecCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.ConsultorTecnico == null)
                Splittel.LoadObject(Enums.EcomObjects.ConsultorTecnico);
            if (Splittel.ConsultorRespuestas == null)
                Splittel.LoadObject(Enums.EcomObjects.ConsultorRespuestas);
        }
        #endregion

        #region Metodos
        public void Terminar()
        {
            Splittel.ConsultorTecnico = null;
            Splittel.ConsultorRespuestas = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
