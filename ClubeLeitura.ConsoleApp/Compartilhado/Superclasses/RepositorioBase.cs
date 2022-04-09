using System;
using System.Collections.Generic;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.Compartilhado;
using Newtonsoft.Json;
using System.IO;

namespace ClubeLeitura.ConsoleApp.Superclasses
{
     public abstract class RepositorioBase<TEntidadeBase> where TEntidadeBase:EntidadeBase, IValidavel
    {
        protected List<TEntidadeBase> registro = new();

        string amigo_Serializado, json;
        List<TEntidadeBase> amigo_Deserializado = new();


        public virtual string Inserir(TEntidadeBase objeto)
        {
            string status = Validar();

            if (status != "Válido")
                return status;

            registro.Add(objeto);

            if (objeto is Amigo)
            {
                Arquivo arq = new(objeto);
                arq.GuardarArquivoJson();
            }

            return status;
        }
        public virtual void Editar(int numeroSelecionado, TEntidadeBase objeto)
        {
            TEntidadeBase obj = registro.Find(r => r.numero.Equals(numeroSelecionado));
            registro.Remove(obj);
            Inserir(objeto);
        }
        public virtual void Excluir(int numeroSelecionado)
        {
            TEntidadeBase x = registro.Find(r => r.numero.Equals(numeroSelecionado));
            
            registro.Remove(x);
        }
        public List<TEntidadeBase> SelecionarTodos()
        {
            return registro;
        }
        public TEntidadeBase SelecionarObjeto(int numeroSelecionado)
        {
           TEntidadeBase obj = registro.Find(r => r.numero.Equals(numeroSelecionado));
            
           return obj;
        }
        public bool VerificarNumeroExistente(int numeroSelecionado)
        {
            TEntidadeBase x = registro.Find(r => r.numero.Equals(numeroSelecionado));

            if (x != null)
                return true;
            else
                return false;
        }

        public string Validar()
        {
            return "Válido";
        }

       
    }
}
