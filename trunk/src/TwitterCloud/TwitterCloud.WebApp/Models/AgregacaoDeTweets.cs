namespace TwitterCloud.WebApp.Models
{
    public class AgregacaoDeTweets
    {
        public AgregacaoDeTweets(string palavra)
        {
            Importancia = 1;
            Palavra = palavra;
        }

        public int Importancia { get; protected set; }
        public string Palavra { get; protected set; }

        public void TornarMaisImportante()
        {
            Importancia++;
        }
        public override string ToString()
        {
            return string.Format("{0} tweets sobre {1}", Importancia, Palavra);
        }
    }
}