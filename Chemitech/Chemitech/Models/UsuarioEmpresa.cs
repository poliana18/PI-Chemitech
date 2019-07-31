using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chemitech.Models
{
    public class UsuarioEmpresa
    {
        [Key]
        public int Id { get; set; }
        
        public int UsuarioQuimicoId { get; set; }
        public virtual UsuarioQuimico UsuarioQuimico { get; set; }
        
        public int EmpresaQuimicoId { get; set; }
        public virtual EmpresaQuimico EmpresaQuimico { get; set; }
    }

    public class ListaEmpresa
    {
        public UsuarioQuimico usuario { get; set; }
        public List<EmpresaQuimico> usuarioEmpresas { get; set; }
    }
}