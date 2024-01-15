using RestSharp;
using System.Globalization;

namespace app.Utilites.GeoCoding {
    public class GoogleMapsGeoCoding
    {
    public async Task<Coordenates> GetCoordinates(string address)
    {
        string apiKey = "AIzaSyBXMVMKpFlxF1uHhrjzFzSlh3VfrTKTV6A";

           using (var httpClient = new HttpClient())
        {
            string baseUri = "https://maps.googleapis.com/maps/api/geocode/json";

            var parametros = $"?address={Uri.EscapeDataString(address)}&key={apiKey}";

            var resposta = await httpClient.GetStringAsync($"{baseUri}{parametros}");

            // Você pode usar uma biblioteca JSON, como Newtonsoft.Json, para analisar a resposta
            // Neste exemplo, estamos apenas usando as classes integradas do System.Text.Json
            var json = System.Text.Json.JsonDocument.Parse(resposta);

            if (json.RootElement.GetProperty("status").GetString() == "OK")
            {
                var localizacao = json.RootElement.GetProperty("results")[0]
                                   .GetProperty("geometry").GetProperty("location");

                var coordenadas =  new Coordenates
                {
                    Latitude = localizacao.GetProperty("lat").GetDecimal(),
                    Longitude = localizacao.GetProperty("lng").GetDecimal()
                };

                if (coordenadas != null)
                {
                    return coordenadas;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            
            }

        }     
    }

    }
    public class Coordenates
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

}