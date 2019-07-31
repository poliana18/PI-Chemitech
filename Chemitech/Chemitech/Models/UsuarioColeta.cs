using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Chemitech.Models.Validacao;

namespace Chemitech.Models
{
    public class UsuarioColeta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome obrigatório")]
        [DisplayName("Nome de usuário")]
        public string Nome { get; set; }
        [Required]
        [DisplayName("E-mail")]
        [EmailAddress]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
       public string RedefinirSenha { get; set; }
        public DateTime? Datalimite { get; set; }
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}