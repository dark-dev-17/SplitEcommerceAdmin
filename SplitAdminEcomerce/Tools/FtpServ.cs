using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SplitAdminEcomerce.Tools
{
    public class FtpServ
    {
        #region Propiedades
        
        private string FTP_server;
        private string FTP_user;
        private string FTP_password;
        public string Dominio { get; internal set; }
        /// <summary>
        /// url del sitio https://fibremex.com/fibra-optica/public/
        /// </summary>
        public string Site { get; internal set; }
        public string FtpSitebase { get; internal set; }
        #endregion

        #region Constructores
        ~FtpServ()
        {

        }
        public FtpServ()
        {

        }
        public FtpServ(string FTP_server, string FTP_user, string FTP_password, string Dominio, string Site,string FtpSitebase)
        {
            this.FTP_server = FTP_server;
            this.FTP_user = FTP_user;
            this.FTP_password = FTP_password;
            this.Dominio = Dominio;
            this.Site = Site;
            this.FtpSitebase = FtpSitebase;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// renombrar un archivo
        /// </summary>
        /// <param name="path"></param>
        /// <param name="oldFile"></param>
        /// <param name="newName"></param>
        public void Rename(string path, string NameOld, string newName)
        {
            //ftp.fibremex.co/{path}/{NameOld}
            //ftp.fibremex.co/{path}/{NameNew}
            string UriNew = $"ftp://{FTP_server}/{path}/{newName}";
            string UriOld = $"ftp://{FTP_server}/{path}/{NameOld}";
            FtpWebResponse response = null;
            try
            {
                if (ExistsFile($"/{path}/{newName}"))
                {
                    throw new SplitException { Category = TypeException.Info, Description = $"Ya existe un archivo con el nombre: {newName}", ErrorCode = 100 };
                }
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(UriOld));
                request.Method = WebRequestMethods.Ftp.Rename;
                request.RenameTo = newName;
                request.Credentials = new NetworkCredential(FTP_user, FTP_password);
                response = (FtpWebResponse)request.GetResponse();
            }
            catch (SplitException ex)
            {
                throw new SplitException { Category = TypeException.Info, Description = ex.Message, ErrorCode = 100 };
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    throw new SplitException { Category = TypeException.Info, Description = $"Error al renombrar tu archivo {NameOld}, no se encontró el directorio", ErrorCode = 1100 };
                else
                    throw new SplitException { Category = TypeException.Info, Description = ex.Message, ErrorCode = 100 };
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
        /// Eliminar el archivo selcccionado
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Filename"></param>
        public void DeleteFile(string path, string Filename)
        {
            string Uri = "ftp://" + FTP_server + path;
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            try
            {
                if (!ExistsFile(path))
                {
                    throw new SplitException { Category = TypeException.Info, Description = "No existe el archivo", ErrorCode = 100 };
                }
                request = (FtpWebRequest)WebRequest.Create(new Uri(Uri));
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(FTP_user, FTP_password);
                response = (FtpWebResponse)request.GetResponse();
            }
            catch (SplitException ex)
            {
                throw new SplitException { Category = TypeException.Info, Description = ex.Message, ErrorCode = 100 };
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    throw new SplitException { Category = TypeException.Info, Description = $"Error al eliminar tu archivo {Filename}, no se encontró el directorio", ErrorCode = 1100 };
                else
                    throw new SplitException { Category = TypeException.Info, Description = ex.Message, ErrorCode = 100 };
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
        /// Subir un archivo al path seleccionado
        /// </summary>
        /// <param name="path"></param>
        /// <param name="FormFile"></param>
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
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    throw new SplitException { Category = TypeException.Info, Description = $"Error al cargar tu archivo {FormFile.FileName}, no se encontró el directorio", ErrorCode = 1100 };
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
        /// <summary>
        /// Verificar que exista un archivos
        /// </summary>
        /// <param name="PathFile"></param>
        /// <returns></returns>
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
        public FTPDirectorio ListDirectory(string pattern,bool isroot)
        {
            string Uri = "ftp://" + FTP_server + pattern;

            Stream responseStream = null;
            StreamReader reader = null;
            FtpWebResponse response = null;
            FTPDirectorio FTPDirectorio = new FTPDirectorio();
            FTPDirectorio.PathFolder = $"{pattern}";
            FTPDirectorio.Contenido = new List<FtpContenido>();
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(Uri));
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(FTP_user, FTP_password);
                response = (FtpWebResponse)request.GetResponse();
                
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);

                string regex =
                    @"^" +                          //# Start of line
                    @"(?<dir>[\-ld])" +             //# File size          
                    @"(?<permission>[\-rwx]{9})" +  //# Whitespace          \n
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<filecode>\d+)" +
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<owner>\w+)" +
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<group>\w+)" +
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<size>\d+)" +
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<month>\w{3})" +            //# Month (3 letters)   \n
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<day>\d{1,2})" +            //# Day (1 or 2 digits) \n
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<timeyear>[\d:]{4,5})" +    //# Time or year        \n
                    @"\s+" +                        //# Whitespace          \n
                    @"(?<filename>(.*))" +          //# Filename            \n
                    @"$";                           //# End of line
                while (reader.Peek() >= 0)
                {
                    var split = new Regex(regex).Match(reader.ReadLine());
                    string dir = split.Groups["dir"].ToString();
                    string filename = split.Groups["filename"].ToString();
                    bool isDirectory = !string.IsNullOrWhiteSpace(dir) && dir.Equals("d", StringComparison.OrdinalIgnoreCase);
                    if (filename != "."  && filename != ".ftpquota"  && filename != "Thumbs.db")
                    {
                        if(filename == "..")
                        {
                            if (isroot)
                            {
                                FTPDirectorio.PathLast = dir;
                                FTPDirectorio.Contenido.Add(new FtpContenido
                                {
                                    IsDirectorio = isDirectory,
                                    IsFile = (isDirectory ? false : true),
                                    Extension = (isDirectory ? false : true) ? filename.Split('.')[1] : "",
                                    Name = filename,
                                    PathServer = $"{pattern}{filename}"
                                });
                            }
                        }
                        else
                        {
                            FTPDirectorio.PathLast = dir;
                            FTPDirectorio.Contenido.Add(new FtpContenido
                            {
                                IsDirectorio = isDirectory,
                                IsFile = (isDirectory ? false : true),
                                Extension = (isDirectory ? false : true) ? filename.Split('.')[1] : "",
                                Name = filename,
                                PathServer = $"{pattern}{filename}"
                            });
                        }


                        
                        
                    }

                }
                FTPDirectorio.Contenido = FTPDirectorio.Contenido.OrderByDescending(a => a.IsDirectorio).ToList();
                return FTPDirectorio;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                    return FTPDirectorio;
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
        public string Seccion { get; set; }
    }

    public class FTPDirectorio
    {
        /// <summary>
        /// Nombre del folder contenerdor
        /// </summary>
        public string PathFolder { get; set; }
        /// <summary>
        /// Path anterior
        /// </summary>
        public string PathLast { get; set; }
        /// <summary>
        /// Lista de arcchivos
        /// </summary>
        public List<FtpContenido> Contenido { get; set; }
        /// <summary>
        /// Es folder raiz
        /// </summary>
        public bool IsPathRoot { get; set; }
    }

    public class FtpContenido
    {
        /// <summary>
        /// Es una carpeta
        /// </summary>
        public bool IsDirectorio { get; set; }
        /// <summary>
        /// Es un archivo
        /// </summary>
        public bool IsFile { get; set; }
        /// <summary>
        /// Extension del archivo
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// Nombre del directorio o archivo
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string  PathServer { get; set; }

        public object Datos { get; set; }
    }
}
