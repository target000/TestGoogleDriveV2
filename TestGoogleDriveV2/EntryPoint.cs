
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
    class EntryPoint
    {
        private const int NUM_OF_RESULTS = 1000;

        static void Main(string[] args)
        {
            // Authenticate 
            Auth auth = new Auth();
            DriveService service = auth.Service;

            // Define parameters of request, note max cannot be larger than 1000 as per API
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.MaxResults = NUM_OF_RESULTS;

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
            //Operation.deleteFile(service, testFolder.Id);
            //Console.WriteLine(testFolder.Id + " is now deleted! ");

            Console.WriteLine(" --------  ");

            // insert a file
            string fileName = @"C:\hello_drive_1.txt";
            var testFile = Operation.uploadFile(service, fileName, service.About.Get().Execute().RootFolderId);
            Console.WriteLine("\"" + testFile.Title + "\"" + " was created! ");


            // delete the file
            //Operation.deleteFile(service, testFile.Id);
            //Console.WriteLine(testFile.Id + " is now deleted! ");

            //
            Console.Read();

        }
    }
}