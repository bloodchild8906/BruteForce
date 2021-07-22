using System;
using System.Collections.Generic;
using BruteForce.Web;
using BruteForce.Zip;

namespace BruteForce.Application
{
    class Program
    {
        private static readonly Permutations.Permutations Permutations = new Permutations.Permutations();
        private static readonly BruteForceWebClient BruteForceWebClient = new BruteForceWebClient();
        private const string PATH_TO_PREPARED_FILES = @".\PreparedFiles";
        private const string PATH_TO_CV = @"C:\Users\Micha\Google Drive\Personal\Michael CV.pdf";
        private const string USER_NAME = "john";
        private const string PREPARED_ZIP_NAME = ".\\preparedFiles.zip";

        static void Main(string[] args)
        {
            Permutations.CharCombinations = new List<HashSet<char>>
            {
                new HashSet<char> {'a', '@'},
                new HashSet<char> {'o', '0'},
                new HashSet<char> {'s', '5'},
            };
            BruteForceZip.PrepareZipFiles(PATH_TO_PREPARED_FILES, PATH_TO_CV,
                Permutations.GetAllPermutations("password"));
            
            BruteForceWebClient.ApiUrl = "http://recruitment.warpdevelopment.co.za/api/authenticate";
            Permutations.GetAllPermutations("password").ForEach(permutation =>
            {
                if (BruteForceWebClient.GetAuthSuccess(USER_NAME, permutation))
                    Console.WriteLine(BruteForceWebClient.Upload(PREPARED_ZIP_NAME, USER_NAME, permutation)
                        ? "complete"
                        : "failed");
            });
        }
    }
}