using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chemitech.Models
{
    public class Laboratorio
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Nome obrigatório")]
        [DisplayName("Nome do laboratório")]
        public string Nome { get; set; }
        [Required]
        [DisplayName("Localização")]
        public string Localizacao { get; set; }
        [DisplayName("Empresa")]
        public int EmpresaQuimicoId { get; set; }
        public virtual EmpresaQuimico EmpresaQuimico { get; set; }
        public virtual ICollection<Bombona> Bombonas { get; set; }
    }
}