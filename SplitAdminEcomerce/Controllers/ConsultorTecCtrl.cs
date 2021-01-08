using Microsoft.AspNetCore.Http;
using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class ConsultorTecCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public ConsultorTecCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();

            if (Splittel.ConsultorTecnico == null)
                Splittel.LoadObject(Enums.EcomObjects.ConsultorTecnico);
            if (Splittel.ConsultorRespuestas == null)
                Splittel.LoadObject(Enums.EcomObjects.ConsultorRespuestas);
            if (Splittel.Categoria == null)
                Splittel.LoadObject(Enums.EcomObjects.Categoria);
            if (Splittel.UsuarioInterno == null)
                Splittel.LoadObject(Enums.EcomObjects.UsuarioInterno);
            if (Splittel.ConsultorConsultor == null)
                Splittel.LoadObject(Enums.EcomObjects.ConsultorConsultor);
        }
        #endregion

        #region Metodos
        public void AddConsultores(List<int> Consultores, int IdConsultorTecnico_)
        {
            Splittel.ConsultorConsultor.GetOpenquery($"where IdPregunta = {IdConsultorTecnico_}", "").ForEach(con => {
                Splittel.ConsultorConsultor.Element = con;
                if (!Splittel.ConsultorConsultor.Delete())
                {
                    throw new SplitException { Category = TypeException.Info, Description = $"Error al asignar consultores, por favor intenta de nuevo", ErrorCode = 100 };
                }
            });

            if (Consultores != null)
            {
                Consultores.ForEach(a => {
                    ConsultorConsultor consultorConsultor = new ConsultorConsultor { 
                        IdConsultor = a,
                        IdPregunta = IdConsultorTecnico_
                    };
                    Splittel.ConsultorConsultor.Element = consultorConsultor;
                    if (!Splittel.ConsultorConsultor.Add())
                    {
                        throw new SplitException { Category = TypeException.Info, Description = $"Error al asignar consultores, por favor intenta de nuevo", ErrorCode = 100 };
                    }
                });
            }
        }
        /// <summary>
        /// Obtener lista de consultores
        /// </summary>
        /// <returns></returns>
        public List<UsuarioInterno> GetConsultores()
        {
            return Splittel.UsuarioInterno.GetOpenquery("", "order by nombre asc");
        }
        /// <summary>
        /// Obtener Consultores asigandos a la pregunta
        /// </summary>
        /// <param name="IdConsultorTecnico_"></param>
        /// <returns></returns>
        public List<ConsultorConsultor> GetConsultoresPre(int IdConsultorTecnico_)
        {
            return Splittel.ConsultorConsultor.GetOpenquery($"where IdPregunta = {IdConsultorTecnico_}","");
        }
        /// <summary>
        /// Obtener detalle de respuesta
        /// </summary>
        /// <param name="IdConsultorRespuestas"></param>
        /// <returns></returns>
        public ConsultorRespuestas GetRespuesta(int IdConsultorRespuestas)
        {
            ConsultorRespuestas consultorRespuestas = Splittel.ConsultorRespuestas.Get(IdConsultorRespuestas);
            if(consultorRespuestas != null)
            {
                consultorRespuestas.UsuarioInterno = Splittel.UsuarioInterno.GetByColumn($"{consultorRespuestas.IdConsultor}", "IdSplitnet"); ;
            }
            return consultorRespuestas;
        }
        /// <summary>
        /// Editar una respuesta
        /// </summary>
        /// <param name="Respuesta_">Respuesta</param>
        /// <param name="IdConsultor_">Consultor editor</param>
        /// <param name="IdConsultorTecnico_">Id del foro</param>
        /// <param name="IdConsultorRespuestas">Id de la respuesta a modificar</param>
        public void Edit(string Respuesta_, int IdConsultor_, int IdConsultorTecnico_, int IdConsultorRespuestas)
        {
            //string PathfileUploaded = "";
            //Splittel.StartTransaction();
            try
            {
                if (string.IsNullOrEmpty(Respuesta_))
                {
                    throw new SplitException { Category = TypeException.Error, Description = $"El campo respuesta es requerido", ErrorCode = 100 };
                }

                ConsultorRespuestas consultorRespuestas = Splittel.ConsultorRespuestas.Get(IdConsultorRespuestas);
                if(consultorRespuestas is null)
                {
                    throw new SplitException { Category = TypeException.Error, Description = $"No se encontro la respuesta a modificar", ErrorCode = 100 };
                }
                if(consultorRespuestas.IdConsultor != IdConsultor_)
                {
                    throw new SplitException { Category = TypeException.Error, Description = $"Sin autorización para modificar esta respuesta, solo la puede editar el creador de la respuesta", ErrorCode = 100 };
                }
                consultorRespuestas.Editado = DateTime.Now;
                consultorRespuestas.Respuesta = Respuesta_;
                Splittel.ConsultorRespuestas.Element = consultorRespuestas;
                if (!Splittel.ConsultorRespuestas.Update())
                {
                    throw new SplitException { Category = TypeException.Info, Description = $"Error al guardar tu respuesta, por favor intenta de nuevo", ErrorCode = 100 };
                }
                //Splittel.Commit();
            }
            catch (SplitException ex)
            {
                //Splittel.RolBack();
                //if (!string.IsNullOrEmpty(PathfileUploaded))
                //{
                //    Splittel.FtpServ.DeleteFile(PathfileUploaded, Archivo.FileName);
                //}
                throw ex;
            }
        }

        /// <summary>
        /// Crear nueva respuesta 
        /// </summary>
        /// <param name="Archivo">Archivo a subir</param>
        /// <param name="Respuesta_">Respuesta</param>
        /// <param name="IdConsultor_">Consultor que responde</param>
        /// <param name="IdConsultorTecnico_">Consutlor solicitud</param>
        /// <param name="Source"></param>
        public void Create(IFormFile Archivo, string Respuesta_, int IdConsultor_, int IdConsultorTecnico_, string Source)
        {
            //Splittel.StartTransaction();
            string PathfileUploaded = "";
            try
            {
                ConsultorRespuestas consultorRespuestas = new ConsultorRespuestas {
                    IdConsultor = IdConsultor_,
                    Fuente = Source,
                    Creado = DateTime.Now,
                    Editado = DateTime.Now,
                    Respuesta = Respuesta_,
                    IdConsultorTecnico = IdConsultorTecnico_
                };
                if (string.IsNullOrEmpty(Respuesta_))
                {
                    throw new SplitException { Category = TypeException.Error, Description = $"El campo respuesta es requerido", ErrorCode = 100 };
                }
                if (Archivo != null)
                {
                    if(Archivo.Length <= 0)
                    {
                        throw new SplitException { Category = TypeException.Error, Description = $"Error al subir el archivo: {Archivo.FileName}", ErrorCode = 100 };
                    }
                    else
                    {
                        int MaxId = Splittel.ConsultorRespuestas.GetLastId("t42_pk01") + 1;
                        consultorRespuestas.RutaAchivo = $"{MaxId}_{Archivo.FileName}";
                        Splittel.FtpServ.UpdateFile($"public_html/fibra-optica/public/images/img_spl/consultecnico/{consultorRespuestas.RutaAchivo}", Archivo);
                        PathfileUploaded = consultorRespuestas.RutaAchivo;
                    }
                }
                   
                Splittel.ConsultorRespuestas.Element = consultorRespuestas;
                if (!Splittel.ConsultorRespuestas.Add())
                {
                    throw new SplitException { Category = TypeException.Info, Description = $"Error al guardar tu respuesta, por favor intenta de nuevo", ErrorCode = 100 };
                }
                //Splittel.Commit();
            }
            catch (SplitException ex)
            {
                //Splittel.RolBack();
                if(!string.IsNullOrEmpty(PathfileUploaded))
                {
                    Splittel.FtpServ.DeleteFile(PathfileUploaded, Archivo.FileName);
                }
                throw ex;
            }
        }
        /// <summary>
        /// Obtener detalles de la pregunta seleccionada
        /// </summary>
        /// <param name="idPregunta"></param>
        /// <returns></returns>
        public ConsultorTecnico GetConsultorTecnico(int idPregunta)
        {
            var result = Splittel.ConsultorTecnico.Get(idPregunta);

            if(result != null)
            {
                result.Che_Activo = result.Activo == 1 ? true : false;
                result.Che_ConRespuesta = Splittel.ConsultorRespuestas.Count($"where t41_pk01 = '{result.IdConsultorTecnico}'") == 0 ? false : true;
                var cate = Splittel.Categoria.GetString(result.IdCategoria);
                result.Categoria = cate == null ? "--" : cate.DescripcionFamilia;
            }
            
            return result;
        }
        /// <summary>
        /// Listar respuestas creadas por pregunta del consultor
        /// </summary>
        /// <param name="IdConsultorTecnico">Id de la preguntas</param>
        /// <returns></returns>
        public List<ConsultorRespuestas> GetRespuestas(int IdConsultorTecnico)
        {
            var respuestas_re = Splittel.ConsultorRespuestas.GetOpenquery($"where t41_pk01 = '{IdConsultorTecnico}'","");
            respuestas_re.ForEach(a => {
                a.UsuarioInterno = Splittel.UsuarioInterno.GetByColumn($"{a.IdConsultor}", "IdSplitnet");
            });
            return respuestas_re;
        }
        /// <summary>
        /// Listar preguntas 
        /// </summary>
        /// <returns></returns>
        public List<ConsultorTecnico> GetConsultorTecnicos()
        {
            var result = Splittel.ConsultorTecnico.Get();
            result.ForEach(a => {
                a.Che_Activo = a.Activo == 1 ? true : false;
                a.Che_ConRespuesta = Splittel.ConsultorRespuestas.Count($"where t41_pk01 = '{a.IdConsultorTecnico}'") == 0 ? false : true;
                var cate = Splittel.Categoria.GetString(a.IdCategoria);
                a.Categoria = cate == null ? "--" : cate.DescripcionFamilia;
            });
            return result.OrderBy(a => a.Creado).ToList();
        }
        /// <summary>
        /// Desactivar o activar consultor tecnico pregunta
        /// </summary>
        /// <param name="IdConsultorTecnico_"></param>
        /// <param name="Active"></param>
        public void DesactivarPregunta(int IdConsultorTecnico_, bool Active)
        {
            var result = Splittel.ConsultorTecnico.Get(IdConsultorTecnico_);

            if (result != null)
            {
                result.Activo = Active ? 1 : 0;
                Splittel.ConsultorTecnico.Element = result;
                if (!Splittel.ConsultorTecnico.Update())
                {
                    throw new SplitException { Category = TypeException.Info, Description = $"Error al desactivar tu respuesta, por favor intenta de nuevo", ErrorCode = 100 };
                }
            }
            else
            {
                throw new SplitException { Category = TypeException.Info, Description = $"Error al desactivar tu respuesta, esta no fue encontrada", ErrorCode = 100 };
            }
        }
        /// <summary>
        /// terminar metodo controllador y clase manager 
        /// </summary>
        public void Terminar()
        {
            Splittel.ConsultorTecnico = null;
            Splittel.ConsultorRespuestas = null;
            Splittel.Categoria = null;
            Splittel.UsuarioInterno = null;
            Splittel.DisConnect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion
    }
}
