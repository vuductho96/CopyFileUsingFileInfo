// See https://aka.ms/new-console-template for more information
using System;
using System.IO;

namespace LargeFileCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path to the source file:");
            string sourceFilePath = Console.ReadLine();
            Console.WriteLine("Enter the path to the destination file:");
            string destinationFilePath = Console.ReadLine();

            FileInfo sourceFileInfo = new FileInfo(sourceFilePath);
            long fileSize = sourceFileInfo.Length;

            Console.WriteLine($"Copying file of size {fileSize} bytes...");

            const int bufferSize = 1024 * 1024; // 1 MB
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            long totalBytesRead = 0;

            using (FileStream source = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (FileStream destination = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                while ((bytesRead = source.Read(buffer, 0, bufferSize)) > 0)
                {
                    destination.Write(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;

                    Console.WriteLine($"Copied {totalBytesRead} of {fileSize} bytes ({(double)totalBytesRead / fileSize:P})");
                }
            }

            Console.WriteLine("File copy complete.");
        }
    }
}
