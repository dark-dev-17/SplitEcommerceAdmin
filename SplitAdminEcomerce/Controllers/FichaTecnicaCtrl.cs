using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class FichaTecnicaCtrl
    {
        #region Propiedades
        private const string RootPath = @"public_html/fibra-optica/public/images/img_spl/FICHAS TÉCNICAS/";
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public FichaTecnicaCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.FichaTecnica == null)
                Splittel.LoadObject(Enums.EcomObjects.FichaTecnica);

        }
        #endregion

        #region Metodos
        /// <summary>
        /// Registrar un archivo actual dentro del repositorio de fichas tecnicas
        /// </summary>
        /// <param name="Archivo"></param>
        /// <param name="PathCOntainer"></param>
        /// <returns></returns>
        public string RegisterFile(string Archivo, string PathCOntainer)
        {
            if (Archivo.ToUpper().Contains("PDF") == false)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Solo se permite la carga de archivos PDF", ErrorCode = 10, IdAux = "Archivo" };
            }
            //{ 0:0000}
            FichaTecnica fichaTecnica = new FichaTecnica
            {
                Clave = string.Format("HA{0:0000000}", (Splittel.FichaTecnica.GetLastId("id") + 1)),
                Rutapath = $"{PathCOntainer}{Archivo}"
            };
            fichaTecnica.Rutapath = fichaTecnica.Rutapath.Substring(1, fichaTecnica.Rutapath.Length - 5);
            Splittel.FichaTecnica.Element = fichaTecnica;

            if (!Splittel.FichaTecnica.Add())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al guardar tu archivo PDF", ErrorCode = 100 };
            }

            return fichaTecnica.Clave;
        }
        /// <summary>
        /// /Agregar un archivo a careta y registralo
        /// </summary>
        /// <param name="Archivo"></param>
        /// <param name="PathCOntainer"></param>
        public void AddFile(IFormFile Archivo, string PathCOntainer)
        {
            if(Archivo is null)
                throw new SplitException { Category = TypeException.Info, Description = $"Por favor selcciona un archivo PDF", ErrorCode = 10, IdAux = "Archivo" };
            if (Archivo.Length <= 0)
                throw new SplitException { Category = TypeException.Info, Description = $"El archivo {Archivo.FileName} esta dañado", ErrorCode = 10, IdAux = "Archivo" };
            if (Archivo.FileName.ToUpper().Contains("PDF") == false)
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Solo se permite la carga de archivos PDF", ErrorCode = 10, IdAux = "Archivo" };
            }
            //{ 0:0000}
            FichaTecnica fichaTecnica = new FichaTecnica {
                Clave = string.Format("HA{0:0000000}",(Splittel.FichaTecnica.GetLastId("id") + 1)),
                Rutapath = $"{PathCOntainer}{Archivo.FileName}"
            };
            fichaTecnica.Rutapath = fichaTecnica.Rutapath.Substring(1, fichaTecnica.Rutapath.Length - 5);

            Splittel.FtpServ.UpdateFile($"public_html/fibra-optica/public/images/img_spl{PathCOntainer}{Archivo.FileName}",Archivo);

            Splittel.FichaTecnica.Element = fichaTecnica;

            if (!Splittel.FichaTecnica.Add())
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al guardar tu archivo PDF", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// Listar contenido seleccionado
        /// </summary>
        /// <param name="Folder"></param>
        /// <param name="ForderRoot"></param>
        /// <returns></returns>
        public FTPDirectorio ListDirectory(string Folder,string ForderRoot = "FICHAS TÉCNICAS")
        {

            bool isroot = true;
            if (string.IsNullOrEmpty(Folder) || Folder == RootPath || !Folder.Contains(ForderRoot))
            {
                Folder = RootPath;
                isroot = true;
            }

            string[] allAddresses = Folder.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if(allAddresses[allAddresses.Length -1] == ForderRoot || allAddresses[allAddresses.Length - 1] == "..")
            {
                if(allAddresses[allAddresses.Length - 1] == "..")
                {
                    if(allAddresses[allAddresses.Length - 2] == ForderRoot)
                    {
                        Folder = RootPath;
                        isroot = true;
                    }
                }
                else
                {
                    Folder = RootPath;
                    isroot = true;
                }
                
            }

            allAddresses = Folder.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var Result = Splittel.FtpServ.ListDirectory(Folder, isroot);
            Result.ActualFoder = allAddresses[allAddresses.Length - 1];
            if (Result.PathFolder == RootPath)
            {
                Result.IsPathRoot = true;
                Result.PathLast = "";
                Result.PathFolder = RootPath;
            }
            else
            {
                Result.IsPathRoot = false;
                Result.PathFolder = Folder;
            }
            Result.PathFolder.Replace("../", "");
            Result.Contenido.ForEach(ficha => {
                ficha.Datos = Splittel.FichaTecnica.GetOpenquery($" where ruta = '{ficha.PathServer.Replace("public_html/fibra-optica/public/images/img_spl/","").Replace(".pdf","").Replace(".PDF", "")}'");
            });

            Result.ShortPath = Result.PathFolder.Replace("public_html/fibra-optica/public/images/img_spl", "");

            return Result;
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
