using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Chemitech.Models.Validacao;

namespace Chemitech.ViewModel
{
    public class RecuperarSenha
    {
        [Required]
        [DisplayName("E-mail")]
        [EmailAddress]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [MaxLength(255)]
        public string Email { get; set; }
    }

    public class RedefinirSenha
    {
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "Senha tem que conter até 8 caracteres, números, letras maiúsculas e minúsculas", MinimumLength = 3)]
        [PasswordForte]
        public string Senha { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Senha")]
        [DisplayName("Confirmar senha")]
        public string ConfirmaSenha { get; set; }
    }
}