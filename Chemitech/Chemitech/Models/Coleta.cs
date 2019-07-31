using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chemitech.Models
{
    public class Coleta
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime Data { get; set; }
        [DisplayName("Total de descarte")]
        public Decimal TotalColeta { get; set; }
        [DisplayName("Bombona")]
        public int BombonaId { get; set; }
        public virtual Bombona Bombona { get; set; }
        [DisplayName("Químico")]
        public int UsuarioColetaId { get; set; }
        public virtual UsuarioColeta UsuarioQuimico { get; set; }

    }
}