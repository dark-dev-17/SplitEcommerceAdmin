using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class DescricionCompartidaCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public DescricionCompartidaCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.DescripcionCompartida == null)
                Splittel.LoadObject(Enums.EcomObjects.DescripcionCompartida);

        }
        #endregion

         #region Metodos
        public void Update(DescripcionCompartida descripcionCompartida)
        {
            if(descripcionCompartida is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor selecciona una descripción", ErrorCode = 100 };
            }

            if(GetDescripcion(descripcionCompartida.IdDescripcionCompartida) == null)
            {
                throw new SplitException { Category = TypeException.Info, Description = "No se encontró la descripción", ErrorCode = 100 };
            }


            Splittel.DescripcionCompartida.Element = descripcionCompartida;
            if(Splittel.DescripcionCompartida.Update() == false)
            {
                throw new SplitException { Category = TypeException.Info, Description = "Error al Actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// extraer listado de descripciones
        /// </summary>
        /// <returns></returns>
        public List<DescripcionCompartida> GetDescripcion()
        {
            var result = Splittel.DescripcionCompartida.Get();
            return result;
        }
        /// <summary>
        /// extraer listado de descripciones
        /// </summary>
        /// <returns></returns>
        public List<DescripcionCompartida> Buscardor(string patron)
        {
            var result = Splittel.DescripcionCompartida.GetOpenquery($" where desc_larga like '%{patron}%'", " order by desc_larga asc limit 3");
            return result;
        }
        /// <summary>
        /// Extraer descripcion
        /// </summary>
        /// <param name="clave"></param>
        /// <returns></returns>
        public DescripcionCompartida GetDescripcion(int Id)
        {
            var result = Splittel.DescripcionCompartida.Get(Id);
            return result;
        }
        /// <summary>
        /// libera objetos usados
        /// </summary>
        public void Terminar()
        {

            Splittel.DescripcionCompartida = null;
            Splittel.DisConnect();
            Splittel.Terminar();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
