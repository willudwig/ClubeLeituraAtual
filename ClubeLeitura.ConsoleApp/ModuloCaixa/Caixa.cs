﻿
using ClubeLeitura.ConsoleApp.Superclasses;
using ClubeLeitura.ConsoleApp.Compartilhado;


namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa : EntidadeBase, IValidavel
    {
        private readonly string cor;
        private readonly string etiqueta;
        public string Cor => cor;
        public string Etiqueta => etiqueta;

        public Caixa(string cor, string etiqueta)
        {
            this.cor = cor;
            this.etiqueta = etiqueta;
        }

        public string Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}
