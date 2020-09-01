using FlyRemotely.DAL;
using FlyRemotely.Models;
using MvcSiteMapProvider;
using System.Collections.Generic;

namespace FlyRemotely.Infrastructure
{
    public class CategoriesDynamicNodeProvider : DynamicNodeProviderBase
    {
        private FlyRemotelyContext db = new FlyRemotelyContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Category category in db.Categories)
            {
                DynamicNode node = new DynamicNode();
                node.Title = category.Name;
                node.Key = "Category_" + category.CategoryId;
                node.RouteValues.Add("categoryName", category.Name);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}