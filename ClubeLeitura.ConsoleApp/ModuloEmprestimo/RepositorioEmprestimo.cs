using ClubeLeitura.ConsoleApp.Superclasses;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase<Emprestimo>
    {
        public override string Inserir(Emprestimo emprestimo)
        {
            emprestimo.Abrir();

            emprestimo.revista.RegistrarEmprestimo(emprestimo);
            emprestimo.amigo.RegistrarEmprestimo(emprestimo);

            base.Inserir(emprestimo);

            return "Válido";
        }

        public bool RegistrarDevolucao(Emprestimo emprestimo)
        {
            emprestimo.Fechar();

            return true;
        }

        public List<Emprestimo> SelecionarEmprestimosAbertos()
        {
            List<Emprestimo> emprestimosAbertos = null;
            
            registro = SelecionarTodos();

            foreach (Emprestimo emp in registro)
            {
                if(emp.estaAberto == true)
                    emprestimosAbertos.Add(emp);
            }

            return emprestimosAbertos;
        }

    }
}
