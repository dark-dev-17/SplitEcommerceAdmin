using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Uniones.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SplitAdminEcomerce.Controllers
{
    public class ContSeccionCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public ContSeccionCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.Cont_Seccion == null)
                Splittel.LoadObject(Enums.EcomObjects.Cont_Seccion);
            if (Splittel.Cont_SeccionArchivo == null)
                Splittel.LoadObject(Enums.EcomObjects.Cont_SeccionArchivo);

        }
        #endregion

        #region Metodos
        /// <summary>
        /// camiar posiciones de los arcvivos
        /// </summary>
        /// <param name="elementoOrders"></param>
        public void UpdatePosiciones(List<ElementoOrder> elementoOrders)
        {
            if (elementoOrders is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, no se enviaron correctamente las posiciones nuevas", ErrorCode = 10 };
            }

            elementoOrders.ForEach(elem => {
               var result =  Splittel.Cont_SeccionArchivo.GetOpenquery($"where  t39_pk01 = {elem.Key}");
                if(result != null)
                {
                    result.Posicion = elem.Posicion;

                    Splittel.Cont_SeccionArchivo.Element = result;
                    if (!Splittel.Cont_SeccionArchivo.Update())
                    {
                        throw new SplitException
                        {
                            Category = TypeException.Info,
                            Description = $"Error, no fue actualizado tu archivo",
                            ErrorCode = 100
                        };
                    }
                }

            });
        }

        /// <summary>
        /// editar archivo
        /// </summary>
        /// <param name="_SeccionArchivo"></param>
        public void EditSeccionFile(Cont_SeccionArchivo _SeccionArchivo)
        {
            string File = "";
            try
            {
                if (_SeccionArchivo is null)
                {
                    throw new SplitException
                    {
                        Category = TypeException.Info,
                        Description = $"Error, los datos no fueron enviados",
                        ErrorCode = 100
                    };
                }

                #region Extraer archivo de seccion
                var cont_Seccion_re = Splittel.Cont_SeccionArchivo.GetOpenquery($"where t39_pk01 = {_SeccionArchivo.IdCont_SeccionArchivo}");
                if (cont_Seccion_re is null)
                {
                    throw new SplitException
                    {
                        Category = TypeException.Info,
                        Description = $"Error, no se encontro el archivo a editar",
                        ErrorCode = 100
                    };
                }
                #endregion

                #region Get seccion
                var Seccion_re = GetCont_Seccion(_SeccionArchivo.IdCont_Seccion);
                if (Seccion_re is null)
                {
                    throw new SplitException { Category = TypeException.Info, Description = $"No existe la seccion a la cual deseas agregar un archivo", ErrorCode = 10, IdAux = "" };
                }
                #endregion

                if (_SeccionArchivo.Archivo != null)
                {
                    if (_SeccionArchivo.Archivo.Length <= 0)
                        throw new SplitException { Category = TypeException.Info, Description = $"el archivo {_SeccionArchivo.Archivo.FileName} esta dañado", ErrorCode = 10, IdAux = "Archivo" };

                    string RutaImg = $"public_html/{cont_Seccion_re.ArchivoPath}";
                    if (Splittel.FtpServ.ExistsFile(RutaImg))
                    {
                        Splittel.FtpServ.DeleteFile(RutaImg, "Archivo");
                    }

                    RutaImg = $"public_html/{Seccion_re.PathFtp.Trim()}/img_{_SeccionArchivo.IdCont_SeccionArchivo}.{_SeccionArchivo.Archivo.FileName.Split('.')[1]}";
                    Splittel.FtpServ.UpdateFile(RutaImg, _SeccionArchivo.Archivo);

                    _SeccionArchivo.ArchivoPath = $"{Seccion_re.PathFtp.Trim()}/img_{_SeccionArchivo.IdCont_SeccionArchivo}.{_SeccionArchivo.Archivo.FileName.Split('.')[1]}";

                    File = _SeccionArchivo.ArchivoPath;
                }



                cont_Seccion_re.ArchivoPath = _SeccionArchivo.ArchivoPath;
                cont_Seccion_re.DescripcionWeb = _SeccionArchivo.DescripcionWeb;
                cont_Seccion_re.Url = _SeccionArchivo.Url;
                cont_Seccion_re.EsVisible = _SeccionArchivo.Che_EsVisible ? 1 : 0;
                cont_Seccion_re.AbrirNuevatab = _SeccionArchivo.Che_AbrirNuevatab ? 1 : 0;

                Splittel.Cont_SeccionArchivo.Element = cont_Seccion_re;
                if (!Splittel.Cont_SeccionArchivo.Update())
                {
                    throw new SplitException
                    {
                        Category = TypeException.Info,
                        Description = $"Error, no fue registrada tu seccion",
                        ErrorCode = 100
                    };
                }
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(File))
                {
                    Splittel.FtpServ.DeleteFile(File, _SeccionArchivo.Archivo.FileName);
                }
                throw;
            }
        }

        /// <summary>
        /// Agregar imagenes a seccion
        /// </summary>
        /// <param name="_SeccionArchivo"></param>

        public void AddSeccionFile(Cont_SeccionArchivo _SeccionArchivo)
        {
            string File = "";
            try
            {
                if (_SeccionArchivo is null)
                {
                    throw new SplitException
                    {
                        Category = TypeException.Info,
                        Description = $"Error, los datos no fueron enviados",
                        ErrorCode = 100
                    };
                }

                if (_SeccionArchivo.Archivo is null)
                    throw new SplitException { Category = TypeException.Info, Description = $"Por favor selecciona una imagen", ErrorCode = 10, IdAux = "Archivo" };
                if (_SeccionArchivo.Archivo.Length <= 0)
                    throw new SplitException { Category = TypeException.Info, Description = $"el archivo {_SeccionArchivo.Archivo.FileName} esta dañado", ErrorCode = 10, IdAux = "Archivo" };

                var Seccion_re = GetCont_Seccion(_SeccionArchivo.IdCont_Seccion);
                if (Seccion_re is null)
                {
                    throw new SplitException { Category = TypeException.Info, Description = $"No existe la seccion a la cual deseas agregar un archivo", ErrorCode = 10, IdAux = "" };
                }
                string FileRuta = $"public_html/{Seccion_re.PathFtp.Trim()}/img_{(Splittel.Cont_SeccionArchivo.GetLastId("t39_pk01") +1)}.{_SeccionArchivo.Archivo.FileName.Split('.')[1]}";
                Splittel.FtpServ.UpdateFile(FileRuta, _SeccionArchivo.Archivo);

                _SeccionArchivo.ArchivoPath = $"{Seccion_re.PathFtp.Trim()}/img_{(Splittel.Cont_SeccionArchivo.GetLastId("t39_pk01") + 1)}.{_SeccionArchivo.Archivo.FileName.Split('.')[1]}";
                File = _SeccionArchivo.ArchivoPath;

                Splittel.Cont_SeccionArchivo.Element = _SeccionArchivo;
                if (!Splittel.Cont_SeccionArchivo.Add())
                {
                    throw new SplitException
                    {
                        Category = TypeException.Info,
                        Description = $"Error, no fue registrada tu seccion",
                        ErrorCode = 100
                    };
                }
            }
            catch (SplitException)
            {
                if (!string.IsNullOrEmpty(File))
                {
                    Splittel.FtpServ.DeleteFile(File, _SeccionArchivo.Archivo.FileName);
                }
                throw;
            }
            
        }

        /// <summary>
        /// Agregar o editar seccion
        /// </summary>
        /// <param name="cont_Seccion">Datos de la seccion</param>
        /// <returns></returns>
        public int AddUpdSeccion(Cont_Seccion cont_Seccion)
        {
            if (cont_Seccion is null)
            {
                throw new SplitException
                {
                    Category = TypeException.Info,
                    Description = $"Error, los datos no fueron enviados",
                    ErrorCode = 100
                };
            }

            Splittel.Cont_Seccion.Element = cont_Seccion;

            if (cont_Seccion.IdCont_Seccion == 0)
            {
                if (!Splittel.Cont_Seccion.Add())
                {
                    throw new SplitException
                    {
                        Category = TypeException.Info,
                        Description = $"Error, no fue registrada tu seccion",
                        ErrorCode = 100
                    };
                }

                return Splittel.Cont_Seccion.GetLastId("t38_pk01");
            }
            else
            {
                if (!Splittel.Cont_Seccion.Update())
                {
                    throw new SplitException
                    {
                        Category = TypeException.Info,
                        Description = $"Error, no fue registrada tu seccion",
                        ErrorCode = 100
                    };
                }
                return cont_Seccion.IdCont_Seccion;
            }
        }

        /// <summary>
        /// Listar archivos de una seccion
        /// </summary>
        /// <param name="IdCont_Seccion"></param>
        /// <returns></returns>
        public List<Cont_SeccionArchivo> Get_SeccionArchivos(int IdCont_Seccion)
        {
            var Result = Splittel.Cont_SeccionArchivo.GetOpenquery($"where t38_pk01 = {IdCont_Seccion}", "order by t39_f003 asc");
            Result.ForEach(a =>
            {
                a.Che_AbrirNuevatab = a.AbrirNuevatab == 1 ? true : false;
                a.Che_EsVisible = a.EsVisible == 1 ? true : false;
            });
            return Result;
        }

        /// <summary>
        /// Obtener detalle de file
        /// </summary>
        /// <param name="IdCont_Seccion"></param>
        /// <param name="IdCont_SeccionArchivo"></param>
        /// <returns></returns>
        public Cont_SeccionArchivo Get_SeccionArchivo(int IdCont_Seccion, int IdCont_SeccionArchivo)
        {
            var result = Splittel.Cont_SeccionArchivo.GetOpenquery($"where t38_pk01 = {IdCont_Seccion} and t39_pk01 = {IdCont_SeccionArchivo}");
            if(result != null)
            {
                result.Che_AbrirNuevatab = result.AbrirNuevatab == 1 ? true : false;
                result.Che_EsVisible = result.EsVisible == 1 ? true : false;
            }
            
            return result;
        }

        /// <summary>
        /// Obtener detalle de seccion
        /// </summary>
        /// <param name="IdCont_Seccion"></param>
        /// <returns></returns>
        public Cont_Seccion GetCont_Seccion(int IdCont_Seccion)
        {
            var Result = Splittel.Cont_Seccion.GetOpenquery($"where t38_pk01 = {IdCont_Seccion}");
            return Result;
        }
        
        /// <summary>
        /// Listar Secciones
        /// </summary>
        /// <returns></returns>
        public List<Cont_Seccion> GetCont_Seccions()
        {
            var Result = Splittel.Cont_Seccion.Get();
            //Result.ForEach(a => { 
                
            //});
            return Result;
        }
        
        /// <summary>
        /// Terminar todo controller
        /// </summary>
        public void Terminar()
        {
            Splittel.Cont_Seccion = null;
            Splittel.Cont_SeccionArchivo = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
