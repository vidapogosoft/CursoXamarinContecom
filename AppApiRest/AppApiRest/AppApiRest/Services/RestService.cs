using System;
using System.Collections.Generic;
using System.Text;

using AppApiRest.Model;
using AppApiRest.Comun;

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AppApiRest.Services
{
    public class RestService: IRestService
    {

        public List<clsRegistrados> ListRegistrados { get; set; }

        HttpClient _client;

        public RestService()
        {

            _client = new HttpClient();

        }

       public async Task<List<clsRegistrados>> GetRegistrados()
        {
            ListRegistrados = new List<clsRegistrados>();

            var uri = new Uri(string.Format(Constants.ApiUrl, "PerfilDUO", string.Empty));

            try
            {

                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();

                    ListRegistrados = JsonConvert.DeserializeObject<List<clsRegistrados>>(content);

                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
            }

            return ListRegistrados;
        }


        public async Task<int> SaveRegistro(clsPerfilDUO item, bool isNewItem)
        {
            int grabado = 0;

            var uri = new Uri(string.Format(Constants.ApiUrl, "PerfilDUO", string.Empty));

            try
            {

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    response = await _client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    grabado = 1;
                    Debug.WriteLine(@"TodoItem: Saved correct.");
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error {0}", ex.Message);
                return grabado;
            }

            return grabado;
        }

    }
}
