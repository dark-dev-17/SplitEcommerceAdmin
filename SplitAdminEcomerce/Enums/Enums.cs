using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Enums
{
    public enum DbAccess
    {
        Splinet = 1,
        Ecommerce = 2,
        SapBussinesOne = 3
    }
    public enum SapB1Objects
    {
        DireccionPedido = 1
    }
    public enum EcomObjects
    {
        Pedido = 1,
        ViewAd_Pedidos = 2,
        ViewAd_Clientes = 3,
        PedidoDetalle = 4,
        ViewAd_PedidoDetalle = 5,
        DireccionFacturacion = 6,
        DireccionEnvio = 7,
        WsB2C = 8,
        OPWebHookLog = 9,
        WsB2B = 10,
        Producto = 11,
        Categoria = 12,
        SubCategoria = 13,
        ProductoBuscador = 14,
        FichaTecnica = 15,
        DescripcionCompartida = 16,
        BlogBuscador = 17,
        Blog = 18,
        BlogComentario = 19,
        Configurable = 20,
    }
    public enum SplinObjects
    {

    }
}
