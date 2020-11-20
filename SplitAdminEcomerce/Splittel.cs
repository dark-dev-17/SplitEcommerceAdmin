using DbManagerDark.DbManager;
using DbManagerDark.Managers;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Tools;
using SplitAdminEcomerce.Views;
using System;

namespace SplitAdminEcomerce
{
    public class Splittel
    {
        #region Propiedades
        protected DarkConnectionSqlSever DbSapBussinesOne;
        protected DarkConnectionMySQL DbEcommerce;
        protected DarkConnectionMySQL DbSplinet;
        protected IConfiguration Configuration;
        protected EmailServ EmailServ;
        protected string Server { get; set; }
        protected string From { get; set; }
        protected string Port { get; set; }
        protected string User { get; set; }
        protected string Password { get; set; }
        protected bool UserSSL { get; set; }
        protected string CorreosBCC { get; set; }
        protected string StrDbSapBussinesOne { get; set; }
        protected string StrDbEcommerce { get; set; }
        protected string StrDbSplinet { get; set; }
        #endregion

        #region Variables de acceso ecomerce
        public DarkManagerMySQL<ViewAd_Pedidos> ViewAd_Pedidos { get; internal set; }
        public DarkManagerMySQL<ViewAd_Clientes> ViewAd_Clientes { get; internal set; }
        public DarkManagerMySQL<Pedido> Pedido { get; internal set; }
        public DarkManagerMySQL<PedidoDetalle> PedidoDetalle { get; internal set; }
        public DarkManagerMySQL<ViewAd_PedidoDetalle> ViewAd_PedidoDetalle { get; internal set; }
        public DarkManagerMySQL<DireccionEnvio> DireccionEnvio { get; internal set; }
        public DarkManagerMySQL<DireccionFacturacion> DireccionFacturacion { get; internal set; }
        #endregion

        #region Constructores
        public Splittel()
        {

        }
        public Splittel(IConfiguration configuration)
        {
            this.Configuration = configuration;
            bool ModeProduction = Configuration.GetSection("ModeProduction").Value == "true" ? true : false;

            #region Datos de EmailService
            string SMTP = ModeProduction ? "Smtp" : "SmtpTest";

            Server = Configuration.GetSection(SMTP).GetSection("Server").Value;
            Port = Configuration.GetSection(SMTP).GetSection("Port").Value;
            From = Configuration.GetSection(SMTP).GetSection("account").Value;
            User = Configuration.GetSection(SMTP).GetSection("User").Value;
            Password = Configuration.GetSection(SMTP).GetSection("Password").Value;
            UserSSL = Configuration.GetSection(SMTP).GetSection("Ssl").Value == "true" ? true : false;
            CorreosBCC = Configuration.GetSection(SMTP).GetSection("Bcc").Value;

            CorreosBCC = Configuration.GetSection(SMTP).GetSection("Bcc").Value;

            EmailServ = new EmailServ(Server, From, Port, User, Password, UserSSL);
            EmailServ.AddListBCC(CorreosBCC);
            #endregion

            string Base = ModeProduction ? "Prod" : "Test";
            StrDbEcommerce = Configuration.GetConnectionString("Ecommerce"+ Base);
            StrDbSapBussinesOne = Configuration.GetConnectionString("SapB1" + Base);
            StrDbSplinet = Configuration.GetConnectionString("Splittel" + Base);
        }
        #endregion

