using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.ModelsSAP;
using SplitAdminEcomerce.Uniones;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class HomeSlideCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public HomeSlideCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.HomeSlide == null)
                Splittel.LoadObject(Enums.EcomObjects.HomeSlide);
            
        }
        #endregion

        #region Metodos
        public List<CardGroup> getGruposSap()
        {
            Splittel.Connect(Enums.DbAccess.SapBussinesOne);
            Splittel.LoadObject(Enums.SapB1Objects.CardGroup);
            var CardGroup_re = Splittel.CardGroup.GetOpenquery("", "ORDER BY GroupName");
            Splittel.DisConnect(Enums.DbAccess.SapBussinesOne);

            return CardGroup_re;
        }
        /// <summary>
        /// Editar slide
        /// </summary>
        /// <param name="HomeSlide">Datos de slide</param>
        public void Editar(HomeSlide HomeSlide)
        {
            string File1 = "";
            string File2 = "";
            try
            {
                var Slides_re = Splittel.HomeSlide.Get(HomeSlide.IdHomeSlide);
                if(Slides_re is null)
                {
                    throw new SplitException { Category = TypeException.Error, Description = $"Error, no se encontro sl slide seleccionado", ErrorCode = 100 };
                }

                if (HomeSlide.Imagen1 != null)
                {
                    if (HomeSlide.Imagen1.Length <= 0)
                    {
                        throw new SplitException { 
                            Category = TypeException.Error, 
                            Description = $"Error al subir el archivo: {HomeSlide.Imagen1.FileName}", 
                            ErrorCode = 100,
                            NameObject = "Imagen1"
                        };
                    }
                    else
                    {
                        if (Splittel.FtpServ.ExistsFile($"public_html/fibra-optica/public/images/img_spl/slide/img1/{Slides_re.Imagen1_name}"))
                        {
                            Splittel.FtpServ.DeleteFile($"public_html/fibra-optica/public/images/img_spl/slide/img1/{Slides_re.Imagen1_name}", Slides_re.Imagen1_name);
                        }

                        HomeSlide.Imagen1_name = $"anuncio_{HomeSlide.IdHomeSlide}.{HomeSlide.Imagen1.FileName.Split('.')[1]}";
                        if (Splittel.FtpServ.ExistsFile($"public_html/fibra-optica/public/images/img_spl/slide/img1/{HomeSlide.Imagen1_name}"))
                        {
                            Splittel.FtpServ.DeleteFile($"public_html/fibra-optica/public/images/img_spl/slide/img1/{HomeSlide.Imagen1_name}", HomeSlide.Imagen1_name);
                        }

                        
                        Splittel.FtpServ.UpdateFile($"public_html/fibra-optica/public/images/img_spl/slide/img1/{HomeSlide.Imagen1_name}", HomeSlide.Imagen1);
                        File1 = HomeSlide.Imagen1_name;
                    }
                }
                {
                    HomeSlide.Imagen1_name = Slides_re.Imagen1_name;
                }

                if (HomeSlide.Imagen2 != null)
                {
                    if (HomeSlide.Imagen2.Length <= 0)
                    {
                        throw new SplitException { 
                            Category = TypeException.Error, 
                            Description = $"Error al subir el archivo: {HomeSlide.Imagen2.FileName}", 
                            ErrorCode = 100,
                            NameObject = "Imagen2"
                        };
                    }
                    else
                    {
                        if (Splittel.FtpServ.ExistsFile($"public_html/fibra-optica/public/images/img_spl/slide/img2/{Slides_re.Imagen2_name}"))
                        {
                            Splittel.FtpServ.DeleteFile($"public_html/fibra-optica/public/images/img_spl/slide/img2/{Slides_re.Imagen2_name}", Slides_re.Imagen2_name);
                        }

                        HomeSlide.Imagen2_name = $"anuncio_{HomeSlide.IdHomeSlide}.{HomeSlide.Imagen2.FileName.Split('.')[1]}";
                        if (Splittel.FtpServ.ExistsFile($"public_html/fibra-optica/public/images/img_spl/slide/img2/{HomeSlide.Imagen2_name}"))
                        {
                            Splittel.FtpServ.DeleteFile($"public_html/fibra-optica/public/images/img_spl/slide/img2/{HomeSlide.Imagen2_name}", HomeSlide.Imagen2_name);
                        }
                        Splittel.FtpServ.UpdateFile($"public_html/fibra-optica/public/images/img_spl/slide/img2/{HomeSlide.Imagen2_name}", HomeSlide.Imagen2);
                        File2 = HomeSlide.Imagen2_name;
                    }
                }
                else
                {
                    HomeSlide.Imagen2_name = Slides_re.Imagen2_name;
                }

                if (HomeSlide.Segmento == "PUBLIC")
                {
                    HomeSlide.Regla = "ning@<@0";
                }
                else
                {
                    HomeSlide.Regla = "@@0";
                }

                if (HomeSlide.Regla.Contains("ning"))
                {
                    HomeSlide.Regla = "ning@<@0";
                }

                HomeSlide.Activo = HomeSlide.Che_active ? "si" : "no";
                HomeSlide.NewWindows1 = HomeSlide.Che_NewWindows1 ? "si" : "no";
                HomeSlide.NewWindows2 = HomeSlide.Che_NewWindows2 ? "si" : "no";

                Splittel.HomeSlide.Element = HomeSlide;
                if (!Splittel.HomeSlide.Update())
                {
                    throw new SplitException { Category = TypeException.Error, Description = $"Error al guardar los cambios", ErrorCode = 100 };
                }

            }
            catch (SplitException ex)
            {
                if (!string.IsNullOrEmpty(File1))
                {
                    Splittel.FtpServ.DeleteFile(File1, HomeSlide.Imagen1_name);
                }
                if (!string.IsNullOrEmpty(File2))
                {
                    Splittel.FtpServ.DeleteFile(File2, HomeSlide.Imagen2_name);
                }
                throw ex;
            }
        }
        /// <summary>
        /// Crear nuevo slide
        /// </summary>
        /// <param name="HomeSlide"></param>
        public int Crear(HomeSlide HomeSlide)
        {
            string File1 = "";
            string File2 = "";
            try
            {
                int MaxId = Splittel.HomeSlide.GetLastId("t35_pk01") + 1;

                #region validar archivos imagen
                if (HomeSlide.Imagen1 == null)
                {
                    throw new SplitException
                    {
                        Category = TypeException.Error,
                        Description = $"Por favor selecciona una imagen",
                        ErrorCode = 100,
                        NameObject = "Imagen1"
                    };
                }

                if (HomeSlide.Imagen2 == null)
                {
                    throw new SplitException
                    {
                        Category = TypeException.Error,
                        Description = $"Por favor selecciona una imagen",
                        ErrorCode = 100,
                        NameObject = "Imagen2"
                    };

                }
                if (HomeSlide.Imagen1 != null)
                {
                    if (HomeSlide.Imagen1.Length <= 0)
                    {
                        throw new SplitException
                        {
                            Category = TypeException.Error,
                            Description = $"Error al subir el archivo: {HomeSlide.Imagen1.FileName}",
                            ErrorCode = 100,
                            NameObject = "Imagen1"
                        };
                    }
                    else
                    {
                        HomeSlide.Imagen1_name = $"anuncio_{MaxId}.{HomeSlide.Imagen1.FileName.Split('.')[1]}";
                        Splittel.FtpServ.UpdateFile($"public_html/fibra-optica/public/images/img_spl/slide/img1/{HomeSlide.Imagen1_name}", HomeSlide.Imagen1);
                        File1 = HomeSlide.Imagen1_name;
                    }
                }

                if (HomeSlide.Imagen2 != null)
                {
                    if (HomeSlide.Imagen2.Length <= 0)
                    {
                        throw new SplitException
                        {
                            Category = TypeException.Error,
                            Description = $"Error al subir el archivo: {HomeSlide.Imagen2.FileName}",
                            ErrorCode = 100,
                            NameObject = "Imagen2"
                        };
                    }
                    else
                    {
                        HomeSlide.Imagen2_name = $"anuncio_{MaxId}.{HomeSlide.Imagen2.FileName.Split('.')[1]}";
                        Splittel.FtpServ.UpdateFile($"public_html/fibra-optica/public/images/img_spl/slide/img2/{HomeSlide.Imagen1_name}", HomeSlide.Imagen2);
                        File2 = HomeSlide.Imagen2_name;
                    }
                }
                #endregion

                if (HomeSlide.Segmento == "PUBLIC")
                {
                    HomeSlide.Regla = "ning@<@0";
                }
                else
                {
                    HomeSlide.Regla = "@@0";
                }

                if (HomeSlide.Regla.Contains("ning"))
                {
                    HomeSlide.Regla = "ning@<@0";
                }

                HomeSlide.Activo = HomeSlide.Che_active ? "si" : "no";
                HomeSlide.NewWindows1 = HomeSlide.Che_NewWindows1 ? "si" : "no";
                HomeSlide.NewWindows2 = HomeSlide.Che_NewWindows2 ? "si" : "no";

                Splittel.HomeSlide.Element = HomeSlide;
                if (!Splittel.HomeSlide.Add())
                {
                    throw new SplitException { Category = TypeException.Error, Description = $"Error al guardar los cambios", ErrorCode = 100 };
                }

                return Splittel.HomeSlide.GetLastId("t35_pk01");
            }
            catch (SplitException ex)
            {
                if (!string.IsNullOrEmpty(File1))
                {
                    Splittel.FtpServ.DeleteFile(File1, HomeSlide.Imagen1_name);
                }
                if (!string.IsNullOrEmpty(File2))
                {
                    Splittel.FtpServ.DeleteFile(File2, HomeSlide.Imagen2_name);
                }
                throw ex;
            }
        }
        /// <summary>
        /// Ordenar slides
        /// </summary>
        /// <param name="slidePosicions">Lista de slides con posicion</param>
        public void ChangePositions(List<SlidePosicion> slidePosicions)
        {
            if(slidePosicions is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error, por favor intenta de nuevo", ErrorCode = 100 };
            }

            slidePosicions.ForEach(slide=> {
                var Slides_re = Splittel.HomeSlide.Get(slide.IdHomeSlide);
                if(Slides_re != null)
                {
                    Slides_re.Posicion = slide.Posicion;
                    Splittel.HomeSlide.Element = Slides_re;
                    if (!Splittel.HomeSlide.Update())
                    {
                        throw new SplitException { Category = TypeException.Info, Description = $"Error, no se guardo el orden hecho", ErrorCode = 100 };
                    }
                }
            });
        }
        /// <summary>
        /// Detalles de slide home principal
        /// </summary>
        /// <param name="IdHomeSlide">Id del slide</param>
        /// <returns></returns>
        public HomeSlide GetHomeSlide(int IdHomeSlide)
        {
            var Slides_re = Splittel.HomeSlide.Get(IdHomeSlide);
            if(Slides_re != null)
            {
                Slides_re.Che_active = Slides_re.Activo == "si" ? true : false;
                Slides_re.Che_NewWindows1 = Slides_re.NewWindows1 == "si" ? true : false;
                Slides_re.Che_NewWindows2 = Slides_re.NewWindows2 == "si" ? true : false;
            }
            return Slides_re;
        }
        /// <summary>
        /// Listar slidesd el home principal
        /// </summary>
        /// <returns></returns>
        public List<HomeSlide> GetHomeSlides()
        {
            var Slides_re = Splittel.HomeSlide.GetOpenquery("", "Order by t35_pk01 desc");
            Slides_re.ForEach(a =>
            {
                a.Che_active = a.Activo == "si" ? true : false;
                a.Che_NewWindows1 = a.NewWindows1 == "si" ? true : false;
                a.Che_NewWindows2 = a.NewWindows2 == "si" ? true : false;
            });
            return Slides_re;
        }
        /// <summary>
        /// terminar metodo controllador y clase manager 
        /// </summary>
        public void Terminar()
        {
            Splittel.HomeSlide = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
