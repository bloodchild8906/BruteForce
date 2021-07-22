using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace BruteForce.Zip
{
    public static class BruteForceZip
    {
        public static void PrepareZipFiles(string pathOfPreparedDirectory, string pathToCv, List<string> permutations)
        {
            if (!File.Exists(pathToCv)) throw new FileNotFoundException();
            
            if (!Directory.Exists(pathOfPreparedDirectory))
                Directory.CreateDirectory(pathOfPreparedDirectory);
            
            if (File.Exists($"{pathOfPreparedDirectory}\\CV.pdf"))
                File.Delete($"{pathOfPreparedDirectory}\\CV.pdf");

            if (File.Exists(".\\preparedFiles.zip"))
                File.Delete(".\\preparedFiles.zip");
            
            File.WriteAllLines($"{pathOfPreparedDirectory}\\dict.txt", permutations.ToArray());
            File.Copy(pathToCv, $"{pathOfPreparedDirectory}\\CV.pdf");
            ZipFile.CreateFromDirectory(pathOfPreparedDirectory, ".\\preparedFiles.zip");
        }
    }
}