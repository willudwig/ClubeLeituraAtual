using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloCategoria;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloReserva;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using ClubeLeitura.ConsoleApp.Superclasses;
using System;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
       private string opcaoSelecionada;
 
       readonly RepositorioCaixa repositorioCaixa;
       readonly RepositorioCategoria repositorioCategoria;
       readonly RepositorioRevista repositorioRevista;
       readonly RepositorioAmigo repositorioAmigo;
       readonly RepositorioEmprestimo repositorioEmprestimo;
       readonly RepositorioReserva repositorioReserva;

       readonly TelaCadastroCategoria telaCadastroCategoria;
       readonly TelaCadastroCaixa telaCadastroCaixa;
       readonly TelaCadastroRevista telaCadastroRevista;
       readonly TelaCadastroAmigo telaCadastroAmigo;
       readonly TelaCadastroEmprestimo telaCadastroEmprestimo;
       readonly TelaCadastroReserva telaCadastroReserva;

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Clube da Leitura 1.0");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Cadastrar Caixas");
            Console.WriteLine("Digite 2 para Cadastrar Categorias");
            Console.WriteLine("Digite 3 para Cadastrar Revistinhas");
            Console.WriteLine("Digite 4 para Cadastrar Amiguinhos");
            Console.WriteLine("Digite 5 para Gerenciar Empréstimos");
            Console.WriteLine("Digite 6 para Gerenciar Reservas");

            Console.WriteLine("Digite s para sair");

            opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaMenuPrincipal()
        {
            repositorioCaixa = new();
            repositorioCategoria = new();
            repositorioRevista = new();
            repositorioEmprestimo = new();
            repositorioAmigo = new();
            repositorioReserva = new();
            telaCadastroCaixa = new(repositorioCaixa);
            telaCadastroCategoria = new(repositorioCategoria);
            telaCadastroRevista = new(telaCadastroCategoria, repositorioCategoria, telaCadastroCaixa, repositorioCaixa, repositorioRevista);
            telaCadastroAmigo = new(repositorioAmigo);
            telaCadastroEmprestimo = new( repositorioEmprestimo, repositorioRevista, repositorioAmigo, telaCadastroRevista, telaCadastroAmigo);
            telaCadastroReserva = new(repositorioReserva, repositorioAmigo, repositorioRevista, telaCadastroAmigo, telaCadastroRevista, repositorioEmprestimo);
        }

        public TelaCadastroBase ObterTela() 
        {
            string opcao = MostrarOpcoes();
            TelaCadastroBase tela = null;

            switch (opcao)
            {
                case "1":
                    tela = telaCadastroCaixa;
                    break;
                case "2":
                    tela = telaCadastroCategoria;
                    break;
                case "3":
                    tela = telaCadastroRevista;
                    break;
                case "4":
                    tela = telaCadastroAmigo;
                    break;
                case "5":
                    tela = telaCadastroEmprestimo;
                    break;
                case "6":
                    tela = telaCadastroReserva;
                    break;
                case "s":
                    Environment.Exit(0);
                    break;
            }

            return tela;
        }

    }
}