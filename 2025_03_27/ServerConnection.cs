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
        string serverUrl = "";
        public ServerConnection(string serverUrl)
        {
            this.serverUrl = serverUrl;
        }
        public async Task<bool> createKolbi(string kolbaszName, float kolbaszGrade, int kolbaszPrice)
        {
            string url = serverUrl + "/createKolbasz";

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
        public async Task<bool> deleteKolbi(int id)
        {
            string url = serverUrl + "/deleteKolbasz";

            try
            {
                var jsonInfo = new
                {
                    id = id,
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
        public async Task<List<JsonData>> Allkolbi()
        {
            List<JsonData> all = new List<JsonData>();
            string url = serverUrl + "/kolbaszok";
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
