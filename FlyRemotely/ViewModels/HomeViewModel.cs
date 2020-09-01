using FlyRemotely.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlyRemotely.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Offer> FeaturedOffers { get; set; }
        public IEnumerable<Offer> NormalOffers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}