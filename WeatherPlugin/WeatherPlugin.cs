using MyPlugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPlugin
{
    public class WeatherPlugin : IPlugin
    {
        public string Name { get; set; } = "Weather";

        public async Task<string> Do(string info)
        {
            HttpClient httpClient = new HttpClient();
            var WeatherInfo = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={info}&appid=cc4493b68903567df79e51fae129dfb3");
            dynamic weatherobject = JsonConvert.DeserializeObject(WeatherInfo);
            var result = String.Format(" Name : {0} \n Wheather Temp:  {1} \n Pressure : {2} \n Min Temp : {3} \n Max Temp : {4} ", weatherobject.name , weatherobject.main.temp, weatherobject.main.pressure, weatherobject.main.temp_min, weatherobject.main.temp_max);
            return result;
        }
    }
}
