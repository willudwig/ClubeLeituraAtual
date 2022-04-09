using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using ClubeLeitura.ConsoleApp.Superclasses;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class TelaCadastroEmprestimo : TelaCadastroBase, ICadastravel
    {
       readonly RepositorioAmigo repositorioAmigo;
       readonly RepositorioRevista repositorioRevista;
       readonly RepositorioEmprestimo repositorioEmprestimo;
       readonly TelaCadastroRevista telaCadastroRevista;
       readonly TelaCadastroAmigo telaCadastroAmigo;

        public TelaCadastroEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo, TelaCadastroRevista telaCadastroRevista, TelaCadastroAmigo telaCadastroAmigo) : base("Cadastrando Empréstimo")
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioRevista = repositorioRevista;
            this.repositorioAmigo = repositorioAmigo;
            this.telaCadastroRevista = telaCadastroRevista;
            this.telaCadastroAmigo = telaCadastroAmigo;
        } 

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo Novo Empréstimo\n");

            //Validação do Amigo
            Amigo amigoSelecionado = (Amigo)telaCadastroAmigo.ObtemAmigo();

            if (amigoSelecionado.TemMultaEmAberto())
            {
                nota.ApresentarMensagem("Este amigo tem uma multa em aberto.", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemEmprestimoEmAberto())
            {
                nota.ApresentarMensagem("Este amigo já tem um empréstimo em aberto.", TipoMensagem.Erro);
                return;
            }

            // Validação da Revista
            Revista revistaSelecionada = (Revista)telaCadastroRevista.ObtemRevista();

            if (revistaSelecionada.EstaReservada())
            {
                nota.ApresentarMensagem("A revista selecionada já está reservada!", TipoMensagem.Erro);
                return;
            }

            if (revistaSelecionada.EstaEmprestada())
            {
                nota.ApresentarMensagem("A revista selecionada já foi emprestada.", TipoMensagem.Erro);
                return;
            }

            Emprestimo emprestimo = InputarEmprestimo(amigoSelecionado, revistaSelecionada);

            repositorioEmprestimo.Inserir(emprestimo);

            nota.ApresentarMensagem("\nEmpréstimo inserido com sucesso", TipoMensagem.Sucesso);
        }
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Emprestimo");

            //Validação do Amigo
            Amigo amigoSelecionado = (Amigo)telaCadastroAmigo.ObtemAmigo();

            if (amigoSelecionado.TemMultaEmAberto())
            {
                nota.ApresentarMensagem("Este amigo tem uma multa em aberto.", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemEmprestimoEmAberto())
            {
                nota.ApresentarMensagem("Este amigo já tem um empréstimo em aberto.", TipoMensagem.Erro);
                return;
            }

            // Validação da Revista
            Revista revistaSelecionada = (Revista)telaCadastroRevista.ObtemRevista();

            if (revistaSelecionada.EstaReservada())
            {
                nota.ApresentarMensagem("A revista selecionada já está reservada!", TipoMensagem.Erro);
                return;
            }

            if (revistaSelecionada.EstaEmprestada())
            {
                nota.ApresentarMensagem("A revista selecionada já foi emprestada.", TipoMensagem.Erro);
                return;
            }

            int numeroSelecionado = ObtemNumeroEmprestimo();

            Emprestimo entidadeAtualizada = InputarEmprestimo(amigoSelecionado, revistaSelecionada);

            entidadeAtualizada.numero = numeroSelecionado;

            repositorioEmprestimo.Editar(numeroSelecionado, entidadeAtualizada);

            nota.ApresentarMensagem("Reserva editada com sucesso", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipo)
        {
            MostrarTitulo("Visualização de Empréstimos");

            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarTodos();

            for (int i = 0; i < emprestimos.Count; i++)
            {
                Emprestimo emprestimo = (Emprestimo)emprestimos[i];

                string statusEmprestimo = emprestimo.estaAberto ? "Aberto" : "Fechado";

                Console.WriteLine("Número: " + emprestimo.numero);
                Console.WriteLine("Revista emprestada: " + emprestimo.revista.Colecao);
                Console.WriteLine("Nome do amigo: " + emprestimo.amigo.Nome);
                Console.WriteLine("Data do empréstimo: " + emprestimo.dataEmprestimo);
                Console.WriteLine("Status do empréstimo: " + statusEmprestimo);
                Console.WriteLine();
            }

            return true;
        }
        public void ExcluirRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Exclui");

            int numero = ObtemNumeroEmprestimo();

            repositorioEmprestimo.Excluir(numero);
        }

        public bool VisualizarEmprestimosEmAberto(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Empréstimos em Aberto");

            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarEmprestimosAbertos();

            foreach (Emprestimo emp in emprestimos)
            {
                Console.WriteLine("Número: " + emp.numero);
                Console.WriteLine("Revista emprestada: " + emp.revista.Colecao);
                Console.WriteLine("Nome do amigo: " + emp.amigo.Nome);
                Console.WriteLine("Data do empréstimo: " + emp.dataEmprestimo);
                Console.WriteLine();
            }

            return true;
        }

        public void RegistrarDevolucao()
        {
            MostrarTitulo("Devolvendo Empréstimo");

            bool temEmprestimos = VisualizarEmprestimosEmAberto("Pesquisando");

            if (!temEmprestimos)
            {
                nota.ApresentarMensagem("Nenhum empréstimo disponível para devolução.", TipoMensagem.Atencao);
                return;
            }

            int numeroEmprestimo = ObtemNumeroEmprestimo();

            Emprestimo emprestimoParaDevolver = (Emprestimo)repositorioEmprestimo.SelecionarObjeto(numeroEmprestimo);

            if (!emprestimoParaDevolver.estaAberto)
            {
                nota.ApresentarMensagem("O empréstimo selecionado não está mais aberto.", TipoMensagem.Atencao);
                return;
            }

            repositorioEmprestimo.RegistrarDevolucao(emprestimoParaDevolver);

            if (emprestimoParaDevolver.amigo.TemMultaEmAberto())
            {
                decimal multa = emprestimoParaDevolver.amigo.multa.Valor;

                nota.ApresentarMensagem($"A devolução está atrasada, uma multa de R${multa} foi incluída.", TipoMensagem.Atencao);
            }

            nota.ApresentarMensagem("Devolução concluída com sucesso!", TipoMensagem.Sucesso);
        }

        #region métodos provados

        private Emprestimo InputarEmprestimo(Amigo amigo, Revista revista)
        {
            Emprestimo novoEmprestimo = new();

            novoEmprestimo.amigo = amigo;
            novoEmprestimo.revista = revista;

            return novoEmprestimo;
        }

        private int ObtemNumeroEmprestimo()
        {
            int numero;
            bool numeroEncontrado;

            do
            {
                Console.Write("Digite o número que deseja selecionar: ");
                numero = Convert.ToInt32(Console.ReadLine());

                numeroEncontrado = repositorioEmprestimo.VerificarNumeroExistente(numero);

                if (numeroEncontrado == false)
                {
                    nota.ApresentarMensagem("Número não encontrado, digite novamente", TipoMensagem.Atencao);
                    break;
                }

                } while (numeroEncontrado == false);

            return numero;
        }

        //private EntidadeBase ObtemEmprestimo()
        //{
        //    bool temDisponiveis = telaCadastroEmprestimo.VisualizarCadastros();

        //    if (!temDisponiveis)
        //    {
        //        nota.ApresentarMensagem("Não há ítem disponível.", TipoMensagem.Atencao);
        //        return null;
        //    }

        //    Console.Write("Digite o número: ");
        //    int numero = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine();

        //    EntidadeBase itemSelecionado = repositorioEmprestimo.SelecionarObjeto(numero);

        //    return itemSelecionado;

        //}

        private void MostrarTituloEVerificarRegistroVazio(string acaoNoPresente)
        {
            MostrarTitulo(acaoNoPresente + "ndo");

            bool temCadastrados = VisualizarRegistros("");

            if (temCadastrados == false)
            {
                nota.ApresentarMensagem("Nenhum ítem para poder " + acaoNoPresente + "r.", TipoMensagem.Atencao);
                return;
            }
        }

        #endregion
    }
}