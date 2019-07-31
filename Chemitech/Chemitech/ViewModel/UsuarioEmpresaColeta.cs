using Chemitech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Chemitech.Models.Validacao;

namespace Chemitech.ViewModel
{
    public class UsuarioEmpresaColeta
    {
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Nome obrigatório")]
        [DisplayName("Nome de usuário")]
        public string Nome { get; set; }
        [Required]
        [DisplayName("E-mail")]
        [EmailAddress]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
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
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Nome obrigatório")]
        [DisplayName("Nome da empresa")]
        public string NomeEmpresa { get; set; }
        [Required]
        [DisplayName("CNPJ")]
        [RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "Formato do CNPJ inválido.")]
        public string Cnpj { get; set; }
        [Required]
        [DisplayName("Endereço")]
        [MaxLength(255)]
        public string Endereco { get; set; }
        [Required]
        [MaxLength(255)]
        public string Cidade { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Forneça o número do telefone no formato (000) 000-0000")]
        [DisplayName("Número do Telefone")]
        public string Telefone { get; set; }
        //[EnumDataType(typeof(Tipo))]
        //[Required]
        //[DisplayName("Tipo da empresa")]
        //public Tipo TipoEmpresa { get; set; }
        //public enum Tipo
        //{
        //    COLETA = 1,
        //    QUIMICO = 0
        //}
        [DisplayName("Ramo de atividade")]
        public int RamoAtividadeId { get; set; }
        public virtual RamoAtividade RamoAtividade { get; set; }
    }

    public class UsuarioEmpresaQuimica
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
        
        public List<CheckBoxListItem> Empresas { get; set; }

        public UsuarioEmpresaQuimica()
        {
            Empresas = new List<CheckBoxListItem>();
        }
    }
}