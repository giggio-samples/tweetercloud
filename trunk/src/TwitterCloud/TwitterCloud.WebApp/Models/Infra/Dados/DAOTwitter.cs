using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;

namespace TwitterCloud.WebApp.Models.Infra.Dados
{
    public class DAOTwitter
    {
        public Search Pesquisar(string hashtag)
        {
            var twitterCtx = new TwitterContext();
            var stringDePesquisa = string.Format("{0}", hashtag);
            var queryResults =
                from pesquisa in twitterCtx.Search
                where pesquisa.Type == SearchType.Search &&
                      pesquisa.Query == stringDePesquisa &&
                      pesquisa.Page == 1 &&
                      pesquisa.PageSize == 30
                select pesquisa;
            var tweets = queryResults.Single().Entries.Select(e => e.Title);
            var tweet = queryResults.Single().Entries.First();
            var stringtweets = "";
            foreach (var stringtweet in tweets)
            {
                stringtweets += @"""" + stringtweet +  "\"\n" ;
            }
            return queryResults.Single();
        }
    }
}