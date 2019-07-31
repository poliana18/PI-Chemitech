using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chemitech.Models
{
    public class Descarte
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        [DisplayName("Quantidade")]
        public int QuantidadeAtual { get; set; }
        public int ResiduoId { get; set; }
        public virtual Residuo Residuo { get; set; }[DisplayName("Bombona")]
        public int BombonaId { get; set; }
        public virtual Bombona Bombona { get; set; }
        [DisplayName("Químico")]
        public int UsuarioQuimicoId { get; set; }
        public virtual UsuarioQuimico UsuarioQuimico { get; set; }
        
    }
}