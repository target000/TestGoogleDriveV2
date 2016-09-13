using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleDriveSimpleDemo
{
    class Auth
    {

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json

        // TODO if at all possible narrow the scope
        private static string[] Scopes = { DriveService.Scope.Drive,
                                           DriveService.Scope.DriveFile };
        private static string ApplicationName = "Drive API Demo Test";


        public DriveService Service { get; set; }
        public string CredPath { get; set; }
        public UserCredential Credential { get; set; }

        //private DriveService service = null;
        //private string credPath = null;

        public Auth()
        {
            // TODO change the way json is passed
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                // store credential locally
                CredPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                CredPath = Path.Combine(CredPath, ".credentials/xilu_test.json");
                Credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    new FileDataStore(CredPath, true)).Result;

                Console.WriteLine("Credential file saved to: " + CredPath);
            }

            // Create Drive API service
            Service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = Credential,
                ApplicationName = ApplicationName,
            });

        }


    }
}
