using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class TransacaoController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public TransacaoController(IHttpContextAccessor httpContextAcessor)
        {
            HttpContextAccessor = httpContextAcessor;
        }

        public IActionResult Index()
        {
            TransacaoModel objTransacao = new TransacaoModel(HttpContextAccessor);
            ViewBag.ListaTransacao = objTransacao.ListaTransacao();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(TransacaoModel formulario)
        {
            if (ModelState.IsValid)
            {
                formulario.HttpContextAccessor = HttpContextAccessor;
                formulario.Insert();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Registrar(int? id)
        {
            if (id != null)
            {
                TransacaoModel objTransacao = new TransacaoModel(HttpContextAccessor);
                ViewBag.Registro = objTransacao.CarregarRegistro(id);
            }
            ViewBag.ListaContas = new ContaModel(HttpContextAccessor).ListaConta();
            ViewBag.ListaPlanoContas = new PlanoContaModel(HttpContextAccessor).ListaPlanoContas();
            return View();
        }

        [HttpGet]
        public IActionResult ExcluirTransacao(int id)
        {
            TransacaoModel objTransacao = new TransacaoModel(HttpContextAccessor);
            ViewBag.Registro = objTransacao.CarregarRegistro(id);

            return View();
        }


        [HttpGet]
        public IActionResult Excluir(int id)
        {
            TransacaoModel objTransacao = new TransacaoModel(HttpContextAccessor);
            objTransacao.Excluir(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [HttpPost]
        public IActionResult Extrato(TransacaoModel formulario)
        {
            formulario.HttpContextAccessor = HttpContextAccessor;
            ViewBag.ListaTransacao = formulario.ListaTransacao();
            ViewBag.ListaContas = new ContaModel(HttpContextAccessor).ListaConta();
            return View();
        }

        public IActionResult Dashboard()
        {
            List<Dashboard> lista = new Dashboard(HttpContextAccessor).RetornarDadosGraficoPie();
            string valores = "";
            string labels = "";
            string cores = "";

            var random = new Random();
            var color = string.Format("#{0:X6}", random.Next(0x1000000));

            for (int i = 0; i < lista.Count; i++)
            {
                valores += lista[i].Total.ToString() + ",";
                labels += "'" + lista[i].PlanoConta.ToString() + "',";
                cores += "'" + string.Format("#{0:X6}", random.Next(0x1000000)) + "',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;
            return View();
        }
    }
}