
using ClubeLeitura.ConsoleApp.Superclasses;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class RepositorioCaixa : RepositorioBase<Caixa>
    {
        public bool EtiquetaJaUtilizada(string etiquetaInformada)
        {
            bool etiquetaJaUtilizada = false;

            foreach (Caixa c in registro)
            {
                if (c.Etiqueta == etiquetaInformada)
                    etiquetaJaUtilizada = true;
            }

            return etiquetaJaUtilizada;
        }
    }
}
