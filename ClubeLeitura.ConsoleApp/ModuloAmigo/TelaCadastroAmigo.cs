using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.Superclasses;
using System.Collections.Generic;
using System;


namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class TelaCadastroAmigo : TelaCadastroBase, ICadastravel
    {
        readonly RepositorioAmigo repositorioAmigo;
        public TelaCadastroAmigo(RepositorioAmigo repositorioAmigo) :base("Cadastrando Amigo")
        {
            this.repositorioAmigo = repositorioAmigo;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo Novo Amigo");

            repositorioAmigo.Inserir(InputarAmigo());

            nota.ApresentarMensagem("\nAmigo Cadastrado com Sucesso", TipoMensagem.Sucesso);
        }
        public void EditarRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Edita");

            int numeroSelecionado = ObtemNumeroAmigo();

            Amigo amigoAtualizado = InputarAmigo();

            amigoAtualizado.numero = numeroSelecionado;

            repositorioAmigo.Editar(numeroSelecionado, amigoAtualizado);

            nota.ApresentarMensagem("Amigo editado com sucesso", TipoMensagem.Sucesso);
        }
        public bool VisualizarRegistros(string tipo)
        {
            MostrarTitulo("Visualização de Amigos");

            List<Amigo> amigos = repositorioAmigo.SelecionarTodos();

            foreach (Amigo am in amigos)
            {
                Console.WriteLine("Número: " + am.numero);
                Console.WriteLine("Nome: " + am.Nome);
                Console.WriteLine("Nome do responsável: " + am.NomeResponsavel);
                Console.WriteLine("Onde mora: " + am.Endereco);

                Console.WriteLine();
            }

            return true;
        }
        public void ExcluirRegistro()
        {
            MostrarTituloEVerificarRegistroVazio("Exclui");

            int numero = ObtemNumeroAmigo();

            repositorioAmigo.Excluir(numero);
        }
 
        public bool VisualizarAmigosComMulta(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Amigos com Multa");

            List<Amigo> amigos = repositorioAmigo.SelecionarAmigosComMulta();

            foreach (Amigo am in amigos)
            {
                Console.WriteLine("Número: " + am.numero);
                Console.WriteLine("Nome: " + am.Nome);
                Console.WriteLine("Nome do responsável: " + am.NomeResponsavel);
                Console.WriteLine("Onde mora: " + am.Endereco);
                Console.WriteLine("Multa: R$" + am.multa.Valor);

                Console.WriteLine();
            }

            return true;
        }
 
        public void PagarMulta()
        {
            MostrarTitulo("Pagamento de Multas");

            bool temAmigosComMulta = VisualizarAmigosComMulta("Pesquisando");

            if (!temAmigosComMulta)
            {
                nota.ApresentarMensagem("Não há nenhum amigo com multas em aberto", TipoMensagem.Atencao);
                return;
            }

            int numeroAmigoComMulta = ObtemNumeroAmigo();

            Amigo amigoComMulta = repositorioAmigo.SelecionarObjeto(numeroAmigoComMulta);

           amigoComMulta.PagarMulta();
        }

        public Amigo ObtemAmigo()
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

            Amigo itemSelecionado = repositorioAmigo.SelecionarObjeto(numero);

            return itemSelecionado;

        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo("Cadastrando Novo Amigo");

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Amigo com Multa");
            Console.WriteLine("Digite 6 para Pagar Multa");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        #region métodos privados
        private static Amigo InputarAmigo()
        {
            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o número do telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite onde o amigo mora: ");
            string endereco = Console.ReadLine();

            Amigo NovoAmigo = new(nome, nomeResponsavel, telefone, endereco);
            
            return NovoAmigo;
        }

        private int ObtemNumeroAmigo()
        {
            int numero;
            bool numeroEncontrado;

            do
            {
                Console.Write("Digite o número que deseja selecionar: ");
                numero = Convert.ToInt32(Console.ReadLine());

                numeroEncontrado = repositorioAmigo.VerificarNumeroExistente(numero);

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
