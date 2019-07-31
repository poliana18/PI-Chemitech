using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chemitech.Models
{
    public class Bombona
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Nome obrigatório")]
        [DisplayName("Nome da bombona")]
        public string Nome { get; set; }
        [Required]
        public string Capacidade { get; set; }
        [Required]
        [DisplayName("Tipo")]
        public string tipo { get; set; }
        [DisplayName("Data de instalação")]
        public DateTime DataInstalacao { get; set; }
        [DisplayName("Laboratório")]
        public int LaboratorioId { get; set; }
        public virtual Laboratorio Laboratorio { get; set; }
        public virtual ICollection<Descarte> Descartes { get; set; }
    }
}