using ClubeLeitura.ConsoleApp.ModuloRevista;
using ClubeLeitura.ConsoleApp.Superclasses;
using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloCategoria
{
    public class Categoria : EntidadeBase, IValidavel
    {
        private readonly string nome;
        private readonly int diasEmprestimo;

        public Revista[] revistas;

        public string Nome => nome;

        public int DiasEmprestimo
        {
            get
            {
                return diasEmprestimo;
            }
        }

        public Categoria(string nome, int diasEmprestimo)
        {
            this.nome = nome;
            this.diasEmprestimo = diasEmprestimo;
        }

        public string Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}
