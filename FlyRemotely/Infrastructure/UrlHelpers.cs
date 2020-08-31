using System.IO;
using System.Web.Mvc;

namespace FlyRemotely.Infrastructure
{
    public static class UrlHelpers
    {
        public static string CompanyPhotoSourcePath(this UrlHelper helper, string PhotoSource)
        {
            var companyPhotoSourceFolder = AppConfig.CompanyPhotoSourceFolder;
            var path = Path.Combine(companyPhotoSourceFolder, PhotoSource);
            var absolutePath = helper.Content(path);
            return absolutePath;
        }
    }
}