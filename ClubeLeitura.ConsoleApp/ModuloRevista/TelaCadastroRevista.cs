using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloCategoria;
using ClubeLeitura.ConsoleApp.Superclasses;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class TelaCadastroRevista : TelaCadastroBase, ICadastravel
    {
       readonly RepositorioCaixa repositorioCaixa;
       readonly RepositorioRevista repositorioRevista;
       readonly RepositorioCategoria repositorioCategoria;
       readonly TelaCadastroCaixa telaCadastroCaixa;
       readonly TelaCadastroCategoria telaCadastroCategoria;

        public TelaCadastroRevista(
            TelaCadastroCategoria telaCadastroCategoria,
            RepositorioCategoria repositorioCategoria,
            TelaCadastroCaixa telaCadastroCaixa,
            RepositorioCaixa repositorioCaixa,
            RepositorioRevista repositorioRevista
            ) : base("Cadastro de Revistas")
        {
            this.telaCadastroCategoria = telaCadastroCategoria;
            this.repositorioCategoria = repositorioCategoria;
            this.telaCadastroCaixa = telaCadastroCaixa;
            this.repositorioCaixa = repositorioCaixa;
            this.repositorioRevista = repositorioRevista;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Cadastrando Nova Revista\n");

            repositorioRevista.Inserir(InputarRevista());

            nota.ApresentarMensagem("\nRevista Cadastrada com sucesso", TipoMensagem.Sucesso);
        }
        public void EditarRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Edita");

            int numeroSelecionado = ObtemNumeroRevista();

            Revista entidadeAtualizada = InputarRevista();

            entidadeAtualizada.numero = numeroSelecionado;

            repositorioRevista.Editar(numeroSelecionado, entidadeAtualizada);

            nota.ApresentarMensagem("Revista editada com sucesso", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipo)
        {
            MostrarTitulo("Visualização de Revistas");

            List<Revista> revistas = repositorioRevista.SelecionarTodos();

            foreach (Revista r in revistas)
            {
                Console.WriteLine("Número: " + r.numero);
                Console.WriteLine("Nome: " + r.Colecao);
                Console.WriteLine("Edição: " + r.Edicao);
                Console.WriteLine("Ano: " + r.Ano);

                Console.WriteLine();
            }

            return true;
        }
        public void ExcluirRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Exclui");

            int numero = ObtemNumeroRevista();

            repositorioRevista.Excluir(numero);
        }
        public EntidadeBase ObtemRevista()
        {
            bool temDisponiveis = VisualizarRegistros("");

            if (!temDisponiveis)
            {
                nota.ApresentarMensagem("Não há ítem disponível.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número: ");
            int numero = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            EntidadeBase itemSelecionado = repositorioRevista.SelecionarObjeto(numero);

            return itemSelecionado;

        }

        #region métodos privados
        private static Revista InputarRevista()
        {
            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite a edição da revista: ");
            int edicao = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o ano da revista: ");
            int ano = Convert.ToInt32(Console.ReadLine());

            Revista novaRevista = new(colecao, edicao, ano);

            return novaRevista;
        }
        private int ObtemNumeroRevista()
        {
            int numero;
            bool numeroEncontrado;

            do
            {
                Console.Write("Digite o número que deseja selecionar: ");
                numero = Convert.ToInt32(Console.ReadLine());

                numeroEncontrado = repositorioRevista.VerificarNumeroExistente(numero);

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
