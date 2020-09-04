using FlyRemotely.Models;
using System.Collections.Generic;

namespace FlyRemotely.ViewModels
{
    public class EditOfferViewModel
    {
        public Offer Offer { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public bool? Confirm { get; set; }
    }
}