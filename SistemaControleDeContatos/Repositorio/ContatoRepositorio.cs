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

        public ContatoModel ListarPorId(int Id)
        {
            return _context.Contato.FirstOrDefault(x => x.Id == Id);
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
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if(contatoDB == null) throw new System.Exception("Contato não encontrado para atualização");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _context.Contato.Update(contatoDB);
            _context.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int Id)
        {
            ContatoModel contatoDB = ListarPorId(Id);

            if (contatoDB == null) throw new System.Exception("Houve um erro na deleção do contato");

            _context.Contato.Remove(contatoDB);
            _context.SaveChanges();

            return true;
        }
    }
}
