using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows;

namespace _2025_03_27
{
    public class ServerConnection
    {
        HttpClient client = new HttpClient();
        public ServerConnection()
        {
            
        }
        public async Task<bool> createKolbi(string kolbaszName, float kolbaszGrade, int kolbaszPrice)
        {
            string url = "http://localhost:3000" + "/createKolbasz";

            try
            {
                var jsonInfo = new
                {
                    createKolbaszName = kolbaszName,
                    createKolbaszGrade = kolbaszGrade,
                    createKolbaszPrice = kolbaszPrice
                };
                string jsonStringifed = JsonConvert.SerializeObject(jsonInfo);
                HttpContent sendThis = new StringContent(jsonStringifed, Encoding.UTF8, "Application/json");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                JsonData data = JsonConvert.DeserializeObject<JsonData>(result);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Hiba kód: " + e.Message);
            }
            return false;
        }
        public async Task<bool> deleteKolbi(string kolbaszName)
        {
            string url = "http://localhost:3000" + "/deleteKolbasz";

            try
            {
                var jsonInfo = new
                {
                    deleteKolbaszName = kolbaszName
                };
                string jsonStringified = JsonConvert.SerializeObject(jsonInfo);
                HttpContent sendThis = new StringContent(jsonStringified, Encoding.UTF8, "Application/json");
                var request = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = sendThis
                };
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();
                JsonData data = JsonConvert.DeserializeObject<JsonData>(result);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return false;
        }
        public async Task<List<JsonData>> Allkolbi()
        {
            List<JsonData> all = new List<JsonData>();
            string url = "http://localhost:3000" + "/kolbaszok";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                all = JsonConvert.DeserializeObject<List<JsonData>>(result);
            }
            catch(Exception e)
            {
                Console.WriteLine("Hiba kód: " + e.Message);
            }
            return all;
        }
    }
}
