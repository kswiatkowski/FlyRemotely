using FlyRemotely.DAL;
using FlyRemotely.Models;
using System.Web.Mvc;

namespace FlyRemotely.Controllers
{
    public class HomeController : Controller
    {
        private FlyRemotelyContext db = new FlyRemotelyContext();

        public ActionResult Index()
        {

            return View();
        }
    }
}