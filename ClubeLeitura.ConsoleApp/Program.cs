using System;
using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.Superclasses;

namespace ClubeLeitura.ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Notificador notificador = new();
            TelaMenuPrincipal menuPrincipal = new();

            while (true)
            {
                TelaCadastroBase telaSelecionada = menuPrincipal.ObterTela();

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ICadastravel)
                {
                    ICadastravel telaCadastroBasico = (ICadastravel)telaSelecionada;

                    switch (opcaoSelecionada)
                    {
                        case "1":
                            telaCadastroBasico.InserirRegistro();
                            break;
                        case "2":
                            telaCadastroBasico.EditarRegistro();
                            break;
                        case "3":
                            telaCadastroBasico.ExcluirRegistro();
                            break;
                        case "4":
                            bool temRegistro = telaCadastroBasico.VisualizarRegistros("tela");

                            if (!temRegistro)
                                notificador.ApresentarMensagem("Não tem registro", TipoMensagem.Atencao);

                            Console.ReadLine();
                            break;
                    }

                    if (telaSelecionada is TelaCadastroAmigo)
                    {
                        TelaCadastroAmigo telaAmigo = (TelaCadastroAmigo)telaSelecionada;

                        if (opcaoSelecionada == "5")
                        {
                            bool temRegistro = telaAmigo.VisualizarAmigosComMulta("tela");

                            if (!temRegistro)
                                notificador.ApresentarMensagem("Não tem registro", TipoMensagem.Atencao);
                            break;
                        }
                        else if (opcaoSelecionada == "6")
                            telaAmigo.PagarMulta();
                    }
                }
            }
        }
    }
}
