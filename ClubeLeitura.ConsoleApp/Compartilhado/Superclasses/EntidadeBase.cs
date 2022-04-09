using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.Superclasses
{
    public abstract class EntidadeBase
    {
        public int numero = -1;

        public EntidadeBase()
        {
            numero++;
        }

    }
}
