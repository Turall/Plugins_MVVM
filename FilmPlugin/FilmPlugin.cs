using MyPlugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FilmPlugin
{
    public class FilmPlugin : IPlugin
    {
        public string Name { get; set; } = "FilmPlugin";

        public async Task<string> Do(string info)
        {
            HttpClient httpClient = new HttpClient();
            var GetFilms = await httpClient.GetStringAsync($"http://www.omdbapi.com/?apikey=cb2c41fe&t={info}");
            dynamic film = JsonConvert.DeserializeObject(GetFilms);
            return film.Plot;
        }
    }
}
