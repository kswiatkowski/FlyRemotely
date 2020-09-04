using FlyRemotely.App_Start;
using FlyRemotely.DAL;
using FlyRemotely.Infrastructure;
using FlyRemotely.Models;
using FlyRemotely.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FlyRemotely.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private FlyRemotelyContext db = new FlyRemotelyContext();
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialsViewModel
            {
                Message = message
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile()
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        public ActionResult OffersList()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Offer> userOffers;

            if (true)
            {
                userOffers = db.Offers.Include("Category").OrderByDescending(x => x.DateAdded).ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                userOffers = db.Offers.Include("Category").Where(x => x.UserId == userId).OrderByDescending(x => x.DateAdded).ToArray();
            }
            return View(userOffers);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public OfferStatus ChangeOfferStatus(Offer offer)
        {
            Offer offerToModify = db.Offers.Find(offer.OfferId);
            offerToModify.Status = offer.Status;
            db.SaveChanges();

            return offer.Status;
        }

        [Authorize]
        public ActionResult ChangeOfferStatusUser(int offerId, bool isActive)
        {
            Offer offerToModify = db.Offers.Find(offerId);
            if (isActive)
            {
                offerToModify.Status = Models.OfferStatus.Nieaktywne;
            }
            else
            {
                offerToModify.Status = Models.OfferStatus.Aktywne;
            }
            db.SaveChanges();
            return RedirectToAction("OffersList");
        }

        [Authorize]
        public ActionResult AddToFavorites(int favoriteOfferId)
        {
            string userId = User.Identity.GetUserId();
            var favoriteOffers = new List<Favorite>();
            favoriteOffers = db.Favourites.Where(x => x.UserId == userId && x.OfferId == favoriteOfferId).ToList();
            if (favoriteOffers.Count == 0)
            {
                db.Favourites.Add(new Favorite { OfferId = favoriteOfferId, UserId = userId });
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Catalog", new { offerId = favoriteOfferId });
        }

        [Authorize]
        public ActionResult FavoritesList()
        {
            string userId = User.Identity.GetUserId();
            List<Favorite> myFavourite = db.Favourites.Where(x => x.UserId == userId).ToList();
            List<int> myId = new List<int>();
            
            foreach (Favorite favourite in myFavourite)
            {
                myId.Add(favourite.OfferId);
            }
            
            IEnumerable<Offer> myOffer = db.Offers.Where(x => myId.Contains(x.OfferId)).ToArray();
            return View(myOffer);
        }

        [Authorize]
        public ActionResult RemoveFromFavorites(int favoriteOfferId)
        {
            string userId = User.Identity.GetUserId();
            Favorite itemToRemove = db.Favourites.First(x => x.OfferId == favoriteOfferId && x.UserId == userId);
            db.Favourites.Remove(itemToRemove);
            db.SaveChanges();

            return RedirectToAction("FavoritesList");
        }

        [Authorize]
        public ActionResult AddOffer(int? offerId, bool? confirm)
        {
            Offer offer;
            if (offerId.HasValue)
            {
                ViewBag.EditMode = true;
                offer = db.Offers.Find(offerId);
            }
            else
            {
                ViewBag.EditMode = false;
                offer = new Offer();
            }

            var result = new EditOfferViewModel();
            result.Categories = db.Categories.ToList();
            result.Offer = offer;
            //confirm when [HttpPost] AddOfffer succeed
            result.Confirm = confirm;

            return View(result);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddOffer(EditOfferViewModel model, HttpPostedFileBase file)
        {
            if (model.Offer.OfferId > 0)
            {
                // edit
                model.Offer.Status = Models.OfferStatus.Oczekuje;
                db.Entry(model.Offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddOffer", new { confirm = true });
            }
            else
            {
                //add
                if (file != null && file.ContentLength > 0)
                {
                    if (ModelState.IsValid)
                    {
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;
                        var path = Path.Combine(Server.MapPath(AppConfig.CompanyPhotoSourceFolder), filename);
                        file.SaveAs(path);

                        model.Offer.CompanyPhotoSource = filename;
                        model.Offer.DateAdded = DateTime.Now;
                        model.Offer.UserId = User.Identity.GetUserId();
                        model.Offer.Status = Models.OfferStatus.Oczekuje;

                        db.Offers.Add(model.Offer);
                        db.SaveChanges();

                        return RedirectToAction("AddOffer", new { confirm = true });
                    }
                    else
                    {
                        var categories = db.Categories.ToList();
                        model.Categories = categories;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    // in case of an error - the model will be forwarded, but without a category (to dropdown list)
                    var categories = db.Categories.ToList();
                    model.Categories = categories;
                    return View(model);
                }
            }
        }
    }
}