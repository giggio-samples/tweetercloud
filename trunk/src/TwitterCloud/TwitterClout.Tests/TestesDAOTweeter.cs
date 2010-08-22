using LinqToTwitter;
using StoryQ;
using NUnit.Framework;
using TwitterCloud.WebApp.Models.Infra.Dados;

namespace TwitterClout.Tests
{

    [TestFixture]
    public class TestesDAOTweeter
    {
        private DAOTwitter _dao;
        private string _hashtag;
        private Search _pesquisa;

        [Test]
        public void PesquisarNoTwitter()
        {
            new Historia("Pesquisar no twitter")
                .Para("Obter tweets")
                .Enquanto("Cliente do DAO de Tweets")
                .EuQuero("Pesquisar Uma hashtag")

                        .ComCenario("Pesquisando #TDC2010")
                            .Dado(UmaHashTag_, "#TDC2010")
                                .E(UmDAODeTwitter)
                            .Quando(PesquisoAHashtag)
                            .Entao(NãoÉNulo)
                                .E(Há_Tweets, 10)
                                .E(ReceboTweetsComAHashtag)
                .ExecuteWithReport();
        }


        private void UmaHashTag_(string hashtag)
        {
            _hashtag = hashtag;
        }

        private void UmDAODeTwitter()
        {
            _dao = new DAOTwitter();
        }

        private void PesquisoAHashtag()
        {
            _pesquisa = _dao.Pesquisar(_hashtag);
        }

        private void NãoÉNulo()
        {
            Assert.IsNotNull(_pesquisa);
        }

        private void Há_Tweets(int quantidadeEsperadaDeTweets)
        {
            Assert.AreEqual(quantidadeEsperadaDeTweets, _pesquisa.Entries.Count);
        }

        private void ReceboTweetsComAHashtag()
        {
            _pesquisa.Entries.ForEach(s => StringAssert.Contains(_hashtag.ToLower(), s.Content.ToLower()));
        }
    }
    [TestFixture]
    public class TestesAgregadorTweets
    {
        [Test]
        public void PesquisarNoTwitter()
        {
        }
    }
}
