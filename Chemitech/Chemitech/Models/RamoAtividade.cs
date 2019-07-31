using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chemitech.Models
{
    public class RamoAtividade
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Ramo de atividade")]
        public string NomeRamo { get; set; }
    }
}
