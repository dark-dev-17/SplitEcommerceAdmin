using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class AccessCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public AccessCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.Admin_moduloSubPer == null)
                Splittel.LoadObject(Enums.EcomObjects.Admin_moduloSubPer);
            
        }
        #endregion

        #region Metodos
        public bool ValidAction(int IdAction, int IdSplitnet)
        {
            var res = Splittel.Admin_moduloSubPer.GetOpenquery($"where IdAdmin_moduloSub = {IdAction} and IdSplinet = {IdSplitnet} and HasPermise = 1");
            if(res is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Terminar()
        {
            Splittel.Admin_moduloSubPer = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
