using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class Arquivo
    {
        readonly object _entidade;
        readonly string _diretorio;

        private string ObterDiretorio()
        {
            return @"C:\Users\Thais\OneDrive\Área de Trabalho\Cantinho do William\ProjetosClonados\ClubeLeituraTiago\ClubeLeitura.ConsoleApp\Compartilhado\Json\" + _entidade.GetType().Name.ToString() + @".json";
        }

        public Arquivo(Object entidade)
        {
            _entidade = entidade;
            _diretorio = ObterDiretorio();
        }

        public Object ObterArquivoJson()
        {
            try
            {
                string pegaArquivo;

                using (var reader = new StreamReader(_diretorio))
                {
                    pegaArquivo = reader.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<Object>(pegaArquivo);
            }
            catch (Exception)
            {
                Console.WriteLine("Algo errado");
                Console.ReadKey();
                throw;
            }
        }

        public void GuardarArquivoJson()
        {
            try
            {
                var escreverNoArquivo = JsonConvert.SerializeObject(_entidade, Formatting.Indented);

                using (var writer = new StreamWriter(_diretorio))
                {
                    writer.Write(escreverNoArquivo);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Algo errado");
                Console.ReadKey();
                throw;
            }
        }
    }
}
