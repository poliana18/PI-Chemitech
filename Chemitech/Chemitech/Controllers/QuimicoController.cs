using Chemitech.Models;
using Chemitech.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Chemitech.Controllers
{
    public class QuimicoController : Controller
    {
        private Contexto db = new Contexto();
        // GET: Quimico
        public ActionResult Index(int? id)
        {
            ListaEmpresa list = new ListaEmpresa();
            List<UsuarioEmpresa> ue = new List<UsuarioEmpresa>();
            UsuarioQuimico uq = new UsuarioQuimico();

            int ids = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            uq = db.UsuarioQuimico.Where(x => x.Id == ids).FirstOrDefault();
            List<EmpresaQuimico> emp = new List<EmpresaQuimico>();

            if (id == null)
            {
                ue = db.UsuarioEmpresa.ToList();

                foreach (var a in ue)
                {
                    emp.Add(db.EmpresaQuimico.Where(x => x.Id == a.EmpresaQuimicoId && ids == a.UsuarioQuimicoId).FirstOrDefault());
                }
                list.usuario = uq;
                list.usuarioEmpresas = emp;
            }
            else
            {
                emp.Add(db.EmpresaQuimico.Find(id));
            }


            list.usuario = uq;

            List<Laboratorio> lab = new List<Laboratorio>();
            foreach (var e in emp)
            {
                if (e != null)
                {
                    lab = db.Laboratorio.Where(x => x.EmpresaQuimicoId == e.Id).ToList();
                }
            }
            ViewBag.EmpresaSelecionada = id;
            return View(lab);
        }

        public ActionResult Bombonas(int? id)
        {
            ListaEmpresa list = new ListaEmpresa();
            List<UsuarioEmpresa> ue = new List<UsuarioEmpresa>();


            UsuarioQuimico uq = new UsuarioQuimico();
            int ids = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            uq = db.UsuarioQuimico.Where(x => x.Id == ids).FirstOrDefault();
            ue = db.UsuarioEmpresa.ToList();
            List<EmpresaQuimico> emp = new List<EmpresaQuimico>();
            foreach (var a in ue)
            {
                emp.Add(db.EmpresaQuimico.Where(x => x.Id == a.EmpresaQuimicoId && ids == a.UsuarioQuimicoId).FirstOrDefault());
            }
            list.usuario = uq;
            list.usuarioEmpresas = emp;


            List<Laboratorio> lab = new List<Laboratorio>();
            if (id == null)
            {
                foreach (var e in emp)
                {
                    if (e != null)
                    {
                        lab = db.Laboratorio.Where(x => x.EmpresaQuimicoId == e.Id).ToList();
                    }
                }
            }
            else
            {
                lab.Add(db.Laboratorio.Find(id));
            }
            GerarGrafico grafico = new GerarGrafico();
            List<Bombona> bon = new List<Bombona>();
            foreach (var e in lab)
            {
                bon = db.Bombona.Where(x => x.LaboratorioId == e.Id).ToList();
            }
            grafico.Bombona = bon;
            ViewBag.EmpresaSelecionada = lab[0].EmpresaQuimicoId;
            
            return View(grafico);
        }



        public ActionResult MaisEmpresasQuimicas()
        {
            ViewBag.Ocultar = "login";
            ListaEmpresa list = new ListaEmpresa();
            List<UsuarioEmpresa> ue = new List<UsuarioEmpresa>();


            UsuarioQuimico uq = new UsuarioQuimico();
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            uq = db.UsuarioQuimico.Where(x => x.Id == id).FirstOrDefault();
            List<EmpresaQuimico> emp = new List<EmpresaQuimico>();
            ue = db.UsuarioEmpresa.ToList();

            foreach (var a in ue)
            {
                emp.Add(db.EmpresaQuimico.Where(x => x.Id == a.EmpresaQuimicoId && id == a.UsuarioQuimicoId).FirstOrDefault());
            }

            list.usuario = uq;
            list.usuarioEmpresas = emp;


            return View(list);

        }

        public ActionResult CriarResiduo(int? id)
        {
            Residuo re = new Residuo();
            re.EmpresaQuimicoId = id.Value;
            re.EmpresaQuimico = db.EmpresaQuimico.Find(re.EmpresaQuimicoId);
            re.UsuarioQuimicoId = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            ViewBag.TipoPericulosidade = new SelectList(db.Residuo, "TipoPericulosidade");
            ViewBag.EmpresaSelecionada = id;
            return View(re);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarResiduo([Bind(Include = "Id,Nome,tipo,TipoPericulosidade,EmpresaQuimicoId,UsuarioQuimicoId")] Residuo res, int? id)
        {
            if (ModelState.IsValid)
            {
                res.EmpresaQuimicoId = id.Value;
                res.UsuarioQuimicoId = Convert.ToInt32(User.Identity.Name.Split('|')[1]);

                db.Residuo.Add(res);
                db.SaveChanges();
                TempData["MSG"] = "success|Cadastro realizado";
                return RedirectToAction("Index", new { id = id.Value });
            }
            TempData["MSG"] = "Warning|Cadastro não realizado";
            ViewBag.TipoPericulosidade = new SelectList(db.Residuo, "TipoPericulosidade");
            Residuo re = new Residuo();
            re.EmpresaQuimicoId = id.Value;
            re.EmpresaQuimico = db.EmpresaQuimico.Find(re.EmpresaQuimicoId);
            re.UsuarioQuimicoId = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            ViewBag.TipoPericulosidade = new SelectList(db.Residuo, "TipoPericulosidade");
            ViewBag.EmpresaSelecionada = id;
            return View(res);
        }


        public ActionResult DescartarResiduo(int? id, int? ids)
        {
            Descarte der = new Descarte();
            der.BombonaId = id.Value;
            der.Bombona = db.Bombona.Find(der.BombonaId);
            ViewBag.ResiduoId = new SelectList(db.Residuo.Where(x => x.tipo == der.Bombona.tipo && x.EmpresaQuimicoId == der.Bombona.Laboratorio.EmpresaQuimicoId), "Id", "Nome");// residuo do tipo da bombona
            der.UsuarioQuimicoId = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            ViewBag.EmpresaSelecionada = ids;
            return View(der);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DescartarResiduo([Bind(Include = "Id,Data,QuantidadeAtual,ResiduoId,BombonaId,UsuarioQuimicoId")] Descarte des, int? id, int? ids)
        {
            if (ModelState.IsValid)
            {

                des.BombonaId = id.Value;
                des.Data = DateTime.Now;
                des.UsuarioQuimicoId = Convert.ToInt32(User.Identity.Name.Split('|')[1]);

                db.Descarte.Add(des);
                db.SaveChanges();
                TempData["MSG"] = "success|Cadastro realizado";
                ViewBag.EmpresaSelecionada = ids;
                return RedirectToAction("Index", new { id = ids });// redirecionar para o index da empresas selicionada
            }
            TempData["MSG"] = "Warning|Cadastro não realizado";

            Descarte der = new Descarte();
            der.BombonaId = id.Value;
            der.Bombona = db.Bombona.Find(der.BombonaId);
            ViewBag.ResiduoId = new SelectList(db.Residuo.Where(x => x.tipo == der.Bombona.tipo && x.EmpresaQuimicoId == der.Bombona.Laboratorio.EmpresaQuimicoId), "Id", "Nome");// residuo do tipo da bombona
            der.UsuarioQuimicoId = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            return View(der);
        }

        public ActionResult HistoricoDescarte(int? id)
        {
            ListaEmpresa list = new ListaEmpresa();
            List<UsuarioEmpresa> ue = new List<UsuarioEmpresa>();


            UsuarioQuimico uq = new UsuarioQuimico();
            int ids = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            uq = db.UsuarioQuimico.Where(x => x.Id == ids).FirstOrDefault();
            ue = db.UsuarioEmpresa.ToList();
            List<EmpresaQuimico> emp = new List<EmpresaQuimico>();
            foreach (var a in ue)
            {
                emp.Add(db.EmpresaQuimico.Where(x => x.Id == a.EmpresaQuimicoId && ids == a.UsuarioQuimicoId).FirstOrDefault());
            }
            list.usuario = uq;
            list.usuarioEmpresas = emp;


            List<Laboratorio> lab = new List<Laboratorio>();
            foreach (var e in emp)
                {
                    if (e != null)
                    {
                        lab = db.Laboratorio.Where(x => x.EmpresaQuimicoId == e.Id).ToList();
                    }
                }
            
            List<Bombona> bon = new List<Bombona>();
            foreach (var e in lab)
            {
                bon = db.Bombona.Where(x => x.LaboratorioId == e.Id).ToList();
            }
            
            Descarte de = new Descarte();
            List<Descarte> des = new List<Descarte>();
            des = db.Descarte.Where(x => x.UsuarioQuimicoId == ids).ToList();
            ViewBag.EmpresaSelecionada = id;
            return View(des);

        }

        public ActionResult RecuperarSenha(int? ids)
        {
            ViewBag.EmpresaSelecionada = ids;
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarSenha(RecuperarSenha rec, int? ids)
        {

            UsuarioQuimico usu2 = db.UsuarioQuimico.Where(t => t.Email == rec.Email).ToList().FirstOrDefault();
            if (usu2 != null)
            {
                string hash = Funcoes.HashTexto(DateTime.Now.ToString("yyyyMMddHHmmss"), "SHA512");
                hash = hash.Replace("\\", "").Replace("+", "").Replace("&", "").Replace("/", "");
                usu2.RedefinirSenha = hash;
                usu2.Datalimite = DateTime.Now.AddDays(1);
                db.Entry(usu2).State = EntityState.Modified;
                db.SaveChanges();

                string assunto = "Alteraçao de senha";
                string mensagem = "Para alterar sua senha acesse o link abaixo: <br/>";
                mensagem += "<a href='http://localhost:56266/Homepage/RedefinirSenha/" + hash + "'>Clique aqui para alterar sua senha</a>";
                if (Funcoes.EnviarEmail(rec.Email, assunto, mensagem) != "error|Erro ao enviar e-mail")
                {
                    TempData["MSG"] = "success|Redefinição de senha enviada por e-mail";
                    ViewBag.EmpresaSelecionada = ids;
                    return RedirectToAction("Index", "Quimico", new { id = ids });// redirecionar para o index da empresas selicionada
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao enviar e-mail!");
                    return View();
                }
            }
            else

            {
                ModelState.AddModelError("", "Este E-mail não pertence a nenhuma conta!");
                return View();
            }

        }

        public ActionResult RedefinirSenha(string id, int? ids)
        {
            UsuarioQuimico usu2 = db.UsuarioQuimico.Where(t => t.RedefinirSenha == id).ToList().FirstOrDefault();
            if (usu2 != null)
            {
                if (DateTime.Now <= usu2.Datalimite)
                {
                    RedefinirSenha red = new RedefinirSenha();
                    red.Email = usu2.Email;
                    ViewBag.Ocultar = "login";
                    return View(red);
                }
                else
                {
                    TempData["MSG"] = "warning|Data limite de alteração expirada";
                    ViewBag.EmpresaSelecionada = ids;
                    return RedirectToAction("Index", "Quimico", new { id = ids });// redirecionar para o index da empresas selicionada
                }
            }
            else
            {
                TempData["MSG"] = "warning|Hash inválida";
                ViewBag.EmpresaSelecionada = ids;
                return RedirectToAction("Index", "Quimico", new { id = ids });// redirecionar para o index da empresas selicionada
            }



        }

        [HttpPost]
        public ActionResult RedefinirSenha(RedefinirSenha red, string id, int? ids)
        {
            if (ModelState.IsValid)
            {
                UsuarioQuimico usu2 = db.UsuarioQuimico.Where(t => t.Email == red.Email).ToList().FirstOrDefault();
                if (usu2 != null)
                {
                    usu2.Senha = Funcoes.HashTexto(red.Senha, "SHA512");
                    usu2.RedefinirSenha = null;
                    usu2.Datalimite = null;
                    db.Entry(usu2).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Senha alterada com sucesso";
                    ViewBag.EmpresaSelecionada = ids;
                    return RedirectToAction("Index", "Quimico", new { id = ids });// redirecionar para o index da empresas selicionada
                }
                else
                {
                    TempData["MSG"] = "danger|Erro ao alterar senha";
                    ViewBag.EmpresaSelecionada = ids;
                    return RedirectToAction("Index", "Quimico", new { id = ids });// redirecionar para o index da empresas selicionada
                }



            }
            else
            {
                return View(red);
            }
        }



    }

}