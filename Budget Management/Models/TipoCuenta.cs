using Budget_Management.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Budget_Management.Models
{
    public class TipoCuenta 
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "El campo {0} no puede estar vacío")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "La longitud del campo debe ser de mínimo {2} carácteres y de máximo {1} caracteres")]
        [Display(Name = "Nombre de Tipo Cuenta")]
        [PrimeraLetraMayuscula] /*attribute dentro de validaciones */
        [Remote(action: "VerificarTipoCuenta", controller: "TiposCuentas")]

        /*[Remote] para indicar que se debe validar el tipo de cuenta llamando a este método remoto. 
         * La anotación [Remote(action: "VerificarTipoCuenta", controller: "TiposCuentas")]
         * le dice a ASP.NET MVC que valide el campo llamando al método VerificarTipoCuenta en el controlador TiposCuentas.*/

        public string Nombre { get; set; }
        public  int UsuarioId { get; set; }
        public int Orden { get; set; }

  








        /* OTHER VALIDATIONS 

        [Required(ErrorMessage = "El campo {0} no puede estar vacío")]
        [EmailAddress(ErrorMessage = "El correo debe ser válido")]
        public string   Email { get; set; }

        [Required(ErrorMessage = "El campo {0} no puede estar vacío")]
        [Range(minimum:18, maximum:130, ErrorMessage = "Debe estár comprendido entre {1} y {2}")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El campo {0} no puede estar vacío")]
        [Url(ErrorMessage = "Debe ser una URL válida")]
        public  string Url { get; set; }


        [Required(ErrorMessage = "El campo {0} no puede estar vacío")]
        [CreditCard(ErrorMessage = "Debe introducir una TC válida")]

        [Display(Name = "Tarjeta de Crédito")]
        public string TarjetaCrédito { get; set; }
        */



    }
}
