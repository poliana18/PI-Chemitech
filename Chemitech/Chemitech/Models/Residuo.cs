using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chemitech.Models
{
    public class Residuo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Nome obrigatório")]
        [DisplayName("Nome do residuo")]
        public string Nome { get; set; }
        [Required]
        [DisplayName("Tipo")]
        public string tipo { get; set; }
        [EnumDataType(typeof(Tipo2))]
        [Required]
        [DisplayName("Tipo de periculosidade")]
        public Tipo2 TipoPericulosidade { get; set; }
        public enum Tipo2
        {
            Perigoso = 1,
            Inerte = 0
        }
        public int EmpresaQuimicoId { get; set; }
        public virtual EmpresaQuimico EmpresaQuimico { get; set; }
        public int UsuarioQuimicoId { get; set; }
        public virtual UsuarioQuimico UsuarioQuimico { get; set; }

    }
}