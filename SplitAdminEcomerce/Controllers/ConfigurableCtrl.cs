using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class ConfigurableCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        public Configurable Configurable { get; internal set; }
        #endregion

        #region Constructores
        public ConfigurableCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.Configurable == null)
                Splittel.LoadObject(Enums.EcomObjects.Configurable);
        }
        #endregion

        #region Metodos
        public void Update(Configurable configurable)
        {
            if (configurable is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor envia datos correctos", ErrorCode = 404 };
            }
            configurable.Activo = configurable.Che_Activo ? "si" : "no";
            configurable.IsProximamente = configurable.Che_IsProximamente ? 1 : 0;
            Splittel.Configurable.Element = configurable;

            if (!Splittel.Configurable.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = "Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Obtener configurable
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public Configurable GetConfigurable(string codigo)
        {
            var result = Splittel.Configurable.GetString(codigo);
            if(result!= null)
            {
                result.Che_Activo = result.Activo == "si" ? true : false;
                result.Che_IsProximamente = result.IsProximamente == 1 ? true : false;
            }
            return result;
        }
        /// <summary>
        /// Obtener lista de configurable ordenados por codigo
        /// </summary>
        /// <returns></returns>
        public List<Configurable> GetConfigurables()
        {
            var result = Splittel.Configurable.Get();

            return FixData(result);
        }
        /// <summary>
        /// LLenar varibles no mapeadas en la base de datos
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<Configurable> FixData(List<Configurable> result)
        {
            result.ForEach(cate => {
                cate.Che_Activo = cate.Activo == "si" ? true : false;
                cate.Che_IsProximamente = cate.IsProximamente == 1 ? true : false;
            });

            return result.OrderBy(subCat => subCat.Codigo).ToList();
        }
        /// <summary>
        /// libera objetos usados
        /// </summary>
        public void Terminar()
        {
            Splittel.Configurable = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
