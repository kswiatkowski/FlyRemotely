using FlyRemotely.DAL;
using FlyRemotely.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FlyRemotely.Controllers
{
    public class HomeController : Controller
    {
        private FlyRemotelyContext db = new FlyRemotelyContext();

        public ActionResult Index()
        {
            var featuredOffers = db.Offers.Include("Category").Where(x => x.Featured && x.Status == Models.OfferStatus.Aktywne).OrderBy(x => Guid.NewGuid()).Take(2).ToList();
            var normalOffers = db.Offers.Include("Category").Where(x => !x.Featured && x.Status == Models.OfferStatus.Aktywne).OrderBy(x => Guid.NewGuid()).Take(6).ToList();

            var vm = new HomeViewModel { FeaturedOffers = featuredOffers, NormalOffers = normalOffers };
            return View(vm);
        }
    }
}