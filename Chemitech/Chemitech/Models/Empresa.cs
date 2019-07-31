using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Chemitech.Models
{
    public class Empresa
    {
       
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome obrigatório")]
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
        [DataType(DataType.PhoneNumber, ErrorMessage = "Forneça o número do telefone no formato (00) 0000-0000")]
        [DisplayName("Número do Telefone")]
        public string Telefone { get; set; }
        [DisplayName("Ramo de atividade")]
        public int RamoAtividadeId { get; set; }
        public virtual RamoAtividade RamoAtividade { get; set; }

    }
}