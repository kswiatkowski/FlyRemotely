using FlyRemotely.DAL;
using FlyRemotely.Infrastructure;
using FlyRemotely.Models;
using FlyRemotely.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FlyRemotely.Controllers
{
    public class HomeController : Controller
    {
        private FlyRemotelyContext db = new FlyRemotelyContext();

        public ActionResult Index()
        {
            var offers = db.Offers.Include("Category").Where(x => x.Status == Models.OfferStatus.Aktywne).OrderBy(x => Guid.NewGuid()).Take(6).ToList();

            ICacheProvider cache = new DefaultCacheProvider();

            List<Category> categories;
            if (cache.IsSet(Consts.CategoriesCacheKey))
            {
                categories = cache.Get(Consts.CategoriesCacheKey) as List<Category>;
            }
            else
            {
                categories = db.Categories.ToList();
                cache.Set(Consts.CategoriesCacheKey, categories, 24);
            }

            List<Offer> featuredOffers;
            if (cache.IsSet(Consts.FeaturedCacheKey))
            {
                featuredOffers = cache.Get(Consts.FeaturedCacheKey) as List<Offer>;
            }
            else
            {
                featuredOffers = db.Offers.Include("Category").Where(x => x.Featured && x.Status == Models.OfferStatus.Aktywne).OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                cache.Set(Consts.FeaturedCacheKey, featuredOffers, 24);
            }

            var vm = new HomeViewModel { FeaturedOffers = featuredOffers, NormalOffers = offers, Categories = categories };
            return View(vm);
        }
    }
}