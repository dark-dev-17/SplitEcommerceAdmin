using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SplitAdminEcomerce.Uniones
{
    public class Usuario
    {
        [Display(Name = "Usuario SplitNet")]
        [Required]
        public string Username { get; set; }
        [Display(Name = "Contraseña")]
        [Required]
        public string Password { get; set; }
    }
}
