using SplitAdminEcomerce.Controllers;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Uniones
{
    public class ImagenesSeccion
    {
        public ProductoTipoFile Tipo { get; set; }
        public List<FileFtp> Imagenes { get; set; }
    }
}
