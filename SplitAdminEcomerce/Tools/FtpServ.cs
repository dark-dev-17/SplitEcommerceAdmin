using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SplitAdminEcomerce.Tools
{
    public class FtpServ
    {
        #region Propiedades
        
        private string FTP_server;
        private string FTP_user;
        private string FTP_password;
        public string Dominio { get; internal set; }
        public string Site { get; internal set; }
        #endregion

        #region Constructores
        ~FtpServ()
        {

        }
        public FtpServ()
        {

        }
        public FtpServ(string FTP_server, string FTP_user, string FTP_password, string Dominio, string Site)
        {
            this.FTP_server = FTP_server;
            this.FTP_user = FTP_user;
            this.FTP_password = FTP_password;
            this.Dominio = Dominio;
            this.Site = Site;
        }
        #endregion

        #region Metodos
        public void UpdateFile(string path, IFormFile FormFile)
        {
            string Uri = "ftp://" + FTP_server + path;
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                if (ExistsFile(path))
                {
                    throw new SplitException { Category = TypeException.Info, Description = "Ya existe un archivo con el mismo nombre", ErrorCode = 100 };
                }
                request = (FtpWebRequest)WebRequest.Create(new Uri(Uri));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(FTP_user, FTP_password);
                using (Stream ftpStream = request.GetRequestStream())
                {
                    FormFile.CopyTo(ftpStream);
                }
            }
            catch (SplitException ex)
            {
                throw new SplitException { Category = TypeException.Info, Description = ex.Message, ErrorCode = 100 };
            }
            catch (Exception ex)
            {
                throw new SplitException { Category = TypeException.Info, Description = ex.Message, ErrorCode = 100 };
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
        }
        public bool ExistsFile(string PathFile)
        {
            string Uri = "ftp://" + FTP_server + PathFile;

            Stream responseStream = null;
            FtpWebResponse response = null;
            //StreamWriter writeStream = null;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(Uri));
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = new NetworkCredential(FTP_user, FTP_password);
                response = (FtpWebResponse)request.GetResponse();
                responseStream = response.GetResponseStream();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new SplitException { Category = TypeException.Info, Description = ex.Message, ErrorCode = 100 };
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }
        /// <summary>
        /// Listar archivos en base a una RL dada
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="publicRoute"></param>
        /// <returns></returns>
        public List<FileFtp> Getfiles(string pattern, string publicRoute)
        {
            string Uri = "ftp://" + FTP_server + pattern;

            Stream responseStream = null;
            StreamReader reader = null;
            FtpWebResponse response = null;
            List<FileFtp> lista = new List<FileFtp>();
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(Uri));
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(FTP_user, FTP_password);
                response = (FtpWebResponse)request.GetResponse();
                
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                while (reader.Peek() >= 0)
                {
                    string nameFile = reader.ReadLine();
                    lista.Add(new FileFtp { Ruta = Site + publicRoute + nameFile, Name = nameFile });
                }
                return lista;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    return lista;
                else
                    throw new SplitException { Category = TypeException.Info, Description = ex.Message, ErrorCode = 100 };
            }
            catch (Exception ex)
            {
                throw new SplitException { Category = TypeException.Info, Description = ex.Message, ErrorCode = 100 };
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
        }
        #endregion
    }

    public class FileFtp
    {
        public string Ruta { get; set; }
        public string Name { get; set; }
    }
}
