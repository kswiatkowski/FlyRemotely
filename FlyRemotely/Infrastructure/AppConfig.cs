using System.Configuration;

namespace FlyRemotely.Infrastructure
{
    public class AppConfig
    {
        private static string companyPhotoSourceFolder = ConfigurationManager.AppSettings["CompanyPhotoSourceFolder"];
        public static string CompanyPhotoSourceFolder
        {
            get
            {
                return companyPhotoSourceFolder;
            }
        }
    }
}