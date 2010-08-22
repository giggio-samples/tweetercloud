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
                      pesquisa.PageSize == 10
                select pesquisa;
            return queryResults.Single();
        }
    }
}