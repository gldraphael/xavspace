using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XavSpace.Facade
{
    public static class KeyRepository
    {
        public static string FacebookAppId { 
            get 
            { 
#if DEBUG
                return ConfigurationManager.AppSettings["FacebookAppIdTest"]; 
#else
                return ConfigurationManager.AppSettings["FacebookAppId"]; 
#endif
            }
        }

        public static string FacebookAppSecret
        {
            get
            {
#if DEBUG
                return ConfigurationManager.AppSettings["FacebookAppSecretTest"];
#else
                return ConfigurationManager.AppSettings["FacebookAppSecret"]; 
#endif
            }
        }

        public static class Constants
        {
            public const string FacebookAccessToken = "urn:facebook:access_token";
        }
    }
}
