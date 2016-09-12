using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Security.AccessControl;

namespace TestLocalFile
{
    class Program
    {
        // This method is used to test working with local files
        static void Main(string[] args)
        {
            string filePath = @"C:\hello_drive_1.txt";


            //FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.AllAccess,  "‪C:\\Users\\xlu\\Desktop\\hello_drive_1.txt");
            //AddFileSecurity(filePath, @"APPBRIDGE\xlu",FileSystemRights.ReadData, AccessControlType.Allow);

            if (File.Exists(filePath))
            {
                Console.WriteLine("Yes the file is there");
            } 
            else
            {
                Console.WriteLine("the file does NOT exists");
            }

            Console.ReadLine();

        }


        // Adds an ACL entry on the specified file for the specified account.
        public static void AddFileSecurity(string fileName, string account,
            FileSystemRights rights, AccessControlType controlType)
        {


            // Get a FileSecurity object that represents the
            // current security settings.
            FileSecurity fSecurity = File.GetAccessControl(fileName);

            // Add the FileSystemAccessRule to the security settings.
            fSecurity.AddAccessRule(new FileSystemAccessRule(account,
                rights, controlType));

            // Set the new access settings.
            File.SetAccessControl(fileName, fSecurity);

        }
    }
}
