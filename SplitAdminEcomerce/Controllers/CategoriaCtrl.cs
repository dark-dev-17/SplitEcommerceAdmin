using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
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

            if (Splittel.Categoria == null)
                Splittel.LoadObject(Enums.EcomObjects.Categoria);
        }
        #endregion

        #region Metodos
        public void Update(Categoria Categoria)
        {
            if(Categoria is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor envia datos correctos", ErrorCode = 404 };
            }

            Categoria.Activo = Categoria.Che_Activo ? "si" : "no";
            Categoria.ActivoInfoTec = Categoria.Che_ActivoInfoTec ? "si" : "no";
            Categoria.ActivoInfoTec2 = Categoria.Che_ActivoInfoTec2 ? "si" : "no";

            Splittel.Categoria.Element = Categoria;

            if (!Splittel.Categoria.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = "Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Obtener categorias
        /// </summary>
        /// <returns></returns>
        public List<Categoria> GetCategorias()
        {
            var result = Splittel.Categoria.Get();
            result.ForEach(cate => {
                cate.Che_Activo = cate.Activo == "si" ? true : false;
                cate.Che_ActivoInfoTec = cate.ActivoInfoTec == "si" ? true : false;
                cate.Che_ActivoInfoTec2 = cate.ActivoInfoTec2 == "si" ? true : false;
            });
            return result;
        }
        /// <summary>
        /// obtener categoria bajo clave seleccionada
        /// </summary>
        /// <param name="Clave"></param>
        /// <returns></returns>
        public Categoria GetCategorias(string Clave)
        {
            var result = Splittel.Categoria.GetString(Clave);
            if(result != null)
            {
                result.Che_Activo = result.Activo == "si" ? true : false;
                result.Che_ActivoInfoTec = result.ActivoInfoTec == "si" ? true : false;
                result.Che_ActivoInfoTec2 = result.ActivoInfoTec2 == "si" ? true : false;
            }
            return result;
        }
        /// <summary>
        /// libera objetos usados
        /// </summary>
        public void Terminar()
        {
            Splittel.Categoria = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
