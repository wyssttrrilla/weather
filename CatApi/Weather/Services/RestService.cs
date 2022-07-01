using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text.Encodings;
using System.Threading.Tasks;
using CatApi.Model;
using System.Text;

namespace CatApi.Services
{
    public class RestService :  IRestService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        List<CatModel> Cats { get; set; }
        public RestService()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);

            serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        public async Task<List<CatModel>> GetCatsAsync()
        {
            Cats = new List<CatModel>();

            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Cats = JsonSerializer.Deserialize<List<CatModel>>(content, serializerOptions);
                }

                Debug.WriteLine("Gooooooooooood");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return Cats;
        }

        public async Task SaveCatAsync(CatModel cat, bool isNewItem)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize(cat, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }

                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task DeleteCatAsync(CatModel cat)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, cat.Id));

            try
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(uri);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Success");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
        }
    }
}
