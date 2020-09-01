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

        public ActionResult Index(string searchQuery = null)
        {
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

            //version without search engine
            //var offers = db.Offers.Include("Category").Where(x => x.Status == Models.OfferStatus.Aktywne).OrderBy(x => Guid.NewGuid()).Take(6).ToList();
            var offers = db.Offers.Include("Category").Where(x => searchQuery == null ||
                                                        x.Title.ToLower().Contains(searchQuery.ToLower()) ||
                                                        x.Category.Name.ToLower().Contains(searchQuery.ToLower()) ||
                                                        x.CompanyName.ToLower().Contains(searchQuery.ToLower()) &&
                                                        x.Status == Models.OfferStatus.Aktywne).Take(6).ToList();

            if (Request.IsAjaxRequest())
                return PartialView("_PartialListOffers", offers);



            var vm = new HomeViewModel { FeaturedOffers = featuredOffers, NormalOffers = offers, Categories = categories };
            return View(vm);
        }

        public ActionResult SearchTips(string term)
        {
            var offers = db.Offers.Where(x => x.Status == Models.OfferStatus.Aktywne &&
                                                        (x.Title.ToLower().Contains(term.ToLower()))
                                                        || (x.Category.Name.ToLower().Contains(term.ToLower())))
                                                        .Take(4).Select(x => new { label = x.Title });

            return Json(offers, JsonRequestBehavior.AllowGet);
        }
    }
}