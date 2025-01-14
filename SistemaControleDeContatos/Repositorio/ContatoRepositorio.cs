using SistemaControleDeContatos.Data;
using SistemaControleDeContatos.Models;

namespace SistemaControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _context;
        public ContatoRepositorio(BancoContext bancoContext) 
        { 
            this._context = bancoContext;
        }

        public ContatoModel ListarPorId(int id)
        {
            return _context.Contato.FirstOrDefault(x => x.id == id);
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _context.Contato.ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            
            _context.Contato.Add(contato);
            _context.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.id);

            if (contatoDB == null) throw new System.Exception("Contato não encontrado para atualização");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _context.Contato.Update(contatoDB);
            _context.SaveChanges();

            return contatoDB;
        }


    }
}
