using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.Superclasses;
using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class TelaCadastroCaixa : TelaCadastroBase, ICadastravel
    {
        readonly RepositorioCaixa repositorioCaixa;
        public TelaCadastroCaixa(RepositorioCaixa repositorioCaixa) : base("Cadastro de Caixas")
        {
            this.repositorioCaixa = repositorioCaixa;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Cadastrando Nova Caixa\n");

            repositorioCaixa.Inserir(InputarCaixa());

            nota.ApresentarMensagem("\nCaixa Cadastrada com Sucesso", TipoMensagem.Sucesso);
        }
        public void EditarRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Edita");

            int numeroSelecionado = ObtemNumeroCaixa();

            Caixa entidadeAtualizada = InputarCaixa();

            entidadeAtualizada.numero = numeroSelecionado;

            repositorioCaixa.Editar(numeroSelecionado, entidadeAtualizada);

            nota.ApresentarMensagem("Caixa editada com sucesso", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipoVisualizado)
        {
            MostrarTitulo("Visualização de Caixas");
 
            List<Caixa> caixas = repositorioCaixa.SelecionarTodos();

            foreach (Caixa c in caixas)
            {
                Console.WriteLine("Número: " + c.numero);
                Console.WriteLine("Cor: " + c.Cor);
                Console.WriteLine("Etiqueta: " + c.Etiqueta);

                Console.WriteLine();
            }

            return true;
        }
        public void ExcluirRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Exclui");

            int numero = ObtemNumeroCaixa();

            repositorioCaixa.Excluir(numero);
        }

        #region métodos privados
        private static Caixa InputarCaixa()
        {
            Console.Write("Digite a cor: ");
            string cor = Console.ReadLine();

            Console.Write("Digite a etiqueta: ");
            string etiqueta = Console.ReadLine();

            Caixa novaCaixa = new (cor, etiqueta);

            return novaCaixa;
        }
        private int ObtemNumeroCaixa()
        {
            int numero;
            bool numeroEncontrado;

            do
            {
                Console.Write("Digite o número que deseja selecionar: ");
                numero = Convert.ToInt32(Console.ReadLine());

                numeroEncontrado = repositorioCaixa.VerificarNumeroExistente(numero);

                if (numeroEncontrado == false)
                {
                    nota.ApresentarMensagem("Número não encontrado, digite novamente", TipoMensagem.Atencao);
                    break;
                }

                } while (numeroEncontrado == false);

            return numero;
        }

        //private EntidadeBase ObtemCaixa()
        //{
        //    bool temDisponiveis = telaCadastroCaixa.VisualizarCadastros();

        //    if (!temDisponiveis)
        //    {
        //        nota.ApresentarMensagem("Não há ítem disponível.", TipoMensagem.Atencao);
        //        return null;
        //    }

        //    Console.Write("Digite o número: ");
        //    int numero = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine();

        //    EntidadeBase itemSelecionado = repositorioCaixa.SelecionarObjeto(numero);

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