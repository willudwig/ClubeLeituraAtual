using System;
using System.Collections.Generic;
using ClubeLeitura.ConsoleApp.Superclasses;


namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class RepositorioAmigo : RepositorioBase<Amigo>
    {
        public List<Amigo> SelecionarAmigosComMulta()
        {
            return registro.FindAll(r => r.multa.Equals(true));
        }
    }
}
