using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterCloud.WebApp.Models;
using TwitterCloud.WebApp.Models.Infra.Dados;

namespace TwitterCloud.WebApp.Controllers
{
    public class TwitterController : Controller
    {
        //
        // GET: /Twitter/

        public ActionResult Index(string hashtag)
        {
            var dao = new DAOTwitter();
            var search = dao.Pesquisar(hashtag);
            var agregador = new AgregadorDeTweets();
            var pesquisa = agregador.Agregar(search.Entries.Select(e => e.Title));
            pesquisa = from p in pesquisa
                       where p.Palavra.ToLower() != hashtag.ToLower()
                       select p;
            return View(pesquisa);
        }

    }
}
