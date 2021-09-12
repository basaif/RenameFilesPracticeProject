using System;
using System.IO;
using System.Linq;

namespace RenameFilesPracticeProject
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Rename Files Practice Project!");

            string newName = "Drawing";
            string dirForFilesToRename = @"C:\Users\user\Pictures\Drawings\"; //Change to appropiate directiory on your machine

            FileInfo[] orderedFiles = GetFilesOrderedByDate(dirForFilesToRename);

            for (int i = 0; i < orderedFiles.Length; i++)
            {
                string newFilePath = GetNewFileName(newName, orderedFiles[i].FullName, i+1);
                File.Move(orderedFiles[i].FullName, newFilePath);
            }

            Console.WriteLine("Finished");
        }

        static FileInfo[] GetFilesOrderedByDate(string dirPath)
        {
            DirectoryInfo info = new (dirPath);
            FileInfo[] files = info.GetFiles().OrderBy(p => p.LastWriteTime).ToArray();
            return files;
        }

        static string GetNewFileName(string newName, string oldFileName, int fileNumber)
        {
            string date = File.GetLastWriteTime(oldFileName).ToString("d").Replace('/', '-');

            var dirName = Path.GetDirectoryName(oldFileName);

            string extension = GetExtension(oldFileName);

            return $@"{dirName}\{newName} ({fileNumber}) {date}{extension}";
        }

        static string GetExtension(string filePath)
        {
            string fileNameWithExtension = Path.GetFileName(filePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithExtension);

            return fileNameWithExtension[fileNameWithoutExtension.Length..];
        }
    }
}
