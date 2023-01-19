using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace Paises
{
    class Program
    {
        static void Main(string[] args)
        {
           
           
            var fileName = "db.json"; //Caminho do arquivo q vai receber o JSON
            dynamic json = File.ReadAllText(fileName); //Leitura do arquivo JSON
            //Deserializando o arquivo JSON e convertendo ele em uma lista de objetos
            var Dados = JsonConvert.DeserializeObject<List<Paises.dados>>(json);

            //Listas que foram criadas
            List<string> Lang = new List<string>(); //Lista de todos os idiomas
            List<MaiorPais> PaisMaior = new List<MaiorPais>(); //Lista do pais com mais idiomas
            List<MaisFaladas> MaiorLingua = new List<MaisFaladas>(); //Lista de idiomas mais falados

            //Variaveis
            var size = Dados.Count; //Quantidade de paises
            var NomePais = "";
            var NomePaisAnterior = "";
            var Qtd = 0;
            var QtdAnterior = 0;
            var result = Lang.GroupBy(x => x);
            var temp = 0;
            var PrimeiraLingua = "";
            var SegundaLingua = "";
            var TerceiraLingua = "";
            var qtdLingua1 = 0;
            var qtdLingua2 = 0;
            var qtdLingua3 = 0;



            for (int i = 0; i < Dados.Count; i++)
            {
                // inserindo dados na lista do pais com mais idiomas
                var ItemMaior = new MaiorPais 
                {
                    nomepais = Dados[i].country,
                    qtdpais = Dados[i].languages.Count
                };
                PaisMaior.Add(ItemMaior);
                
                Qtd = Dados[i].languages.Count;
                if (Qtd > QtdAnterior)
                {
                    QtdAnterior = Qtd;
                    NomePais = Dados[i].country;

                }
                //Inserindo dados na lista de idiomas 
                for (int n = 0; n < Dados[i].languages.Count; n++)
                {
                    Lang.Add(Dados[i].languages[n]);
                }
                //Inserindo dados na lista de idiomas mais falados
                for(int b = 0; b < Lang.Count; b++)
                {
                    var ItemLang = new MaisFaladas
                    {
                        nomelingua = Dados[i].country,
                        qtdlingua = Dados[i].languages.Count
                    };
                    MaiorLingua.Add(ItemLang);
                }
            }

            //Verificando as 3 linguas mais faladas

            foreach (var g in result)
            {
                if (g.Count() > qtdLingua1)
                {
                    qtdLingua1 = g.Count();
                    PrimeiraLingua = g.Key;
                }
            }
            foreach (var g in result)
            {
                if (g.Count() > qtdLingua2 && g.Key != PrimeiraLingua)
                {
                    qtdLingua2 = g.Count();
                    SegundaLingua = g.Key;
                }
            }
            foreach (var g in result)
            {
                if (g.Count() > qtdLingua3 && g.Key != PrimeiraLingua && g.Key != SegundaLingua)
                {
                    qtdLingua3 = g.Count();
                    TerceiraLingua = g.Key;
                }
            }




            Console.WriteLine(" ---------------------------------------------------------");
            Console.WriteLine("|                          Teste                          |");
            Console.WriteLine(" ---------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Quantidade de paises na lista: " + size);
            Console.WriteLine(" ---------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Lista de todas as linguas presentes no arquivo:");
            Console.WriteLine("");          
            
            foreach(var item in Lang)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" ---------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Pais com a maior quantidade de linguas faladas: " + NomePais);
            Console.WriteLine("Quantidade de linguas: " + QtdAnterior);
            Console.WriteLine(" ---------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Lingua mais falada: " + PrimeiraLingua + "  " + qtdLingua1 + " vezes");
            Console.WriteLine("Segunda lingua mais falada: " + SegundaLingua + "  " + qtdLingua2 + " vezes");
            Console.WriteLine("Terceira lingua mais falada: " + TerceiraLingua + "  " + qtdLingua3 + " vezes");
            Console.WriteLine(" ---------------------------------------------------------");
            Console.ReadLine();

        }
    }
}