        #region Carga de objectos
        public void LoadObject(Enums.EcomObjects ecomObjects)
        {
            if (DbEcommerce == null)
                throw new Exceptions.SplitException {ErrorCode= -100, Category = Exceptions.TypeException.Error, Description = "Por favor conecta db ecommerce" };

            if (ecomObjects == Enums.EcomObjects.Pedido)
                Pedido = new DarkManagerMySQL<Pedido>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.ViewAd_Pedidos)
                ViewAd_Pedidos = new DarkManagerMySQL<ViewAd_Pedidos>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.ViewAd_Clientes)
                ViewAd_Clientes = new DarkManagerMySQL<ViewAd_Clientes>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.PedidoDetalle)
                PedidoDetalle = new DarkManagerMySQL<PedidoDetalle>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.ViewAd_PedidoDetalle)
                ViewAd_PedidoDetalle = new DarkManagerMySQL<ViewAd_PedidoDetalle>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.DireccionEnvio)
                DireccionEnvio = new DarkManagerMySQL<DireccionEnvio>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.DireccionFacturacion)
                DireccionFacturacion = new DarkManagerMySQL<DireccionFacturacion>(DbEcommerce);
        }
        #endregion

        #region Metodos Generales
        /// <summary>
        /// Conecta a la base de datos seleccionada
        /// </summary>
        /// <param name="dbAccess"></param>
        public void Connect(Enums.DbAccess dbAccess = Enums.DbAccess.Ecommerce)
        {
            if (dbAccess == Enums.DbAccess.Splinet)
            {
                DbSplinet = new DarkConnectionMySQL(StrDbSplinet);
                DbSplinet.OpenConnection();
            }
            else if (dbAccess == Enums.DbAccess.SapBussinesOne)
            {
                DbSapBussinesOne = new DarkConnectionSqlSever(StrDbSapBussinesOne);
                DbSapBussinesOne.OpenConnection();
            }
            else if (dbAccess == Enums.DbAccess.Ecommerce)
            {
                DbEcommerce = new DarkConnectionMySQL(StrDbEcommerce);
                DbEcommerce.OpenConnection();
            }
        }
        /// <summary>
        /// Cierra conexion a la base de datos seleccionada
        /// </summary>
        /// <param name="dbAccess"></param>
        public void DisConnect(Enums.DbAccess dbAccess = Enums.DbAccess.Ecommerce)
        {
            if (dbAccess == Enums.DbAccess.Splinet)
            {
                if (DbSplinet != null)
                {
                    DbSplinet.CloseDataBaseAccess();
                    DbSplinet = null;
                }
            }
            else if (dbAccess == Enums.DbAccess.SapBussinesOne)
            {
                if (DbSapBussinesOne != null)
                {
                    DbSapBussinesOne.CloseDataBaseAccess();
                    DbSapBussinesOne = null;
                }
            }
            else if (dbAccess == Enums.DbAccess.Ecommerce)
            {
                if (DbEcommerce != null)
                {
                    DbEcommerce.CloseDataBaseAccess();
                    DbEcommerce = null;
                }
            }
        }
        /// <summary>
        /// Inicia transaccion
        /// </summary>
        /// <param name="dbAccess"></param>
        public void StartTransaction(Enums.DbAccess dbAccess = Enums.DbAccess.Ecommerce)
        {
            if (dbAccess == Enums.DbAccess.Splinet)
            {
                if (DbSplinet != null)
                {
                    DbSplinet.StartTransaction();
                    DbSplinet = null;
                }
            }
            else if (dbAccess == Enums.DbAccess.SapBussinesOne)
            {
                if (DbSapBussinesOne != null)
                {
                    DbSapBussinesOne.StartTransaction();
                    DbSapBussinesOne = null;
                }
            }
            else if (dbAccess == Enums.DbAccess.Ecommerce)
            {
                if (DbEcommerce != null)
                {
                    DbEcommerce.StartTransaction();
                    DbEcommerce = null;
                }
            }
        }
        /// <summary>
        /// Termina transaccion, guarda de forma permanente los cambios realizados
        /// </summary>
        /// <param name="dbAccess">Tipo de base de datos que se desea terminar la accion</param>
        public void Commit(Enums.DbAccess dbAccess = Enums.DbAccess.Ecommerce)
        {
            if (dbAccess == Enums.DbAccess.Splinet)
            {
                if (DbSplinet != null)
                {
                    DbSplinet.Commit();
                    DbSplinet = null;
                }
            }
            else if (dbAccess == Enums.DbAccess.SapBussinesOne)
            {
                if (DbSapBussinesOne != null)
                {
                    DbSapBussinesOne.Commit();
                    DbSapBussinesOne = null;
                }
            }
            else if (dbAccess == Enums.DbAccess.Ecommerce)
            {
                if (DbEcommerce != null)
                {
                    DbEcommerce.Commit();
                    DbEcommerce = null;
                }
            }
        }
        /// <summary>
        /// Termina transaccion, restaura de forma permanente los cambios realizados
        /// </summary>
        /// <param name="dbAccess">Tipo de base de datos que se desea terminar la accion</param>
        public void RolBack(Enums.DbAccess dbAccess = Enums.DbAccess.Ecommerce)
        {
            if (dbAccess == Enums.DbAccess.Splinet)
            {
                if (DbSplinet != null)
                {
                    DbSplinet.RolBack();
                    DbSplinet = null;
                }
            }
            else if (dbAccess == Enums.DbAccess.SapBussinesOne)
            {
                if (DbSapBussinesOne != null)
                {
                    DbSapBussinesOne.RolBack();
                    DbSapBussinesOne = null;
                }
            }
            else if (dbAccess == Enums.DbAccess.Ecommerce)
            {
                if (DbEcommerce != null)
                {
                    DbEcommerce.RolBack();
                    DbEcommerce = null;
                }
            }
        }
        /// <summary>
        /// termina y libera objetos usados durante el proceso
        /// </summary>
        public void Terminar()
        {
            if(DbSplinet != null)
            {
                DbSplinet = null;
            }
            if (DbEcommerce != null)
            {
                DbEcommerce = null;
            }
            if (DbSapBussinesOne != null)
            {
                DbSapBussinesOne = null;
            }
            GC.Collect();
            GC.WaitForFullGCComplete();
        }
        #endregion
    }
}
