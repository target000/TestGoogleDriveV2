using Google.Apis.Drive.v2;

using System;
using System.Collections.Generic;

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
            var folderName = "my awesome test folder 426";
            var testFolder = Operation.createDirectory(service, folderName, "This is for testing", service.About.Get().Execute().RootFolderId);
            Console.WriteLine("\"" + folderName + "\"" + " was created! ");



            // delete the folder
            //Operation.deleteFile(service, testFolder.Id);
            //Console.WriteLine(testFolder.Id + " is now deleted! ");

            Console.WriteLine(" --------  ");

            // insert a file
            var fileName = @"C:\hello_drive_1.txt";
            var numOfUploads = 3;
            for (var i = 0; i < numOfUploads; i++)
            {
                var testFile = Operation.uploadFile(service, fileName, service.About.Get().Execute().RootFolderId);
                Console.WriteLine("\"" + testFile.Title + "\"" + " was created! File ID = " + testFile.Id);
            }

            // delete the file
            //Operation.deleteFile(service, testFile.Id);
            //Console.WriteLine(testFile.Id + " is now deleted! ");


            // delete code using the displayed hashcode
            Console.WriteLine("*** Test file deletion on the drive *** ");

            char myChar;
            do
            {
                var toBeDeleted = Console.ReadLine();
                Operation.deleteFile(service, toBeDeleted);
                Console.WriteLine("To delete more press y ... ");
                myChar = Console.ReadKey().KeyChar;
            } while (myChar == 'y');



            //
            Console.Read();

        }
    }
}