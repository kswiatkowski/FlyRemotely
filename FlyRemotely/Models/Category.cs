using System.Collections.Generic;

namespace FlyRemotely.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}