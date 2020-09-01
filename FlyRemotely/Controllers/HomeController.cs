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
            var offers = db.Offers.Include("Category").Where(x => x.Status == Models.OfferStatus.Aktywne).OrderBy(x => Guid.NewGuid()).Take(6).ToList();
            var categories = db.Categories.ToList();

            var vm = new HomeViewModel { FeaturedOffers = featuredOffers, NormalOffers = offers, Categories = categories };
            return View(vm);
        }
    }
}