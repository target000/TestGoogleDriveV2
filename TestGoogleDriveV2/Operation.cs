using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Requests;
using System.Collections.Generic;
using System.Net;
using System;



public class Operation
{
    /// <summary>
    /// Insert new file.
    /// </summary>
    /// <param name="service">Drive API service instance.</param>
    /// <param name="title">Title of the file to insert, including the extension.</param>
    /// <param name="description">Description of the file to insert.</param>
    /// <param name="parentId">Parent folder's ID.</param>
    /// <param name="mimeType">MIME type of the file to insert.</param>
    /// <param name="filename">Filename of the file to insert.</param>
    /// <returns>Inserted file metadata, null is returned if an API error occurred.</returns>
    public static File insertFile(DriveService service, String title, String description, String parentId, String mimeType, String filename)
    {
        // File's metadata
        File body = new File();
        body.Title = title;
        body.Description = description;
        body.MimeType = mimeType;

        // Set the parent folder
        if (!String.IsNullOrEmpty(parentId))
        {
            body.Parents = new List<ParentReference>() { new ParentReference() { Id = parentId } };
        }

        // File's content
        byte[] byteArray = System.IO.File.ReadAllBytes(filename);
        System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

        try
        {
            FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, mimeType);
            request.Upload();

            File file = request.ResponseBody;

            // Uncomment the following line to print the File ID.
            // Console.WriteLine("File ID: " + file.Id);

            return file;
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
            return null;
        }
    }


    /// Create a new Directory 
    /// 
    /// a Valid authenticated DriveService
    /// The title of the file. Used to identify file or folder name.
    /// A short description of the file.
    /// Collection of parent folders which contain this file. 
    /// Setting this field will put the file in all of the provided folders. root folder.
    public static File createDirectory(DriveService service, string title, string description, string parent)
    {

        File newFolder = null;

        // Create metaData for a new Directory/Folder
        File body = new File();
        body.Title = title;
        body.Description = description;
        body.MimeType = "application/vnd.google-apps.folder"; // this is a folder/dir
        body.Parents = new List<ParentReference>() { new ParentReference() { Id = parent } };
        try
        {
            FilesResource.InsertRequest request = service.Files.Insert(body);
            newFolder = request.Execute();
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }

        return newFolder;
    }


    /// Uploads a file
    ///
    /// a Valid authenticated DriveService
    /// path to the file to upload
    /// Collection of parent folders which contain this file. 
    /// Setting this field will put the file in all of the provided folders. root folder.
    /// If upload succeeded returns the File resource of the uploaded file 
    /// If the upload fails returns null
    public static File uploadFile(DriveService service, string filePath, string parent)
    {

        if (System.IO.File.Exists(filePath))
        {
            File body = new File();
            body.Title = System.IO.Path.GetFileName(filePath);
            body.Description = "File upload";
            body.MimeType = GetMimeType(filePath);
            body.Parents = new List<ParentReference>() { new ParentReference() { Id = parent } };

            // File's content.
            byte[] byteArray = System.IO.File.ReadAllBytes(filePath);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
            try
            {
                FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, GetMimeType(filePath));
                request.Upload();
                return request.ResponseBody;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
        }
        else
        {
            Console.WriteLine("File does not exist: " + filePath);
            return null;
        }

    }

    public static void deleteFile(DriveService service, String fileID)
    {
        FilesResource.DeleteRequest DeleteRequest = service.Files.Delete(fileID);
        DeleteRequest.Execute();
    }

    // tries to figure out the mime type of the file.
    private static string GetMimeType(string fileName)
    {
        string mimeType = "application/unknown";
        string ext = System.IO.Path.GetExtension(fileName).ToLower();
        Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (regKey != null && regKey.GetValue("Content Type") != null)
            mimeType = regKey.GetValue("Content Type").ToString();
        return mimeType;
    }


}