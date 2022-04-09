using ClubeLeitura.ConsoleApp.Compartilhado;
using System;
using ClubeLeitura.ConsoleApp.Superclasses;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloCategoria
{
    public class TelaCadastroCategoria : TelaCadastroBase, ICadastravel
    {
        readonly RepositorioCategoria repositorioCategoria;

        public TelaCadastroCategoria(RepositorioCategoria repositorioCategoria) : base("Cadastrando Categoria")
        {
            this.repositorioCategoria = repositorioCategoria;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Cadastrando Nova Categoria\n");

            repositorioCategoria.Inserir(InputarCategoria());

            nota.ApresentarMensagem("\nCategoria Cadastrada com Sucesso", TipoMensagem.Sucesso);
        }
        public void EditarRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Edita");

            int numeroSelecionado = ObtemNumeroCategoria();

            Categoria entidadeAtualizada = InputarCategoria();

            entidadeAtualizada.numero = numeroSelecionado;

            repositorioCategoria.Editar(numeroSelecionado, entidadeAtualizada);

            nota.ApresentarMensagem("Categoria editada com sucesso", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipo)
        {
            MostrarTitulo("Visualização de Categorias");

            List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

            foreach (Categoria cat in categorias )
            {
                Console.WriteLine("Número: " + cat.numero);
                Console.WriteLine("Tipo de Categoria: " + cat.Nome);
                Console.WriteLine("Limite de empréstimo: " + cat.DiasEmprestimo + " dias");

                Console.WriteLine();
            }

            return true;
        }
        public void ExcluirRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Exclui");

            int numero = ObtemNumeroCategoria();

            repositorioCategoria.Excluir(numero);
        }

        #region métodos privados
        private static Categoria InputarCategoria()
        {
            Console.Write("Digite o nome da categoria: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o limite de dias de empréstimo das revistas: ");
            int diasEmprestimo = Convert.ToInt32(Console.ReadLine());

            Categoria novaCategoria = new(nome, diasEmprestimo);

            return novaCategoria;
        }

        //private EntidadeBase ObtemCategoria()
        //{
        //    bool temDisponiveis = telaCadastroCategoria.VisualizarCadastros();

        //    if (!temDisponiveis)
        //    {
        //        nota.ApresentarMensagem("Não há ítem disponível.", TipoMensagem.Atencao);
        //        return null;
        //    }

        //    Console.Write("Digite o número: ");
        //    int numero = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine();

        //    EntidadeBase itemSelecionado = repositorioCategoria.SelecionarObjeto(numero);

        //    return itemSelecionado;


        //}

        private int ObtemNumeroCategoria()
        {
            int numero;
            bool numeroEncontrado;

            do
            {
                Console.Write("Digite o número que deseja selecionar: ");
                numero = Convert.ToInt32(Console.ReadLine());

                numeroEncontrado = repositorioCategoria.VerificarNumeroExistente(numero);

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
