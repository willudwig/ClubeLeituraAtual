using ClubeLeitura.ConsoleApp.Superclasses;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class RepositorioReserva : RepositorioBase<Reserva>
    {
        public override string Inserir(Reserva reserva)
        {
            reserva.Abrir();
            reserva.revista.RegistrarReserva(reserva);
            reserva.amigo.RegistrarReserva(reserva);

            base.Inserir(reserva);

            return "Valido";
        }

        public List<Reserva> SelecionarReservasEmAberto()
        {
            List<Reserva> reservasInseridas = null;
            
            registro = SelecionarTodos();

            foreach (Reserva res in registro)
            {
                if(res.estaAberta == true)
                    reservasInseridas.Add(res);
            }

            return reservasInseridas;
        }

    }
}
