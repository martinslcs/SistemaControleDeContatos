using Microsoft.AspNetCore.Mvc;
using SistemaControleDeContatos.Models;
using SistemaControleDeContatos.Repositorio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaControleDeContatos.Controllers
{

    public class ContatoController : Controller
    {

        private readonly IContatoRepositorio _contatoRepositorio;
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {

           List<ContatoModel> contato = _contatoRepositorio.BuscarTodos();
            return View(contato);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int Id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(Id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int Id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(Id);
            return View(contato);
        }

        public IActionResult Apagar(int Id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(Id);

                if (apagado) {
                    TempData["MensagemSucesso"] = "Contato deletado com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não foi possivel deletar o seu contato.";
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não foi possivel deletar o seu contato, detalhe do erro:{erro.Message}.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso.";
                    return RedirectToAction("Index");
                }



                return View(contato);

            }
            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não foi possivel cadastrar o seu contato, detalhe do erro:{erro.Message}.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso.";
                    return RedirectToAction("Index");
                }



                return View("Editar", contato); ;
            }
            catch (System.Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não foi possivel alterar o seu contato, detalhe do erro:{erro.Message}.";
                return RedirectToAction("Index");
            }
        }
    }
}
