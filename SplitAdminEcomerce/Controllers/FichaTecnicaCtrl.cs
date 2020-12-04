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
        public FTPDirectorio ListDirectory(string Folder)
        {
            bool isroot = true;
            if (string.IsNullOrEmpty(Folder) || Folder == RootPath)
            {
                Folder = RootPath;
                isroot = false;
            }

            var Result = Splittel.FtpServ.ListDirectory(Folder, isroot);
            if(Result.PathFolder == RootPath)
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
            Result.PathFolder = Result.PathFolder.Replace("../", "");
            Result.Contenido.ForEach(ficha => {
                ficha.Datos = Splittel.FichaTecnica.GetOpenquery($" where ruta = '{ficha.PathServer.Replace("public_html/fibra-optica/public/images/img_spl/","").Replace(".pdf","").Replace(".PDF", "")}'");
            });
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
