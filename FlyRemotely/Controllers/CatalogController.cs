using FlyRemotely.DAL;
using FlyRemotely.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace FlyRemotely.Controllers
{
    public class CatalogController : Controller
    {
        private FlyRemotelyContext db = new FlyRemotelyContext();
        public ActionResult Index(int? categoryId = null)
        {
            var offers = new List<Offer>();

            if (categoryId == null)
            {
                offers = db.Offers.Where(x => x.Status == Models.OfferStatus.Aktywne && x.Category.CategoryId == 1).OrderBy(x => x.Featured).ToList();
            }
            else
            {
                offers = db.Offers.Where(x => x.Status == Models.OfferStatus.Aktywne && x.Category.CategoryId == categoryId).OrderBy(x => x.Featured).ToList();
            }
            return View(offers);
        }

        [ChildActionOnly]
        public ActionResult GetCategories()
        {
            var categories = db.Categories.ToList();
            return PartialView("_PartialCategoriesNav", categories);
        }

        [ChildActionOnly]
        public ActionResult GetFeaturedOffers(int quantity = 1)
        {
            var featuredOffers = db.Offers.Where(x => x.Status == Models.OfferStatus.Aktywne && x.Featured).OrderBy(x => Guid.NewGuid()).Take(quantity).ToList();
            return PartialView("_PartialFeaturedOffers", featuredOffers);
        }

        public ActionResult Details(int offerId = 1)
        {
            var offer = db.Offers.Find(offerId);
            return View(offer);
        }
    }
}