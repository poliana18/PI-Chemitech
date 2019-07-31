using Chemitech.Models;
using Chemitech.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Chemitech.Controllers
{
    public class HomepageController : Controller
    {
        private Contexto db = new Contexto();
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult ValidarEmail(string email)
        {
            UsuarioColeta u = db.UsuarioColeta.Where(t => t.Email == email).FirstOrDefault();
            if (u != null)
            {
                return Json("s");
            }
            else
            {
                return Json("n");
            }
        }

        // GET: Homepage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Contat()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            ViewBag.Ocultar = "login";
            ViewBag.RamoAtividadeId = new SelectList(db.RamoAtividade, "Id", "NomeRamo");

            return View();
        }


        [HttpPost]
        public ActionResult Cadastrar(UsuarioEmpresaColeta col)
        {

            if (ModelState.IsValid)
            {
                if (db.Empresa.Where(x => x.Cnpj == col.Cnpj).FirstOrDefault() == null)
                {

                    UsuarioColeta usu = new UsuarioColeta();
                    usu.Nome = col.Nome;
                    usu.Email = col.Email;
                    usu.Senha = Funcoes.HashTexto(col.Senha, "SHA512");

                    Empresa emp = new Empresa();

                    emp.NomeEmpresa = col.NomeEmpresa;
                    emp.Cnpj = col.Cnpj;
                    emp.Endereco = col.Endereco;
                    emp.Cidade = col.Cidade;
                    emp.Telefone = col.Telefone;
                    emp.RamoAtividadeId = col.RamoAtividadeId;



                    //Usuario depende de empresa
                    db.Empresa.Add(emp);
                    db.SaveChanges();

                    //identificando o id do banco de dados
                    usu.Empresa = db.Empresa.Where(t => t.Cnpj == emp.Cnpj).FirstOrDefault();
                    usu.EmpresaId = usu.Empresa.Id;


                    //add o objeto usuario
                    db.UsuarioColeta.Add(usu);
                    db.SaveChanges();

                    
                    TempData["MSG"] = "success|Cadastro realizado, Faça seu login";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["MSG"] = "warning|Cadastro não realizado";
                    ModelState.AddModelError("", "Empresa já cadastrada");
                }

            }

           
            ViewBag.RamoAtividadeId = new SelectList(db.RamoAtividade, "Id", "NomeRamo");
            return View();
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Homepage");
        }

        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string senha, string ReturnUrl)
        {
            
            string senhacrip = Funcoes.HashTexto(senha, "SHA512");

            UsuarioColeta usu = db.UsuarioColeta.Where(t => t.Email == email && t.Senha == senhacrip).ToList().FirstOrDefault();
            if (usu != null)
            {
                string permissoes = "";

                if (permissoes.Length > 0)
                    permissoes = permissoes.Substring(0, permissoes.Length - 1);
                FormsAuthentication.SetAuthCookie(usu.Nome, false);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usu.Nome + "|" + usu.Id + "|"+usu.EmpresaId, DateTime.Now, DateTime.Now.AddMinutes(30), false, permissoes);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                if (ticket.IsPersistent)
                    cookie.Expires = ticket.Expiration;
                Response.Cookies.Add(cookie);
                if (String.IsNullOrEmpty(ReturnUrl))
                {
                    return RedirectToAction("Index", "EmpresaColeta");
                }
                else
                {
                    var decodedUrl = Server.UrlDecode(ReturnUrl);
                    if (Url.IsLocalUrl(decodedUrl))
                        return Redirect(decodedUrl);
                    else
                        return RedirectToAction("Index", "EmpresaColeta");
                }
            }
            else
            {
                UsuarioQuimico usu2 = db.UsuarioQuimico.Where(t => t.Email == email && t.Senha == senhacrip).ToList().FirstOrDefault();
                if (usu2 != null)
                {
                    string permissoes = "";

                    if (permissoes.Length > 0)
                        permissoes = permissoes.Substring(0, permissoes.Length - 1);
                    FormsAuthentication.SetAuthCookie(usu2.Nome, false);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usu2.Nome + "|" + usu2.Id , DateTime.Now, DateTime.Now.AddMinutes(30), false, permissoes);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    if (ticket.IsPersistent)
                        cookie.Expires = ticket.Expiration;
                    Response.Cookies.Add(cookie);
                    if (String.IsNullOrEmpty(ReturnUrl))
                    {

                        List<UsuarioEmpresa> up = db.UsuarioEmpresa.Where(x => x.UsuarioQuimicoId == usu2.Id).ToList();
                        if (up.Count == 1)
                            return RedirectToAction("Index", "Quimico");
                        else
                            return RedirectToAction("MaisEmpresasQuimicas", "Quimico");


                    }
                    else
                    {
                        var decodedUrl = Server.UrlDecode(ReturnUrl);
                        if (Url.IsLocalUrl(decodedUrl))
                            return Redirect(decodedUrl);
                        else
                            return RedirectToAction("Index", "Quimico");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuário/Senha inválidos");
                    return View();
                }
            }
        }

        public ActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarSenha(RecuperarSenha rec)
        {

            UsuarioColeta usu = db.UsuarioColeta.Where(t => t.Email == rec.Email).ToList().FirstOrDefault();
            if (usu != null)
            {
                string hash = Funcoes.HashTexto(DateTime.Now.ToString("yyyyMMddHHmmss"), "SHA512");
                hash = hash.Replace("\\", "").Replace("+", "").Replace("&", "").Replace("/", "");
                usu.RedefinirSenha = hash;
                usu.Datalimite = DateTime.Now.AddDays(1);
                db.Entry(usu).State = EntityState.Modified;
                db.SaveChanges();

                string assunto = "Redefinição de senha";
                string mensagem = "Para redefinir sua senha acesse o link abaixo: <br/>";
                mensagem += "<a href='http://localhost:56266/Homepage/RedefinirSenha/" + hash + "'>link</a>";
                if (Funcoes.EnviarEmail(rec.Email, assunto, mensagem) != "error|Erro ao enviar e-mail")
                {
                    TempData["MSG"] = "success|Redefinição de senha enviada por e-mail";
                    return RedirectToAction("Login", "Homepage");
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao enviar e-mail!");
                    return View();
                }
            }
            else
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

                    string assunto = "Redefinição de senha";
                    string mensagem = "Para redefinir sua senha acesse o link abaixo: <br/>";
                    mensagem += "<a href='http://localhost:56266/Homepage/RedefinirSenha/" + hash + "'>Clique aqui para redefinir sua senha</a>";
                    if (Funcoes.EnviarEmail(rec.Email, assunto, mensagem) != "error|Erro ao enviar e-mail")
                    {
                        TempData["MSG"] = "success|Redefinição de senha enviada por e-mail";
                        return RedirectToAction("Login", "Homepage");
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
        }

        public ActionResult RedefinirSenha(string id)
        {
            UsuarioColeta usu = db.UsuarioColeta.Where(t => t.RedefinirSenha == id).ToList().FirstOrDefault();
            if (usu != null)
            {
                if (DateTime.Now <= usu.Datalimite)
                {
                    RedefinirSenha red = new RedefinirSenha();
                    red.Email = usu.Email;
                    return View(red);
                }
                else
                {
                    TempData["MSG"] = "warning|Data limite de alteração expirada";
                    return RedirectToAction("Login", "Homepage");
                }
            }
            else
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
                        return RedirectToAction("Login", "Homepage");
                    }
                }
                else
                {
                    TempData["MSG"] = "warning|Hash inválida";
                    return RedirectToAction("Login", "Homepage");
                }

            }
            
        }

        [HttpPost]
        public ActionResult RedefinirSenha(RedefinirSenha red, string id)
        {
            if (ModelState.IsValid)
            {
                UsuarioColeta usu = db.UsuarioColeta.Where(t => t.Email == red.Email).ToList().FirstOrDefault();
                if (usu != null)
                {
                    usu.Senha = Funcoes.HashTexto(red.Senha, "SHA512");
                    usu.RedefinirSenha = null;
                    usu.Datalimite = null;
                    db.Entry(usu).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Senha atualizada com sucesso";
                    return RedirectToAction("Login", "Homepage");
                }
                else
                {
                    UsuarioQuimico usu2 = db.UsuarioQuimico.Where(t => t.Email == red.Email).ToList().FirstOrDefault();
                    if (usu2 != null)
                    {
                        usu2.Senha = Funcoes.HashTexto(red.Senha, "SHA512");
                        usu2.RedefinirSenha = null;
                        usu2.Datalimite = null;
                        db.Entry(usu2).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|Senha atualizada com sucesso";
                        return RedirectToAction("Login", "Homepage");
                    }
                    else
                    {
                        TempData["MSG"] = "danger|Erro ao atualizar senha";
                        return RedirectToAction("Login", "Homepage");
                    }

                }
                
            }
            else
            {
                return View(red);
            }
        }

    }
}