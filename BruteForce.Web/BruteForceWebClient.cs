using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BruteForce.Web
{
    public class BruteForceWebClient
    {
        public string ApiUrl { get; set; }
        public string Response { get; private set; }

        public bool GetAuthSuccess(string userName, string password)
        {
            var client = new WebClient();
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            client.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";

            try
            {
                Response = client.DownloadString(ApiUrl);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Upload(string filePath, string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(Response)) return false;
            using var client = new WebClient();
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"));
            client.Headers[HttpRequestHeader.Authorization] = $"Basic {credentials}";
            client.Headers[HttpRequestHeader.ContentType] = "application/json"; 
            var fileBytes = File.ReadAllBytes(filePath);
            var base64EncodedFile = Convert.ToBase64String(fileBytes);
            var jsonFileObject=JsonConvert.SerializeObject(new DataModel {Data = base64EncodedFile});
            try
            {
                client.UploadString(Response, "POST", jsonFileObject);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}