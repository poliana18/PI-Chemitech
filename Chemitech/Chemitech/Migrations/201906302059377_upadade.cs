namespace Chemitech.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upadade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bombonas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Capacidade = c.String(nullable: false, unicode: false),
                        tipo = c.String(nullable: false, unicode: false),
                        DataInstalacao = c.DateTime(nullable: false, precision: 0),
                        LaboratorioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Laboratorios", t => t.LaboratorioId, cascadeDelete: true)
                .Index(t => t.LaboratorioId);
            
            CreateTable(
                "dbo.Descartes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false, precision: 0),
                        QuantidadeAtual = c.Int(nullable: false),
                        ResiduoId = c.Int(nullable: false),
                        BombonaId = c.Int(nullable: false),
                        UsuarioQuimicoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bombonas", t => t.BombonaId, cascadeDelete: true)
                .ForeignKey("dbo.Residuos", t => t.ResiduoId, cascadeDelete: true)
                .ForeignKey("dbo.UsuarioQuimicoes", t => t.UsuarioQuimicoId, cascadeDelete: true)
                .Index(t => t.ResiduoId)
                .Index(t => t.BombonaId)
                .Index(t => t.UsuarioQuimicoId);

            CreateTable(
                "dbo.Residuos",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nome = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                    tipo = c.String(nullable: false, unicode: false),
                    TipoPericulosidade = c.Int(nullable: false),
                    EmpresaQuimicoId = c.Int(nullable: false),
                    UsuarioQuimicoId = c.Int(nullable: false),
                    
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmpresaQuimicoes", t => t.EmpresaQuimicoId, cascadeDelete: true)
                .ForeignKey("dbo.UsuarioQuimicoes", t => t.UsuarioQuimicoId, cascadeDelete: true)
                .Index(t => t.EmpresaQuimicoId)
                .Index(t => t.UsuarioQuimicoId);
                
            
            CreateTable(
                "dbo.EmpresaQuimicoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeEmpresa = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Cnpj = c.String(nullable: false, unicode: false),
                        Endereco = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Cidade = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Telefone = c.String(nullable: false, unicode: false),
                        RamoAtividadeId = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.EmpresaId, cascadeDelete: true)
                .ForeignKey("dbo.RamoAtividades", t => t.RamoAtividadeId, cascadeDelete: true)
                .Index(t => t.RamoAtividadeId)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeEmpresa = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Cnpj = c.String(nullable: false, unicode: false),
                        Endereco = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Cidade = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Telefone = c.String(nullable: false, unicode: false),
                        RamoAtividadeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RamoAtividades", t => t.RamoAtividadeId, cascadeDelete: true)
                .Index(t => t.RamoAtividadeId);
            
            CreateTable(
                "dbo.RamoAtividades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeRamo = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Laboratorios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Localizacao = c.String(nullable: false, unicode: false),
                        EmpresaQuimicoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmpresaQuimicoes", t => t.EmpresaQuimicoId, cascadeDelete: true)
                .Index(t => t.EmpresaQuimicoId);
            
            CreateTable(
                "dbo.UsuarioQuimicoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Senha = c.String(unicode: false),
                        RedefinirSenha = c.String(unicode: false),
                        Datalimite = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsuarioColetas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Senha = c.String(nullable: false, unicode: false),
                        RedefinirSenha = c.String(unicode: false),
                        Datalimite = c.DateTime(precision: 0),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.EmpresaId, cascadeDelete: true)
                .Index(t => t.EmpresaId);
            
            CreateTable(
                "dbo.UsuarioEmpresas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioQuimicoId = c.Int(nullable: false),
                        EmpresaQuimicoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmpresaQuimicoes", t => t.EmpresaQuimicoId, cascadeDelete: true)
                .ForeignKey("dbo.UsuarioQuimicoes", t => t.UsuarioQuimicoId, cascadeDelete: true)
                .Index(t => t.UsuarioQuimicoId)
                .Index(t => t.EmpresaQuimicoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioEmpresas", "UsuarioQuimicoId", "dbo.UsuarioQuimicoes");
            DropForeignKey("dbo.UsuarioEmpresas", "EmpresaQuimicoId", "dbo.EmpresaQuimicoes");
            DropForeignKey("dbo.UsuarioColetas", "EmpresaId", "dbo.Empresas");
            DropForeignKey("dbo.Descartes", "UsuarioQuimicoId", "dbo.UsuarioQuimicoes");
            DropForeignKey("dbo.Residuos", "Descarte_Id", "dbo.Descartes");
            DropForeignKey("dbo.Descartes", "ResiduoId", "dbo.Residuos");
            DropForeignKey("dbo.Residuos", "UsuarioQuimicoId", "dbo.UsuarioQuimicoes");
            DropForeignKey("dbo.Residuos", "EmpresaQuimicoId", "dbo.EmpresaQuimicoes");
            DropForeignKey("dbo.EmpresaQuimicoes", "RamoAtividadeId", "dbo.RamoAtividades");
            DropForeignKey("dbo.Laboratorios", "EmpresaQuimicoId", "dbo.EmpresaQuimicoes");
            DropForeignKey("dbo.Bombonas", "LaboratorioId", "dbo.Laboratorios");
            DropForeignKey("dbo.EmpresaQuimicoes", "EmpresaId", "dbo.Empresas");
            DropForeignKey("dbo.Empresas", "RamoAtividadeId", "dbo.RamoAtividades");
            DropForeignKey("dbo.Descartes", "BombonaId", "dbo.Bombonas");
            DropIndex("dbo.UsuarioEmpresas", new[] { "EmpresaQuimicoId" });
            DropIndex("dbo.UsuarioEmpresas", new[] { "UsuarioQuimicoId" });
            DropIndex("dbo.UsuarioColetas", new[] { "EmpresaId" });
            DropIndex("dbo.Laboratorios", new[] { "EmpresaQuimicoId" });
            DropIndex("dbo.Empresas", new[] { "RamoAtividadeId" });
            DropIndex("dbo.EmpresaQuimicoes", new[] { "EmpresaId" });
            DropIndex("dbo.EmpresaQuimicoes", new[] { "RamoAtividadeId" });
            DropIndex("dbo.Residuos", new[] { "Descarte_Id" });
            DropIndex("dbo.Residuos", new[] { "UsuarioQuimicoId" });
            DropIndex("dbo.Residuos", new[] { "EmpresaQuimicoId" });
            DropIndex("dbo.Descartes", new[] { "UsuarioQuimicoId" });
            DropIndex("dbo.Descartes", new[] { "BombonaId" });
            DropIndex("dbo.Descartes", new[] { "ResiduoId" });
            DropIndex("dbo.Bombonas", new[] { "LaboratorioId" });
            DropTable("dbo.UsuarioEmpresas");
            DropTable("dbo.UsuarioColetas");
            DropTable("dbo.UsuarioQuimicoes");
            DropTable("dbo.Laboratorios");
            DropTable("dbo.RamoAtividades");
            DropTable("dbo.Empresas");
            DropTable("dbo.EmpresaQuimicoes");
            DropTable("dbo.Residuos");
            DropTable("dbo.Descartes");
            DropTable("dbo.Bombonas");
        }
    }
}
