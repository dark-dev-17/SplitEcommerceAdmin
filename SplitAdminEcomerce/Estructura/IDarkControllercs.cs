using SplitAdminEcomerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Estructura
{
    interface IDarkControllercs<T>
    {
        /// <summary>
        /// Limpiar Lista para retorno a vista o proceso
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        List<T> FixData(List<T> result);
        /// <summary>
        /// Terminar controlador, 
        /// </summary>
        void Terminar();
    }
}
