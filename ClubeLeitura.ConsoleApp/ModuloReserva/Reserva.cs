using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.Superclasses;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class Reserva : EntidadeBase, IValidavel
    {
        public Amigo amigo;
        public Revista revista;
        public DateTime dataInicialReserva;
        public bool estaAberta;

        public Reserva()
        {
        }

        public void Abrir()
        {
            if (!estaAberta)
            {
                estaAberta = true;
                dataInicialReserva = DateTime.Today;
            }
        }

        public void Fechar()
        {
            if (estaAberta)
                estaAberta = false;
        }

        public bool EstaExpirada()
        {
            bool ultrapassouDataReserva = dataInicialReserva.AddDays(2) > DateTime.Today;

            if (ultrapassouDataReserva)
                estaAberta = false;

            return ultrapassouDataReserva;
        }

        public string Validar()
        {
            throw new NotImplementedException();
        }
    }
}
