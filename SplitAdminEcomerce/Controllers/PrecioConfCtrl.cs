using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class PrecioConfCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public PrecioConfCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.PrecioCableServ == null)
                Splittel.LoadObject(Enums.EcomObjects.PrecioCableServ);
            if (Splittel.PrecioDistriPreca == null)
                Splittel.LoadObject(Enums.EcomObjects.PrecioDistriPreca);
            if (Splittel.PrecioDistriPrecon == null)
                Splittel.LoadObject(Enums.EcomObjects.PrecioDistriPrecon);
            if (Splittel.PrecioJumperCable == null)
                Splittel.LoadObject(Enums.EcomObjects.PrecioJumperCable);
            if (Splittel.PrecioJumperConect == null)
                Splittel.LoadObject(Enums.EcomObjects.PrecioJumperConect);
            if (Splittel.PrecioMPO == null)
                Splittel.LoadObject(Enums.EcomObjects.PrecioMPO);
            if (Splittel.PrecioPatchCord == null)
                Splittel.LoadObject(Enums.EcomObjects.PrecioPatchCord);
            if (Splittel.PrecioPigtail == null)
                Splittel.LoadObject(Enums.EcomObjects.PrecioPigtail);
        }
        #endregion

        #region Metodos

        #region Cables de servicio
        /// <summary>
        /// 
        /// </summary>
        /// <param name="precioCableServ"></param>
        public void EditPrecioCableServs(PrecioCableServ precioCableServ)
        {
            if(precioCableServ is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, por favor envia correctamente los datos", ErrorCode = 100 };
            }

            var res_re = Splittel.PrecioCableServ.Get(precioCableServ.IdPrecioCableServ);
            if(res_re == null)
                throw new SplitException { Category = TypeException.Info, Description = $"Error, no se encontró el componente", ErrorCode = 100 };
            res_re.Precio = precioCableServ.Precio;
            Splittel.PrecioCableServ.Element = res_re;
            if (!Splittel.PrecioCableServ.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Obtener lista de precios de componentes
        /// </summary>
        /// <returns></returns>
        public List<PrecioCableServ> GetPrecioCableServs()
        {
            var result_re = Splittel.PrecioCableServ.GetOpenquery("", "Order by componente");
            return result_re;
        }
        public PrecioCableServ GetPrecioCableServ(int id)
        {
            var result_re = Splittel.PrecioCableServ.Get(id);
            return result_re;
        }
        #endregion

        #region Distribuidores precargados
        /// <summary>
        /// Editar precio de componente
        /// </summary>
        /// <param name="PrecioDistriPreca"></param>
        public void EditPrecioDistriPreca(PrecioDistriPreca PrecioDistriPreca)
        {
            if (PrecioDistriPreca is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, por favor envia correctamente los datos", ErrorCode = 100 };
            }

            var res_re = Splittel.PrecioDistriPreca.Get(PrecioDistriPreca.IdPrecioDistriPreca);
            if (res_re == null)
                throw new SplitException { Category = TypeException.Info, Description = $"Error, no se encontró el componente", ErrorCode = 100 };
            res_re.Precio = PrecioDistriPreca.Precio;
            Splittel.PrecioDistriPreca.Element = res_re;
            if (!Splittel.PrecioDistriPreca.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Listar cprecios y componetes de producto seleccionado
        /// </summary>
        /// <returns></returns>
        public List<PrecioDistriPreca> GetPrecioDistriPrecas()
        {
            var result_re = Splittel.PrecioDistriPreca.GetOpenquery("", "Order by componente");
            return result_re;
        }

        public PrecioDistriPreca GetPrecioDistriPreca(int id)
        {
            var result_re = Splittel.PrecioDistriPreca.Get(id);
            return result_re;
        }
        #endregion

        #region Distribuidor preconectorizado
        /// <summary>
        /// Editar precio de componente
        /// </summary>
        /// <param name="PrecioDistriPrecon"></param>
        public void EditPrecioDistriPrecon(PrecioDistriPrecon PrecioDistriPrecon)
        {
            if (PrecioDistriPrecon is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, por favor envia correctamente los datos", ErrorCode = 100 };
            }

            var res_re = Splittel.PrecioDistriPrecon.Get(PrecioDistriPrecon.IdPrecioDistriPrecon);
            if (res_re == null)
                throw new SplitException { Category = TypeException.Info, Description = $"Error, no se encontró el componente", ErrorCode = 100 };
            res_re.Precio = PrecioDistriPrecon.Precio;
            Splittel.PrecioDistriPrecon.Element = res_re;
            if (!Splittel.PrecioDistriPrecon.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Listar cprecios y componetes de producto seleccionado
        /// </summary>
        /// <returns></returns>
        public List<PrecioDistriPrecon> GetPrecioDistriPrecons()
        {
            var result_re = Splittel.PrecioDistriPrecon.GetOpenquery("", "Order by componente");
            return result_re;
        }
        public PrecioDistriPrecon GetPrecioDistriPrecon(int id)
        {
            var result_re = Splittel.PrecioDistriPrecon.Get(id);
            return result_re;
        }
        #endregion

        #region jumper cable
        /// <summary>
        /// Editar precio de componente
        /// </summary>
        /// <param name="PrecioJumperCable"></param>
        public void EditPrecioJumperCable(PrecioJumperCable PrecioJumperCable)
        {
            if (PrecioJumperCable is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, por favor envia correctamente los datos", ErrorCode = 100 };
            }

            var res_re = Splittel.PrecioJumperCable.Get(PrecioJumperCable.IdPrecioJumperCable);
            if (res_re == null)
                throw new SplitException { Category = TypeException.Info, Description = $"Error, no se encontró el componente", ErrorCode = 100 };

            res_re.Precio = PrecioJumperCable.Precio;
            Splittel.PrecioJumperCable.Element = res_re;
            if (!Splittel.PrecioJumperCable.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Listar cprecios y componetes de producto seleccionado
        /// </summary>
        /// <returns></returns>
        public List<PrecioJumperCable> GetPrecioJumperCables()
        {
            var result_re = Splittel.PrecioJumperCable.GetOpenquery("", "Order by t91_pk01");
            result_re.ForEach(jum => {
                jum.TipoJumper_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoJumper}");
                jum.TipoFibra_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoFibra}");
                jum.TipoCubierta_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoCubierta}");
                jum.TipoHilo_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoHilo}");
            });
            return result_re;
        }
        public PrecioJumperCable GetPrecioJumperCable(int id)
        {
            var jum = Splittel.PrecioJumperCable.Get(id);
            if(jum != null)
            {
                jum.TipoJumper_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoJumper}");
                jum.TipoFibra_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoFibra}");
                jum.TipoCubierta_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoCubierta}");
                jum.TipoHilo_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoHilo}");
            }
            return jum;
        }
        #endregion

        #region jumper conector
        /// <summary>
        /// Editar precio de componente
        /// </summary>
        /// <param name="PrecioJumperConect"></param>
        public void EditPrecioJumperConect(PrecioJumperConect PrecioJumperConect)
        {
            if (PrecioJumperConect is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, por favor envia correctamente los datos", ErrorCode = 100 };
            }

            var res_re = Splittel.PrecioJumperConect.Get(PrecioJumperConect.IdPrecioJumperConect);
            if (res_re == null)
                throw new SplitException { Category = TypeException.Info, Description = $"Error, no se encontró el componente", ErrorCode = 100 };

            res_re.Precio = PrecioJumperConect.Precio;
            Splittel.PrecioJumperConect.Element = res_re;
            if (!Splittel.PrecioJumperConect.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Listar cprecios y componetes de producto seleccionado
        /// </summary>
        /// <returns></returns>
        public List<PrecioJumperConect> GetPrecioJumperConects()
        {
            var result_re = Splittel.PrecioJumperConect.GetOpenquery("", "Order by t91_pk01");
            result_re.ForEach(jum => {
                jum.TipoJumper_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoJumper}");
                jum.TipoFibra_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoFibra}");
                jum.TipoCubierta_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoCubierta}");
            });
            return result_re;
        }
        public PrecioJumperConect GetPrecioJumperConect(int id)
        {
            var jum = Splittel.PrecioJumperConect.Get(id);
            if (jum != null)
            {
                jum.TipoJumper_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoJumper}");
                jum.TipoFibra_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoFibra}");
                jum.TipoCubierta_ = Splittel.PrecioJumperCable.GetStringValueGen($"select t91_f002 from t91_subdefiniciones where t91_pk01 = {jum.TipoCubierta}");
            }
            return jum;
        }
        #endregion

        #region MPO
        /// <summary>
        /// Editar precio de componente
        /// </summary>
        /// <param name="PrecioMPO"></param>
        public void EditPrecioMPO(PrecioMPO PrecioMPO)
        {
            if (PrecioMPO is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, por favor envia correctamente los datos", ErrorCode = 100 };
            }

            var res_re = Splittel.PrecioMPO.Get(PrecioMPO.IdPrecioMPO);
            if (res_re == null)
                throw new SplitException { Category = TypeException.Info, Description = $"Error, no se encontró el componente", ErrorCode = 100 };


            res_re.Precio = PrecioMPO.Precio;
            Splittel.PrecioMPO.Element = res_re;
            if (!Splittel.PrecioMPO.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Listar cprecios y componetes de producto seleccionado
        /// </summary>
        /// <returns></returns>
        public List<PrecioMPO> GetPrecioMPOs()
        {
            var result_re = Splittel.PrecioMPO.GetOpenquery("", "Order by componente");
            return result_re;
        }
        public PrecioMPO GetPrecioMPO(int id)
        {
            var result_re = Splittel.PrecioMPO.Get(id);
            return result_re;
        }
        #endregion

        #region PatchCord
        /// <summary>
        /// Editar precio de componente
        /// </summary>
        /// <param name="PrecioPatchCord"></param>
        public void EditPrecioPatchCord(PrecioPatchCord PrecioPatchCord)
        {
            if (PrecioPatchCord is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, por favor envia correctamente los datos", ErrorCode = 100 };
            }

            var res_re = Splittel.PrecioPatchCord.Get(PrecioPatchCord.IdPrecioPatchCord);
            if (res_re == null)
                throw new SplitException { Category = TypeException.Info, Description = $"Error, no se encontró el componente", ErrorCode = 100 };

            res_re.Base = PrecioPatchCord.Base;
            Splittel.PrecioPatchCord.Element = res_re;
            if (!Splittel.PrecioPatchCord.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Listar cprecios y componetes de producto seleccionado
        /// </summary>
        /// <returns></returns>
        public List<PrecioPatchCord> GetPrecioPatchCords()
        {
            var result_re = Splittel.PrecioPatchCord.GetOpenquery("", "Order by tipo");
            return result_re;
        }
        public PrecioPatchCord GetPrecioPatchCord(int id)
        {
            var result_re = Splittel.PrecioPatchCord.Get(id);
            return result_re;
        }
        #endregion

        #region Pigtail
        /// <summary>
        /// Editar precio de componente
        /// </summary>
        /// <param name="PrecioPigtail"></param>
        public void EditPrecioPigtail(PrecioPigtail PrecioPigtail)
        {
            if (PrecioPigtail is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, por favor envia correctamente los datos", ErrorCode = 100 };
            }

            var res_re = Splittel.PrecioPigtail.Get(PrecioPigtail.IdPrecioPigtail);
            if (res_re == null)
                throw new SplitException { Category = TypeException.Info, Description = $"Error, no se encontró el componente", ErrorCode = 100 };


            res_re.Precio = PrecioPigtail.Precio;
            Splittel.PrecioPigtail.Element = res_re;
            if (!Splittel.PrecioPigtail.Update())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al actualizar", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Listar cprecios y componetes de producto seleccionado
        /// </summary>
        /// <returns></returns>
        public List<PrecioPigtail> GetPrecioPigtails()
        {
            var result_re = Splittel.PrecioPigtail.GetOpenquery("", "Order by tipo");
            return result_re;
        }
        public PrecioPigtail GetPrecioPigtail(int id)
        {
            var result_re = Splittel.PrecioPigtail.Get(id);
            return result_re;
        }
        #endregion

        /// <summary>
        /// LImpiar objeto
        /// </summary>
        public void Terminar()
        {
            Splittel.BlogComentario = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
