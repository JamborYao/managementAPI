using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.WindowsAzure.Management.Storage;
using Microsoft.WindowsAzure.Management.Storage.Models;
using Microsoft.WindowsAzure.Management.Models;
using Microsoft.WindowsAzure.Management.Compute.Models;
using Microsoft.WindowsAzure.Subscriptions;

namespace azureManagementAPI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
        

            SubscriptionCloudCredentials cre = new CertificateCloudCredentials(
                "8692422d-d6df-4b0a-a1b0-f296e4a43841",
                new System.Security.Cryptography.X509Certificates.X509Certificate2(Convert.FromBase64String("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")));

            var subscriptionClient = new SubscriptionClient(cre, new Uri("https://management.core.chinacloudapi.cn/"));
            subscriptionClient.Subscriptions.List();



            var token = GetAuthorizationHeader();
            var credential = new TokenCloudCredentials(ConfigurationManager.AppSettings["subscriptionId"], token);
            var computeClient = new ComputeManagementClient(cre, new Uri("https://management.core.chinacloudapi.cn/")); //, new Uri("https://management.core.chinacloudapi.cn/")


            var hostServes = computeClient.HostedServices.List();
          
            Console.ReadKey();
        }


        private static string GetAuthorizationHeader()
        {
            AuthenticationResult result = null;

            var context = new AuthenticationContext(string.Format(
              ConfigurationManager.AppSettings["login"],
              ConfigurationManager.AppSettings["tenantId"]));
            //string clientId = "72305ada-c635-4f18-bb2e-f96bd519f149";
            //string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
            //ClientCredential clientCred = new ClientCredential(clientId, clientSecret);
            //nf1AkPAnrvEBrRP5g+g3Yea1W+CLpctwJysAha5ndF8=
            var thread = new Thread(() =>
            {
                result = context.AcquireToken(
                  ConfigurationManager.AppSettings["apiEndpoint"],
                  ConfigurationManager.AppSettings["clientId"],
                  new Uri(ConfigurationManager.AppSettings["redirectUri"]));
            });
            //var thread = new Thread(() =>
            //{
            //    result = context.AcquireToken(
            //      "https://management.core.windows.net/",
            //      clientCred);
            //});
            thread.SetApartmentState(ApartmentState.STA);
            thread.Name = "AquireTokenThread";
            thread.Start();
            thread.Join();

            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            string token = result.AccessToken;
            string token1 = result.CreateAuthorizationHeader().Substring("Bearer ".Length);
            return token;
        }

    }
}
