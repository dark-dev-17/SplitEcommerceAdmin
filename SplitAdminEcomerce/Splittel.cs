using DbManagerDark.DbManager;
using DbManagerDark.Managers;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.ModelsSAP;
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
        public FtpServ FtpServ { get; internal set; }
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
        public DarkManagerMySQL<WsB2C> WsB2C { get; internal set; }
        public DarkManagerMySQL<OPWebHookLog> OPWebHookLog { get; internal set; }
        public DarkManagerMySQL<WsB2B> WsB2B { get; internal set; }
        public DarkManagerMySQL<Producto> Producto { get; internal set; }
        public DarkManagerMySQL<Categoria> Categoria { get; internal set; }
        public DarkManagerMySQL<SubCategoria> SubCategoria { get; internal set; }
        public DarkManagerMySQL<ProductoBuscador> ProductoBuscador { get; internal set; }
        public DarkManagerMySQL<FichaTecnica> FichaTecnica { get; internal set; }
        public DarkManagerMySQL<DescripcionCompartida> DescripcionCompartida { get; internal set; }
        public DarkManagerMySQL<Configurable> Configurable { get; internal set; }
        public DarkManagerMySQL<Blog> Blog { get; internal set; }
        public DarkManagerMySQL<BlogBuscador> BlogBuscador { get; internal set; }
        public DarkManagerMySQL<BlogComentario> BlogComentario { get; internal set; }
        public DarkManagerMySQL<ConsultorTecnico> ConsultorTecnico { get; internal set; }
        public DarkManagerMySQL<ConsultorRespuestas> ConsultorRespuestas { get; internal set; }
        public DarkManagerMySQL<UsuarioInterno> UsuarioInterno { get; internal set; }
        public DarkManagerMySQL<ConsultorConsultor> ConsultorConsultor { get; internal set; }
        public DarkManagerMySQL<HomeSlide> HomeSlide { get; internal set; }
        public DarkManagerMySQL<Cont_Seccion> Cont_Seccion { get; internal set; }
        public DarkManagerMySQL<Cont_SeccionArchivo> Cont_SeccionArchivo { get; internal set; }
        public DarkManagerMySQL<PrecioCableServ> PrecioCableServ { get; internal set; }
        public DarkManagerMySQL<PrecioDistriPreca> PrecioDistriPreca { get; internal set; }
        public DarkManagerMySQL<PrecioDistriPrecon> PrecioDistriPrecon { get; internal set; }
        public DarkManagerMySQL<PrecioJumperCable> PrecioJumperCable { get; internal set; }
        public DarkManagerMySQL<PrecioJumperConect> PrecioJumperConect { get; internal set; }
        public DarkManagerMySQL<PrecioMPO> PrecioMPO { get; internal set; }
        public DarkManagerMySQL<PrecioPatchCord> PrecioPatchCord { get; internal set; }
        public DarkManagerMySQL<PrecioPigtail> PrecioPigtail { get; internal set; }
        #endregion

        #region Variables de acceso SAP B1
        /// <summary>
        /// Direcciones de envio en SAP B1 para clientes B2B
        /// </summary>
        public DarkSpecialMSSQL<DireccionPedido> DireccionPedido { get; internal set; }
        public DarkManagerMSSQL<CardGroup> CardGroup { get; internal set; }

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
            
            string FTPS = ModeProduction ? "Ftp" : "FtpTest";
            string FtpServer = Configuration.GetSection(FTPS).GetSection("server").Value;
            string FtpUser = Configuration.GetSection(FTPS).GetSection("User").Value;
            string FtpPassword = Configuration.GetSection(FTPS).GetSection("Password").Value;
            string FtpDomain = Configuration.GetSection(FTPS).GetSection("Domain").Value;
            string FtpSiterute = Configuration.GetSection(FTPS).GetSection("Siterute").Value;
            string FtpSitebase = Configuration.GetSection(FTPS).GetSection("Sitebase").Value;

            FtpServ = new FtpServ(FtpServer, FtpUser, FtpPassword, FtpDomain, FtpSiterute, FtpSitebase);

        }
        #endregion

        #region Carga de objectos
        public void LoadObject(Enums.SapB1Objects SapB1Objects)
        {
            if (DbSapBussinesOne == null)
                throw new Exceptions.SplitException { ErrorCode = -100, Category = Exceptions.TypeException.Error, Description = "Por favor conecta db SAPB1" };

            if (SapB1Objects == Enums.SapB1Objects.DireccionPedido)
                DireccionPedido = new DarkSpecialMSSQL<DireccionPedido>(DbSapBussinesOne);

            if (SapB1Objects == Enums.SapB1Objects.CardGroup)
                CardGroup = new DarkManagerMSSQL<CardGroup>(DbSapBussinesOne);
        }
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
            else if (ecomObjects == Enums.EcomObjects.WsB2C)
                WsB2C = new DarkManagerMySQL<WsB2C>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.OPWebHookLog)
                OPWebHookLog = new DarkManagerMySQL<OPWebHookLog>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.WsB2B)
                WsB2B = new DarkManagerMySQL<WsB2B>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.Producto)
                Producto = new DarkManagerMySQL<Producto>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.Categoria)
                Categoria = new DarkManagerMySQL<Categoria>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.SubCategoria)
                SubCategoria = new DarkManagerMySQL<SubCategoria>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.ProductoBuscador)
                ProductoBuscador = new DarkManagerMySQL<ProductoBuscador>(DbEcommerce);

            else if (ecomObjects == Enums.EcomObjects.FichaTecnica)
                FichaTecnica = new DarkManagerMySQL<FichaTecnica>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.DescripcionCompartida)
                DescripcionCompartida = new DarkManagerMySQL<DescripcionCompartida>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.Configurable)
                Configurable = new DarkManagerMySQL<Configurable>(DbEcommerce);

            else if (ecomObjects == Enums.EcomObjects.Blog)
                Blog = new DarkManagerMySQL<Blog>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.BlogBuscador)
                BlogBuscador = new DarkManagerMySQL<BlogBuscador>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.BlogComentario)
                BlogComentario = new DarkManagerMySQL<BlogComentario>(DbEcommerce);

            else if (ecomObjects == Enums.EcomObjects.ConsultorTecnico)
                ConsultorTecnico = new DarkManagerMySQL<ConsultorTecnico>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.ConsultorRespuestas)
                ConsultorRespuestas = new DarkManagerMySQL<ConsultorRespuestas>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.UsuarioInterno)
                UsuarioInterno = new DarkManagerMySQL<UsuarioInterno>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.ConsultorConsultor)
                ConsultorConsultor = new DarkManagerMySQL<ConsultorConsultor>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.HomeSlide)
                HomeSlide = new DarkManagerMySQL<HomeSlide>(DbEcommerce);

            else if (ecomObjects == Enums.EcomObjects.Cont_Seccion)
                Cont_Seccion = new DarkManagerMySQL<Cont_Seccion>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.Cont_SeccionArchivo)
                Cont_SeccionArchivo = new DarkManagerMySQL<Cont_SeccionArchivo>(DbEcommerce);

            else if (ecomObjects == Enums.EcomObjects.PrecioCableServ)
                PrecioCableServ = new DarkManagerMySQL<PrecioCableServ>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.PrecioDistriPreca)
                PrecioDistriPreca = new DarkManagerMySQL<PrecioDistriPreca>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.PrecioDistriPrecon)
                PrecioDistriPrecon = new DarkManagerMySQL<PrecioDistriPrecon>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.PrecioJumperCable)
                PrecioJumperCable = new DarkManagerMySQL<PrecioJumperCable>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.PrecioJumperConect)
                PrecioJumperConect = new DarkManagerMySQL<PrecioJumperConect>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.PrecioMPO)
                PrecioMPO = new DarkManagerMySQL<PrecioMPO>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.PrecioPatchCord)
                PrecioPatchCord = new DarkManagerMySQL<PrecioPatchCord>(DbEcommerce);
            else if (ecomObjects == Enums.EcomObjects.PrecioPigtail)
                PrecioPigtail = new DarkManagerMySQL<PrecioPigtail>(DbEcommerce);
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
