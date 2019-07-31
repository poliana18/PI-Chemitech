using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace Chemitech.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base(nameOrConnectionString: "StringConexao") {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
        }

        public DbSet<UsuarioQuimico> UsuarioQuimico { get; set; }
        public DbSet<UsuarioColeta> UsuarioColeta { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<EmpresaQuimico> EmpresaQuimico { get; set; }
        public DbSet<Laboratorio> Laboratorio { get; set; }
        public DbSet<Bombona> Bombona { get; set; }
        public DbSet<Residuo> Residuo { get; set; }
        public DbSet<Descarte> Descarte { get; set; }
        public DbSet<RamoAtividade> RamoAtividade { get; set; }
        public DbSet<UsuarioEmpresa> UsuarioEmpresa { get; set; }


    }
}