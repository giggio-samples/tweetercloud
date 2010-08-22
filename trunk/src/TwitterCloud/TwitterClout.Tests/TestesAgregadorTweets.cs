using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StoryQ;
using TwitterCloud.WebApp.Models;

namespace TwitterClout.Tests
{
    [TestFixture]
    public class TestesAgregadorTweets
    {
        private List<string> _tweets;
        private AgregadorDeTweets _agregador;
        private IEnumerable<AgregacaoDeTweets> _agregacao;

        [Test]
        public void AgregarTweets()
        {
            new Historia("Agregar Tweets")
                .Para("Agregar tweets a partir de pesquisas")
                .Enquanto("cliente de um agregador de tweets")
                .EuQuero("agregar tweets")

                        .ComCenario("vários tweets")
                            .Dado(UmaSérieDeStringsDeTweets)
                                .E(UmServicoDeAgregacaoDeTweets)
                            .Quando(AgregoOsTweets)
                            .Entao(Recebo_PalavrasAgregadas, 30)
                                .E(ReceboAsPalavrasAgregadasPorImportancia)
                                .E(NenhumaPalavraÉIgual)
                .Execute();
        }

        


        private void UmaSérieDeStringsDeTweets()
        {
            _tweets = new List<string>
                          {
                              @"RT @oclaudiobr: Muito bom #LEAN do @screscencio.  Como administrador alegra muito ver esses papos no desenvolvimento. Chega de falar p as paredes. #TDC2010",
                              @"@lucabastos:Bom demais o #TDC2010 Já vi organizador de evento pedir ajuda da família mas trazer a vó para palestrar e bombar foi a 1a vez.",
                              @"RT @alnascimento: Palestra sobre coaching do @manoelp começando agora no #tdc2010.",
                              @"Agora @manoelp falando sobre coaching em http://187.45.202.105/globalcodetv #TDC2010",
                              @"'Web Analytics Permite mensurar sucesso e propor melhorias em um ciclo virtuoso', Rodrigo Rubido #TDC2010 http://twitpic.com/2ha5hy",
                              @"Iniciando a palestra ""#Coaching e #Facilitação"" com Manoel Pimenta, ao vivo, no #TDC2010: http://187.45.202.105/globalcodetv",
                              @"Palestra sobre coaching do @manoelp começando agora no #tdc2010.",
                              @"RT @philHenri: Palestra do manifesto 2.0 nota 10 parabéns @alegomes #tdc2010",
                              @"Grande Manoel! =) RT @manoelp: Agora é eu!!!! #TDC2010 /via @manoelp",
                              @"RT @manoelp: Agora é eu!!!! #TDC2010 [ Pessoal,absorvam tudo o que conseguirem...Ele é fodástico ]"
                          };

        }

        private void UmServicoDeAgregacaoDeTweets()
        {
            _agregador = new AgregadorDeTweets();
        }

        private void AgregoOsTweets()
        {
            _agregacao = _agregador.Agregar(_tweets);
        }
        
        private void Recebo_PalavrasAgregadas(int palavrasAgregadas)
        {
            Assert.AreEqual(palavrasAgregadas, _agregacao.Count());
        }

        private void ReceboAsPalavrasAgregadasPorImportancia()
        {
            var agregacao = _agregacao.OrderByDescending(a => a.Importancia).ToArray();
            var palavra0 = agregacao[0].Palavra;
            var palavra1 = agregacao[1].Palavra;
            var palavra2 = agregacao[2].Palavra;
            var palavra3 = agregacao[3].Palavra;
            var palavra4 = agregacao[4].Palavra;
            var palavra5 = agregacao[5].Palavra;
            var palavra6 = agregacao[6].Palavra;
            var palavra7 = agregacao[7].Palavra;
            var palavra8 = agregacao[8].Palavra;
            var palavra9 = agregacao[9].Palavra;

            Assert.AreEqual("#tdc2010", palavra0);
            Assert.AreEqual("agora", palavra1);
            Assert.AreEqual("palestra", palavra2);
            Assert.AreEqual("@manoelp", palavra3);
            Assert.AreEqual("sobre", palavra4);
            Assert.AreEqual("coaching", palavra5);
            Assert.AreEqual("muito", palavra6);
            Assert.AreEqual("começando", palavra7);
            Assert.AreEqual("#lean", palavra8);
            Assert.AreEqual("como", palavra9);

        }
        private void NenhumaPalavraÉIgual()
        {
            CollectionAssert.AllItemsAreUnique(_agregacao.Select(a => a.Palavra));
        }

    }
}