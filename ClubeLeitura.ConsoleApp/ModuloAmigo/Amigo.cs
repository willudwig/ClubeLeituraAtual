using ClubeLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeLeitura.ConsoleApp.ModuloReserva;
using ClubeLeitura.ConsoleApp.Superclasses;
using ClubeLeitura.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class Amigo : EntidadeBase, IValidavel
    {
        private readonly string nome;
        private readonly string nomeResponsavel;
        private readonly string telefone;
        private readonly string endereco;

        public Multa multa;

        private readonly List<Emprestimo> historicoEmprestimos;
        private readonly List<Reserva> historicoReservas;
       
        public string Nome => nome;

        public string NomeResponsavel => nomeResponsavel;

        public string Telefone => telefone;

        public string Endereco => endereco;

        public Amigo(string nome, string nomeResponsavel, string telefone, string endereco)
        {
            this.nome = nome;
            this.nomeResponsavel = nomeResponsavel;
            this.telefone = telefone;
            this.endereco = endereco;
        }

        public void RegistrarEmprestimo(Emprestimo emprestimo)
        {
            historicoEmprestimos.Add(emprestimo);
        }

        public void RegistrarReserva(Reserva reserva)
        {
            historicoReservas.Add(reserva);
        }

        public bool TemReservaEmAberto()
        {
            List<Reserva> temEmAberto = historicoReservas.FindAll(hr => hr.estaAberta.Equals(true));

            if(temEmAberto != null)
            {
                return false;
            }
            else return true;
        }

        public bool TemEmprestimoEmAberto()
        {
            List<Emprestimo> temEmAberto = historicoEmprestimos.FindAll(he => he.estaAberto.Equals(true));

            if (temEmAberto != null)
            {
                return false;
            }
            else return true;
        }

        public void RegistrarMulta(decimal valor)
        {
            Multa novaMulta = new (valor);

            multa = novaMulta;
        }

        public void PagarMulta()
        {
            if (multa != null)
                multa = null;
        }

        public bool TemMultaEmAberto()
        {
            if (multa == null)
                return false;

            return true;
        }

        public string Validar()
        {
            throw new System.NotImplementedException();
        }

        #region Métodos privados
        private int ObtemPosicaoVazia()
        {
            for (int i = 0; i < historicoEmprestimos.Count; i++)
            {
                if (historicoEmprestimos[i] == null)
                    return i;
            }

            return -1;
        }

        private int ObtemPosicaoReservasVazia()
        {
            for (int i = 0; i < historicoReservas.Count; i++)
            {
                if (historicoReservas[i] == null)
                    return i;
            }

            return -1;
        }

        #endregion
    }
}