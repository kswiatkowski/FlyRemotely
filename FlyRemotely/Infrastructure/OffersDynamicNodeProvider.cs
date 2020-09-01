using FlyRemotely.DAL;
using FlyRemotely.Models;
using MvcSiteMapProvider;
using System.Collections.Generic;

namespace FlyRemotely.Infrastructure
{
    public class OffersDynamicNodeProvider : DynamicNodeProviderBase
    {
        private FlyRemotelyContext db = new FlyRemotelyContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Offer offer in db.Offers)
            {
                DynamicNode node = new DynamicNode();
                node.Title = offer.Title;
                node.Key = "Offer_" + offer.OfferId;
                node.ParentKey = "Category_" + offer.CategoryId;
                node.RouteValues.Add("offerId", offer.OfferId);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}