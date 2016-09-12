
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
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.Drive,
                                   DriveService.Scope.DriveFile };
        static string ApplicationName = "Drive API Demo Test";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request, note that max can only be 100 as per Google API
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.MaxResults = 1000;

            // List files
            IList<Google.Apis.Drive.v2.Data.File> files = listRequest.Execute().Items;


            Console.WriteLine("Files:");
            Console.WriteLine("-------------------------------");

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Title, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }

            Console.WriteLine("-------------------------------");
            Console.WriteLine();
            Console.WriteLine("insert files");


            // insert a directory/folder
            string folderName = "my awesome test folder 426";
            var testFolder = Operation.createDirectory(service, folderName, "This is for testing", service.About.Get().Execute().RootFolderId);
            Console.WriteLine("\"" + folderName + "\"" + " was created! ");



            // delete the folder
            Operation.deleteFile(service, testFolder.Id);
            Console.WriteLine(testFolder.Id + " is now deleted! ");

            Console.WriteLine(" --------  ");

            // insert a file
            string fileName = @"C:\hello_drive_1.txt";
            var testFile = Operation.uploadFile(service, fileName, service.About.Get().Execute().RootFolderId);
            Console.WriteLine("\"" + testFile.Title + "\"" + " was created! ");


            // delete the file
            Operation.deleteFile(service, testFile.Id);
            Console.WriteLine(testFile.Id + " is now deleted! ");

            //
            Console.Read();

        }
    }
}