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
            var stringaoDePalavras = ObterStringaoDePalavras(tweets);
            var agregacoesDeTweets = new List<AgregacaoDeTweets>();
            foreach (var palavra in stringaoDePalavras.Split(' '))
            {
                var palavra1 = palavra;
                palavra1 = RemoverCaracteresIndesejados(palavra1);
                if (Pular(palavra1)) continue;
                
                var agregacaoDeTweet = agregacoesDeTweets.FirstOrDefault(a => a.Palavra == palavra1);
                var palavraNova = agregacaoDeTweet == null;
                if (palavraNova)
                    agregacoesDeTweets.Add(new AgregacaoDeTweets(palavra));
                else
                    agregacaoDeTweet.TornarMaisImportante();
            }
            agregacoesDeTweets = LimitarNumeroDeAgregacoes(agregacoesDeTweets);
            return agregacoesDeTweets;
        }

        private List<AgregacaoDeTweets> LimitarNumeroDeAgregacoes(List<AgregacaoDeTweets> agregacoesDeTweets)
        {
            if (agregacoesDeTweets.Count > 30)
                agregacoesDeTweets = (from a in agregacoesDeTweets
                                      orderby a.Importancia descending
                                      select a).Take(30).ToList();
            return agregacoesDeTweets;
        }

        private string ObterStringaoDePalavras(IEnumerable<string> tweets)
        {
            var stringaoDePalavras = string.Join(" ", tweets).ToLower();
            stringaoDePalavras = EliminarEspacos(stringaoDePalavras);
            return stringaoDePalavras;
        }

        private string EliminarEspacos(string stringaoDePalavras)
        {
            while (stringaoDePalavras.Contains("  "))
                stringaoDePalavras = stringaoDePalavras.Replace("  ", " ");
            return stringaoDePalavras;
        }

        private bool Pular(string palavra1)
        {
            var pular = false;
            foreach (var palavraQueIniciaParaPular in PalavrasQueIniciamParaPular)
            {
                if (palavra1.ToLower().StartsWith(palavraQueIniciaParaPular.ToLower()))
                    pular = true;
            }
            if (PalavrasParaPular.Contains(palavra1.ToUpper()))
                pular = true;
            if (palavra1.Length <= 3)
                pular = true;
            return pular;
        }

        private string RemoverCaracteresIndesejados(string palavra1)
        {
            foreach (var pontuacaoParaRemover in PontuacaoParaRemover)
            {
                while (palavra1.EndsWith(pontuacaoParaRemover.ToString()) && palavra1.Length > 1)
                    palavra1 = palavra1.Substring(palavra1.Length - 1);
                while (palavra1.StartsWith(pontuacaoParaRemover.ToString()) && palavra1.Length > 1)
                    palavra1 = palavra1.Substring(1, palavra1.Length - 1);
            }
            return palavra1;
        }
    }

}