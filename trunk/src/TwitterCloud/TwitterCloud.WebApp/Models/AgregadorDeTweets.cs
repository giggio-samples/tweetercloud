using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitterCloud.WebApp.Models
{
    public class AgregadorDeTweets
    {
        private static readonly string[] PalavrasParaPular =
            {
                "RT",
                "para"
            };
        private static readonly string[] PalavrasQueIniciamParaPular =
            {
                "http://"
            };
        private static readonly char[] PontuacaoParaRemover =
            {
                '!',
                '.',
                '?',
                ';',
                ':',
                '"',
                '\'',
                '~',
                '(',
                ')',
                ','
            };
        public IEnumerable<AgregacaoDeTweets> Agregar(IEnumerable<string> tweets)
        {
            var stringaoDePalavras = string.Join(" ", tweets).ToLower();
            while (stringaoDePalavras.Contains("  "))
                stringaoDePalavras = stringaoDePalavras.Replace("  ", " ");
            var agregacoesDeTweets = new List<AgregacaoDeTweets>();
            foreach (var palavra in stringaoDePalavras.Split(' '))
            {
                var palavra1 = palavra;
                
                foreach (var pontuacaoParaRemover in PontuacaoParaRemover)
                {
                    while (palavra1.EndsWith(pontuacaoParaRemover.ToString()) && palavra1.Length > 1)
                        palavra1 = palavra1.Substring(palavra1.Length - 1);
                    while (palavra1.StartsWith(pontuacaoParaRemover.ToString()) && palavra1.Length > 1)
                        palavra1 = palavra1.Substring(1, palavra1.Length - 1);
                }
                bool pular = false;
                foreach (var palavraQueIniciaParaPular in PalavrasQueIniciamParaPular)
                {
                    if (palavra1.ToLower().StartsWith(palavraQueIniciaParaPular.ToLower()))
                        pular = true;
                }
                if (pular) continue;
                if (PalavrasParaPular.Contains(palavra1.ToUpper()))
                    continue;
                if (palavra1.Length <= 3)
                    continue;

                var agregacaoDeTweet = agregacoesDeTweets.FirstOrDefault(a => a.Palavra == palavra1);
                if (agregacaoDeTweet == null)
                {
                    agregacaoDeTweet = new AgregacaoDeTweets(palavra);
                    agregacoesDeTweets.Add(agregacaoDeTweet);
                }
                else
                {
                    agregacaoDeTweet.TornarMaisImportante();
                }
            }
            if (agregacoesDeTweets.Count > 30)
            {
                agregacoesDeTweets = (from a in agregacoesDeTweets
                                      orderby a.Importancia descending
                                      select a).Take(30).ToList();
            }
            return agregacoesDeTweets;
        }
    }

}