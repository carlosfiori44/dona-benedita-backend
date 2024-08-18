using DonaBenedita.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DonaBenedita.Controllers
{
    public class GeralController : Controller
    {
        // GET: Geral
        public ActionResult Principal() //Opção para Login/Cadastro
        {
            return View();
        }

        public ActionResult Personalizado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Personalizado(string preenche) //Opção de enviar imagem ou não
        {
            foreach (string nomeArquivo in Request.Files)
            {
                HttpPostedFileBase arquivoPostado = Request.Files[nomeArquivo];
                string tipoArquivo = arquivoPostado.ContentType;

                if (tipoArquivo.Contains("png"))
                {
                    Modelo log = new Modelo();
                    //se for imagem, vou salvar no BD
                    //convertera a imagem para bytes
                    int numeroBytes = arquivoPostado.ContentLength;
                    byte[] bytesImagem = new byte[numeroBytes];
                    arquivoPostado.InputStream.Read(bytesImagem, 0, numeroBytes);
                    log.Imagem = bytesImagem;
                    string msg = log.enviarPersonalizado();
                    TempData["msg"] = msg;
                }
                else
                {
                    TempData["msg"] = "Insira somente imagens!";
                }
            }

            return View();
        }        

        public ActionResult Sair()
        {
            Session["id_usuario"] = "0";
            return View("Principal");
        }

        public ActionResult Login() //Fazer login no site
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string senha) //HttpPost do Login
        {
            Modelo login = new Modelo();
            login.Email = email;
            login.Senha = senha;

            string msg = login.VerificarLogin();
            TempData["msg"] = msg;

            if (msg.Contains("entrou"))
            {
                Session["id_usuario"] = login.Id;
                return RedirectToAction("Listar");
            }
            else if (msg.Contains("incorreta") || msg.Contains("Erro"))
            {
                return View();
            }
            else if (msg.Contains("existe"))
            {
                return View();
            }
            return View();
        }

        public ActionResult Registrar() //Site para se registrar
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(string email, string senha, string nome, string genero) //HttpPost do Registrar
        {
            Modelo login = new Modelo();
            login.Nome = nome;
            login.Email = email;
            login.Senha = senha;
            login.Genero = genero;

            string msg = login.CadastrarLogin();
            TempData["msg"] = msg;
            if (msg.Contains("Cadastrado"))
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Registrar");
            }
        }

        public ActionResult Listar() //Apenas administrador, serve para listar os usuários (email = admin@admin, senha = admin)
        {
            //if (Session["id_usuario"] != null)
            //{
                //if (Session["id_usuario"].ToString().Equals("admin"))
                //{
                    List<Modelo> lista = Modelo.Listar();
                    return View(lista);
                //}
                //else
                //{
                //    TempData["msg"] = "Sem permisão";
                //    return View("Personalizado");
                //}
            //}
            //else
            //{
            //    TempData["msg"] = "Sem permisão";
            //    return View("Principal");
            //}
        }
    }
}