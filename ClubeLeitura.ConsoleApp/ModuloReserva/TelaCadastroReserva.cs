using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using ClubeLeitura.ConsoleApp.Superclasses;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class TelaCadastroReserva : TelaCadastroBase, ICadastravel
    {
      readonly RepositorioReserva repositorioReserva;
      readonly RepositorioRevista repositorioRevista;
      readonly RepositorioEmprestimo repositorioEmprestimo;
      readonly RepositorioAmigo repositorioAmigo;
      readonly TelaCadastroAmigo telaCadastroAmigo;
      readonly TelaCadastroRevista telaCadastroRevista;
        public TelaCadastroReserva(
            RepositorioReserva repositorioReserva,
            RepositorioAmigo repositorioAmigo,
            RepositorioRevista repositorioRevista,
            TelaCadastroAmigo telaCadastroAmigo,
            TelaCadastroRevista telaCadastroRevista,
            RepositorioEmprestimo repositorioEmprestimo) : base("Cadastrando Reserva")
        {
            this.repositorioReserva = repositorioReserva;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.telaCadastroAmigo = telaCadastroAmigo;
            this.telaCadastroRevista = telaCadastroRevista;
            this.repositorioEmprestimo = repositorioEmprestimo;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo Nova Reserva");

            // Validação do Amigo
            Amigo amigoSelecionado = (Amigo)telaCadastroAmigo.ObtemAmigo();

            if (amigoSelecionado.TemMultaEmAberto())
            {
                nota.ApresentarMensagem("Este amigo tem uma multa em aberto e não pode reservar.", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemReservaEmAberto())
            {
                nota.ApresentarMensagem("Este amigo já possui uma reserva em aberto..", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemEmprestimoEmAberto())
            {
                nota.ApresentarMensagem("Este amigo já possui uma reserva em aberto e não pode reservar.", TipoMensagem.Erro);
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

            Reserva novaReserva = InputarReserva(amigoSelecionado, revistaSelecionada);

            repositorioReserva.Inserir(novaReserva);

            nota.ApresentarMensagem("Reserva inserida com sucesso", TipoMensagem.Sucesso);
        }
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Reserva");

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

            int numeroSelecionado = ObtemNumeroReserva();

            Reserva entidadeAtualizada = InputarReserva(amigoSelecionado, revistaSelecionada);

            entidadeAtualizada.numero = numeroSelecionado;

            repositorioReserva.Editar(numeroSelecionado, entidadeAtualizada);

            nota.ApresentarMensagem("Reserva editada com sucesso", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipo)
        {
            MostrarTitulo("Visualização de Reservas");

            List<Reserva> reservas = repositorioReserva.SelecionarTodos();

            for (int i = 0; i < reservas.Count; i++)
            {
                Reserva reserva = (Reserva)reservas[i];

                string statusReserva = reserva.estaAberta ? "Aberta" : "Fechada";

                Console.WriteLine("Número: " + reserva.numero);
                Console.WriteLine("Revista reservada: " + reserva.revista.Colecao);
                Console.WriteLine("Nome do amigo: " + reserva.amigo.Nome);
                Console.WriteLine("Data da reserva: " + reserva.dataInicialReserva.ToShortDateString());
                Console.WriteLine("Status da reserva: " + statusReserva);
                Console.WriteLine();
            }

            return true;
        }
        public void ExcluirRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Exclui");

            int numero = ObtemNumeroReserva();

            repositorioReserva.Excluir(numero);
        }

        public void RegistrarNovoEmprestimo()
        {
            MostrarTitulo("Registrando novo Empréstimo");

            Reserva reservaParaEmprestimo = ObtemReservaParaEmprestimo();

            reservaParaEmprestimo.Fechar();

            Emprestimo novoEmprestimo = new();

            novoEmprestimo.revista = reservaParaEmprestimo.revista;
            novoEmprestimo.amigo = reservaParaEmprestimo.amigo;

            repositorioEmprestimo.Inserir(novoEmprestimo);

            nota.ApresentarMensagem("Empréstimo registrado com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarReservasEmAberto(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Reservas em Aberto");

            List<Reserva> reservas = repositorioReserva.SelecionarReservasEmAberto();

            foreach (Reserva r in reservas)
            {
                Console.WriteLine("Número: " + r.numero);
                Console.WriteLine("Revista reservada: " + r.revista.Colecao);
                Console.WriteLine("Nome do amigo: " + r.amigo.Nome);
                Console.WriteLine("Data de expiração da Reserva: " + r.dataInicialReserva.AddDays(2).ToShortDateString());
                Console.WriteLine();
            }

            return true;
        }

        public Reserva ObtemReservaParaEmprestimo()
        {
            bool temReservasEmAberto = VisualizarReservasEmAberto("Pesquisando");

            if (!temReservasEmAberto)
            {
                nota.ApresentarMensagem("Não há nenhuma reserva aberta disponível para cadastro.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da reserva que irá emprestar: ");
            int numeroReserva = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Reserva reservaSelecionada = (Reserva)repositorioReserva.SelecionarObjeto(numeroReserva);

            return reservaSelecionada;
        }

        #region métodos privados

        private Reserva InputarReserva(Amigo amigoSelecionado, Revista revistaSelecionada)
        {
            Reserva novaReserva = new();

            novaReserva.amigo = amigoSelecionado;
            novaReserva.revista = revistaSelecionada;

            return novaReserva;
        }

        //private EntidadeBase ObtemReserva()
        //{
        //    bool temDisponiveis = telaCadastroReserva.VisualizarCadastros();

        //    if (!temDisponiveis)
        //    {
        //        nota.ApresentarMensagem("Não há ítem disponível.", TipoMensagem.Atencao);
        //        return null;
        //    }

        //    Console.Write("Digite o número: ");
        //    int numero = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine();

        //    EntidadeBase itemSelecionado = repositorioReserva.SelecionarObjeto(numero);

        //    return itemSelecionado;

        //}

        private int ObtemNumeroReserva()
        {
            int numero;
            bool numeroEncontrado;

            do
            {
                Console.Write("Digite o número que deseja selecionar: ");
                numero = Convert.ToInt32(Console.ReadLine());

                numeroEncontrado = repositorioReserva.VerificarNumeroExistente(numero);

                if (numeroEncontrado == false)
                {
                    nota.ApresentarMensagem("Número não encontrado, digite novamente", TipoMensagem.Atencao);
                    break;
                }

                } while (numeroEncontrado == false);

            return numero;
        }

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
