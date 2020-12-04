using SplitAdminEcomerce.Estructura;
using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            if (Splittel.SubCategoria == null)
                Splittel.LoadObject(Enums.EcomObjects.SubCategoria);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Actualizar subcategoria
        /// </summary>
        /// <param name="SubCategoria"></param>
        public void Update(SubCategoria SubCategoria)
        {
            if (SubCategoria is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = "Por favor envia datos correctos", ErrorCode = 404 };
            }

            SubCategoria.Activo = SubCategoria.Che_Activo ? "si" : "no";
            SubCategoria.SubNivel = SubCategoria.Che_SubNivel ? "SI" : "NO";

            Splittel.SubCategoria.Element = SubCategoria;

            if (!Splittel.SubCategoria.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = "Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Obtener categoria por codigo
        /// </summary>
        /// <param name="Clave"></param>
        /// <returns></returns>
        public SubCategoria GetSubCategoria(string Clave)
        {
            var result = Splittel.SubCategoria.GetString(Clave);
            if(result != null)
            {
                result.Che_Activo = result.Activo == "si" ? true : false;
                result.Che_SubNivel = result.SubNivel == "SI" ? true : false;
            }
            
            return result;
        }
        /// <summary>
        /// Obtener sub categorias
        /// </summary>
        /// <returns></returns>
        public List<SubCategoria> GetSubCategorias()
        {
            var result = Splittel.SubCategoria.Get();
            return FixData(result);
        }
        /// <summary>
        /// Obtener subcategorias de acuerdo a la categooria seleccionada
        /// </summary>
        /// <param name="Clave"></param>
        /// <returns></returns>
        public List<SubCategoria> GetSByCategorias(string Clave)
        {
            var result = Splittel.SubCategoria.GetOpenquery($" where id_familia = '{Clave}'","");
            return FixData(result);
        }
        /// <summary>
        /// LLenar varibles no mapeadas en la base de datos
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<SubCategoria> FixData(List<SubCategoria> result)
        {
            result.ForEach(cate => {
                cate.Che_Activo = cate.Activo == "si" ? true : false;
                cate.Che_SubNivel = cate.SubNivel == "SI" ? true : false;
            });

            return result.OrderBy(subCat => subCat.Nombre).ToList();
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
